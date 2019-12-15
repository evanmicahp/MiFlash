using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using QFlashKit.code.data;

namespace QFlashKit.code.Utility
{
    public class Comm
    {
        private volatile bool _keepReading;
        public bool ignoreResponse = true;
        public int intSectorSize;
        public bool isDump;
        public int m_dwBufferSectors;
        public int MAX_SECTOR_STR_LEN = 20;
        public byte[] recData;
        private long received_count;
        public int SECTOR_SIZE_EMMC = 512;
        public int SECTOR_SIZE_UFS = 4096;
        public SerialPort serialPort;
        public Thread thread;

        public Comm()
        {
            serialPort = new SerialPort();
            thread = null;
            _keepReading = false;
        }

        public bool IsOpen
        {
            get
            {
                var num = 10;
                while (num-- > 0 && !serialPort.IsOpen)
                {
                    Log.w(serialPort.PortName, "wait for port open.");
                    Thread.Sleep(1000);
                }

                return serialPort.IsOpen;
            }
        }

        public void StartReading()
        {
            if (_keepReading)
                return;
            _keepReading = true;
            thread = new Thread(ReadPort);
            thread.Start();
        }

        public void StopReading()
        {
            if (!_keepReading)
                return;
            _keepReading = false;
            thread.Join();
            thread = null;
        }

        private void ReadPort()
        {
            while (_keepReading)
                if (serialPort.IsOpen)
                {
                    var bytesToRead = serialPort.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        var buffer = new byte[bytesToRead];
                        try
                        {
                            serialPort.Read(buffer, 0, bytesToRead);
                        }
                        catch (TimeoutException ex)
                        {
                            Log.w(serialPort.PortName, ex, false);
                        }
                    }
                }
        }

        public byte[] ReadPortData()
        {
            byte[] buffer = null;
            if (serialPort.IsOpen)
            {
                var bytesToRead = serialPort.BytesToRead;
                if (bytesToRead > 0)
                {
                    buffer = new byte[bytesToRead];
                    try
                    {
                        serialPort.Read(buffer, 0, bytesToRead);
                    }
                    catch (TimeoutException ex)
                    {
                        Log.w(serialPort.PortName, ex, false);
                    }
                }
            }

            return buffer;
        }

        public byte[] ReadPortData(int offset, int count)
        {
            var buffer = new byte[count];
            try
            {
                serialPort.Read(buffer, offset, count);
            }
            catch (TimeoutException ex)
            {
                Log.w(serialPort.PortName, ex, false);
            }

            return buffer;
        }

        public void Open()
        {
            Close();
            serialPort.Open();
            if (serialPort.IsOpen)
                return;
            var str = "open serial port failed!";
            Log.w(serialPort.PortName, str);
            FlashingDevice.UpdateDeviceStatus(serialPort.PortName, new float?(), str, "error", true);
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytesToRead = serialPort.BytesToRead;
            recData = new byte[bytesToRead];
            received_count += bytesToRead;
            serialPort.Read(recData, 0, bytesToRead);
        }

        public void Close()
        {
            StopReading();
            serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (!IsOpen)
                return;
            var num = 100;
            Exception ex1 = new TimeoutException();
            var flag = false;
            while (num-- > 0 && ex1 != null)
                if (ex1.GetType() == typeof(TimeoutException))
                    try
                    {
                        serialPort.WriteTimeout = 1000;
                        serialPort.Write(send, offSet, count);
                        flag = true;
                        if (isDump)
                            Dump(send);
                        ex1 = null;
                    }
                    catch (TimeoutException ex2)
                    {
                        ex1 = ex2;
                        Log.w(serialPort.PortName, "write time out try agian " + (100 - num));
                        Thread.Sleep(500);
                    }
                    catch (Exception ex2)
                    {
                        Log.w(serialPort.PortName, "write failed:" + ex2.Message);
                    }
                else
                    break;

            if (flag)
                return;
            Log.w(serialPort.PortName, ex1, true);
        }

        public bool SendCommand(string command)
        {
            return SendCommand(command, false);
        }

