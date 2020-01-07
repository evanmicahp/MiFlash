using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QFlashKit;

namespace QFlashKitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ConvertArraytoList()
        {
            MiInstaller installer = new MiInstaller();
            String[] names = new string[5]
            {
                "Google\\Driver\\android_winusb.inf",
                "Nvidia\\Driver\\NvidiaUsb.inf",
                "Microsoft\\Driver\\tetherxp.inf",
                "Microsoft\\Driver\\wpdmtphw.inf",
                "Qualcomm\\Driver\\qcser.inf"
            };
            List<string> getNames,test = new List<string>();
            getNames = installer.GetDriverNames();
            foreach (var str in names)
            {
                test.Add(str);
            }
            Assert.True(Enumerable.SequenceEqual(test, getNames));
        }
    }
}