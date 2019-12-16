using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using Microsoft.Win32;
using QFlashKit.code.Utility;

namespace QFlashKit
{
    [RunInstaller(true)]
    public class MiInstaller : Installer
    {
        private IContainer components;

        public MiInstaller()
        {
            InitializeComponent();
            BeforeInstall += MiInstaller_BeforeInstall;
            AfterInstall += MiInstaller_AfterInstall;
        }

        public event EventHandler EventInstall;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
        }

        private void MiInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            CopyFiles();
            InstallAllDriver();
        }

        public override void Install(IDictionary savedState)
        {
            try
            {
                base.Install(savedState);
            }
            catch (Exception)
            {
            }
        }

        private void MiInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
        }

        public void CopyInstallDrivers()
        {
            CopyFiles();
            InstallAllDriver();
        }

        protected virtual void EventInstallDrivers(EventArgs e)
        {
            var handler = EventInstall;
            handler?.Invoke(this, e);
        }

        private void CopyFiles()
        {
            var parameter = Context.Parameters["assemblypath"];
            var installationPath = parameter.Substring(0, parameter.LastIndexOf('\\') + 1);
            try
            {
                var systemDirectory = Environment.SystemDirectory;
                var strArray1 = new string[1]
                {
                    "Qualcomm\\Driver\\serial\\i386\\qcCoInstaller.dll"
                };
                var strArray2 = new string[1]
                {
                    systemDirectory + "\\qcCoInstaller.dll"
                };
                var str = installationPath + "Source\\ThirdParty\\";
                for (var index = 0; index < strArray1.Length; ++index)
                {
                    File.Copy(str + strArray1[index], strArray2[index], false);
                    Log.Installw(installationPath,
                        string.Format("copy {0} to {1}", str + strArray1[index], strArray2[index]));
                }
            }
            catch (Exception ex)
            {
                Log.Installw(installationPath, string.Format("copy file failed,{0}", ex.Message));
            }
        }

        private void InstallAllDriver()
        {
            var parameter = Context.Parameters["assemblypath"];
            var installationPath = parameter.Substring(0, parameter.LastIndexOf('\\') + 1);
            var strArray = new string[5]
            {
                "Google\\Driver\\android_winusb.inf",
                "Nvidia\\Driver\\NvidiaUsb.inf",
                "Microsoft\\Driver\\tetherxp.inf",
                "Microsoft\\Driver\\wpdmtphw.inf",
                "Qualcomm\\Driver\\qcser.inf"
            };
            var path = installationPath + "Source\\ThirdParty\\";
            if (new DirectoryInfo(path).Exists)
                for (var index = 0; index < strArray.Length; ++index)
                    InstallDriver(path + strArray[index], installationPath);
            else
                Log.Installw(installationPath, "dic " + path + " not exists.");
        }

        private void InstallDriver(string infPath, string installationPath)
        {
            try
            {
                var str1 = "Software\\QFlashKit\\QFlashKit\\"; // TODO rename this
                var fileInfo = new FileInfo(infPath);
                var localMachine = Registry.LocalMachine;
                var registryKey = localMachine.OpenSubKey(str1, true);
                Log.Installw(installationPath, string.Format("open RegistryKey {0}", str1));
                if (registryKey == null)
                {
                    registryKey = localMachine.CreateSubKey(str1, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    Log.Installw(installationPath, string.Format("create RegistryKey {0}", str1));
                }

                registryKey.GetValueNames(); // TODO check if null possible
                registryKey.GetValue(fileInfo.Name);
                var success = true;
                var destinationInfFileNameComponent = "";
                var destinationInfFileName = "";
                var str2 = Driver.SetupOEMInf(fileInfo.FullName, out destinationInfFileName,
                    out destinationInfFileNameComponent, out success);
                Log.Installw(installationPath,
                    string.Format("install driver {0} to {1},result {2},GetLastWin32Error {3}", fileInfo.FullName,
                        destinationInfFileName, success.ToString(), str2));
                if (success)
                {
                    registryKey.SetValue(fileInfo.Name, destinationInfFileNameComponent);
                    Log.Installw(installationPath,
                        string.Format("set RegistryKey value:{0}--{1}", fileInfo.Name,
                            destinationInfFileNameComponent));
                }

                registryKey.Close();
                if (infPath.IndexOf("android_winusb.inf") < 0) // ignore culture-specific warning
                    return;
                var environmentVariable = Environment.GetEnvironmentVariable("USERPROFILE");
                var cmd = new Cmd("");
                var str3 = string.Format("mkdir \"{0}\\.android\"", environmentVariable);
                var str4 = cmd.Execute(null, str3);
                Log.Installw(installationPath, str3);
                Log.Installw(installationPath, "output:" + str4);
                var str5 = string.Format(" echo 0x2717 >>\"{0}\\.android\\adb_usb.ini\"", environmentVariable);
                var str6 = cmd.Execute(null, str5);
                Log.Installw(installationPath, str5);
                Log.Installw(installationPath, "output:" + str6);
            }
            catch (Exception ex)
            {
                Log.Installw(installationPath, string.Format("install driver {0}, exception:{1}", infPath, ex.Message));
            }
        }
    }
}