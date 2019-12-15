using System;
using System.IO;
using System.Text;
using QFlashKit.code.data;

namespace QFlashKit.code.Utility
{
    public class Log
    {
        public static void w(string deviceName, Exception ex, bool stopFlash)
        {
            w(deviceName, ex.Message);
            w(deviceName, ex.StackTrace);
            FlashingDevice.UpdateDeviceStatus(deviceName, new float?(), ex.Message, "error", stopFlash);
        }

        public static void w(string deviceName, string msg)
        {
            var str = "";
            if (FlashingDevice.flashDeviceList.Count <= 0)
                return;
            foreach (var flashDevice in FlashingDevice.flashDeviceList)
                if (flashDevice.Name == deviceName)
                {
                    str = string.Format("{0}@{1}.txt", flashDevice.Name, flashDevice.StartTime.ToString("yyyyMdHms"));
                    break;
                }

            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" + str;
            if (!File.Exists(path))
                File.Create(path).Close();
            var streamWriter =
                new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                    Encoding.Default);
            streamWriter.WriteLine("[{0}  {1}]:{2}", DateTime.Now.ToLongTimeString(), deviceName, msg);
            streamWriter.Close();
            if (msg.ToLower().IndexOf("error") < 0)
                return;
            FlashingDevice.UpdateDeviceStatus(deviceName, new float?(), msg, "error", true);
        }

        public static void w(string msg)
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" +
                       string.Format("{0}@{1}.txt", "mifalsh", DateTime.Now.ToString("yyyyMd"));
            if (!File.Exists(path))
                File.Create(path).Close();
            var streamWriter =
                new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                    Encoding.Default);
            streamWriter.WriteLine("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg);
            streamWriter.Close();
        }

        public static void Installw(string installationPath, string msg)
        {
            var str = string.Format("{0}@{1}.txt", "mifalsh", DateTime.Now.ToString("yyyyMd"));
            var path = installationPath + "log\\" + str;
            if (!File.Exists(path))
                File.Create(path).Close();
            var streamWriter =
                new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                    Encoding.Default);
            streamWriter.WriteLine("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg);
            streamWriter.Close();
        }
    }
}