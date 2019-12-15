using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QFlashKit.code.module;

namespace QFlashKit.code.Utility
{
    public class ComPortCtrl
    {
        private static string[] getDevices()
        {
            var stringList = new List<string>();
            try
            {
                foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\" and Name LIKE '%Qualcomm HS-USB QDLoader 9008%'")
                    .Get())
                {
                    var str = managementObject.GetPropertyValue("Name").ToString()
                        .Replace(managementObject.GetPropertyValue("Description").ToString(), "").Replace('(', ' ')
                        .Replace(')', ' ');
                    stringList.Add(str.Trim());
                }
            }
            catch (Exception ex)
            {
                var num = (int) MessageBox.Show(ex.Message);
                Log.w(ex.Message);
            }

            return stringList.ToArray();
        }

        // fungsi untuk mendekteksi hp
        // dalam keadaan edl
        // melalui command qclsusb.exe 
        public static string[] getDevicesQc()
        {
            var stringList = new List<string>();
            var qcLsUsb = Script.QcLsUsb;
            if (!File.Exists(qcLsUsb))
                throw new Exception("no lsusb.");
            var strArray = Regex.Split(new Cmd("").Execute(null, qcLsUsb), "\r\n");
            for (var index = 0; index < strArray.Length; ++index)
                if (!string.IsNullOrEmpty(strArray[index]) && strArray[index].IndexOf("9008") > 0)
                {
                    var str = strArray[index].Split('(')[1].Replace(')', ' ');
                    stringList.Add(str.Trim());
                }

            return stringList.ToArray();
        }
    }
}