        public bool SendCommand(string command, bool checkAck)
        {
            var bytes = Encoding.Default.GetBytes(command);
            if (isDump || checkAck)
                Log.w(serialPort.PortName, "send command:" + command);
            WritePort(bytes, 0, bytes.Length);
            if (checkAck)
                return GetResponse(checkAck);
            return false;
        }

        public byte[] getRecData()
        {
            var binary = ReadDataFromPort();
            if (binary == null)
                throw new Exception("can not read from port " + serialPort.PortName);
            if (binary.Length > 0 && isDump)
            {
                Log.w(serialPort.PortName, "read from port:");
                Dump(binary);
            }

            return binary;
        }

        private byte[] ReadDataFromPort()
        {
            var num = 10;
            recData = null;
            for (recData = ReadPortData(); num-- > 0 && recData == null; recData = ReadPortData())
                Thread.Sleep(500);
            return recData;
        }

        private bool WaitForAck()
        {
            var flag = false;
            var num = 10;
            while (num-- > 0 && !flag)
            {
                var strArray = Dump(ReadDataFromPort());
                flag = strArray.Length == 2 && strArray[1].IndexOf("<response value=\"ACK\" />") >= 0;
                Thread.Sleep(500);
            }

            return flag;
        }

        public bool GetResponse(bool waiteACK)
        {
            var flag = false;
            Log.w(serialPort.PortName, "get response from target");
            if (!waiteACK)
                return ReadDataFromPort() != null;
            var num = 2;
            if (waiteACK)
                num = 10;
            while (num-- > 0 && !flag)
            {
                var responseXml = GetResponseXml(waiteACK);
                var count = responseXml.Count;
                foreach (XmlNode xmlNode in responseXml)
                foreach (XmlNode childNode in xmlNode.SelectSingleNode("data").ChildNodes)
                foreach (XmlAttribute attribute in childNode.Attributes)
                {
                    if (attribute.Name.ToLower() == "maxpayloadsizetotargetinbytes")
                        m_dwBufferSectors = Convert.ToInt32(attribute.Value) / intSectorSize;
                    if (attribute.Value.ToLower() == "ack")
                        flag = true;
                }

                if (waiteACK)
                    Thread.Sleep(500);
            }

            return flag;
        }

        private List<XmlDocument> GetResponseXml()
        {
            return GetResponseXml(false);
        }

        private List<XmlDocument> GetResponseXml(bool waiteACK)
        {
            var xmlDocumentList = new List<XmlDocument>();
            var strArray = Dump(ReadDataFromPort(), waiteACK);
            if (strArray.Length >= 2)
                foreach (var str in Regex.Split(strArray[1], "\\<\\?xml").ToList())
                    if (!string.IsNullOrEmpty(str))
                    {
                        var xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml("<?xml " + str);
                        xmlDocumentList.Add(xmlDocument);
                    }

            return xmlDocumentList;
        }

        private string GetResponseXmlStr()
        {
            return Dump(ReadDataFromPort())[1];
        }

        private string[] Dump(byte[] binary)
        {
            return Dump(binary, false);
        }

        private string[] Dump(byte[] binary, bool waitACK)
        {
            Log.w(serialPort.PortName, "dump:");
            if (binary == null)
            {
                Log.w(serialPort.PortName, "no Binary dump");
                return new string[2] {"", ""};
            }

            var stringBuilder1 = new StringBuilder();
            var stringBuilder2 = new StringBuilder();
            var stringBuilder3 = new StringBuilder();
            var stringBuilder4 = new StringBuilder();
            var num = 0;
            while (num < binary.Length)
            {
                for (var index = 0; index < 16; ++index)
                    if (num + index < binary.Length)
                        stringBuilder4.Append(Convert.ToChar(binary[num + index]).ToString());
                    else
                        stringBuilder4.Append(" ");
                stringBuilder2.Append(stringBuilder4);
                stringBuilder3.Length = 0;
                stringBuilder4.Length = 0;
                num += 16;
            }

            if (isDump || waitACK)
                Log.w(serialPort.PortName, stringBuilder2 + "\r\n\r\n");
            return new string[2]
            {
                stringBuilder1.ToString(),
                stringBuilder2.ToString()
            };
        }
    }
}