using System;

namespace QFlashKit.code.module
{
    public class Script
    {
        public static string AndroidPath => AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                            "Source\\ThirdParty\\Google\\Android";

        public static string fastboot => AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                         "Source\\ThirdParty\\Google\\Android\\fastboot.exe";

        public static string QcLsUsb => AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                        "Source\\ThirdParty\\Qualcomm\\fh_loader\\lsusb.exe";

        public static string emmcdl => AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                       "Source\\ThirdParty\\Qualcomm\\emmcdl.exe";
    }
}