//
// class untuk mendekteksi perangkat atau handphone 
// dalam mode EDl atau Fastboot
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using QFlashKit.code.bl;
using QFlashKit.code.module;

namespace QFlashKit.code.Utility
{
    public class UsbDevice
    {
        // fungsi untuk mendekteksi hp
        // dalam keadaan edl
        // melalui ComPortCtrl class
        public static List<Device> GetDevice()
        {
            //list devices
            var deviceList = new List<Device>();
            //check devices in edl mode
            foreach (var str in ComPortCtrl.getDevicesQc())
                //save device
                deviceList.Add(new Device
                {
                    Name = str,
                    DeviceCtrl = new SerialPortDevice()
                });
            //chek device in fastboot mode
            foreach (var str in GetScriptDevice())
                deviceList.Add(new Device
                {
                    Name = str,
                    DeviceCtrl = new ScriptDevice()
                });
            return deviceList;
        }

        // fungsi untuk mendekteksi hp
        // dalam keadaan fastboot
        public static string[] GetScriptDevice()
        {
            var stringList = new List<string>();
            var fastboot = Script.fastboot;
            Log.w("fastboot path: " + fastboot);
            if (!File.Exists(fastboot))
                throw new Exception("no fastboot.");
            var strArray = Regex.Split(new Cmd("").Execute(null, fastboot + " devices"), "\r\n");
            for (var index = 0; index < strArray.Length; ++index)
                if (!string.IsNullOrEmpty(strArray[index]))
                    stringList.Add(Regex.Split(strArray[index], "\t")[0]);
            return stringList.ToArray();
        }
    }
}