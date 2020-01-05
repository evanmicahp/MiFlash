using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace QFlashKit.code.Utility
{
    public class MiAppConfig
    {
        private static String _exePath = Application.ExecutablePath.Replace(".exe", String.Empty);

        public static void Add(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Add(key, value);
            configuration.Save();
        }

        public static void SetValue(string appKey, string appValue)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_exePath + ".config");
            var xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            var xmlElement = (XmlElement) xmlNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xmlElement != null)
            {
                xmlElement.SetAttribute("value", appValue);
            }
            else
            {
                var element = xmlDocument.CreateElement("add");
                element.SetAttribute("key", appKey);
                element.SetAttribute("value", appValue);
                xmlNode.AppendChild(element);
            }

            xmlDocument.Save(_exePath + ".config");
            ConfigurationManager.RefreshSection(appKey);
        }

        public static string GetAppConfig(string appKey)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_exePath + ".config");
            var xmlElement = (XmlElement) xmlDocument.SelectSingleNode("//appSettings")
                .SelectSingleNode("//add[@key='" + appKey + "']");
            if (xmlElement != null)
                return xmlElement.Attributes["value"].Value;
            return string.Empty;
        }

        public static string Get(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
                Add(key, "");
            return ConfigurationManager.AppSettings[key];
        }
    }
}