using System;
using System.Xml;

namespace QFlashKit.code.lan
{
    public class LanguageProvider
    {
        private readonly string languageType = "";

        public LanguageProvider(string lanType)
        {
            languageType = lanType;
        }

        public string GetLanguage(string ctrlID)
        {
            var xmlDocument = new XmlDocument();
            new XmlReaderSettings().IgnoreComments = true;
            var reader = XmlReader.Create(AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                          "Source\\LanguageLibrary.xml");
            xmlDocument.Load(reader);
            var childNodes = xmlDocument.SelectSingleNode("LanguageLibrary").ChildNodes;
            var str = "";
            foreach (XmlElement xmlElement in childNodes)
                if (!(xmlElement.Name.ToLower() != "lan") && xmlElement.Attributes["CTRLID"].Value == ctrlID)
                {
                    str = xmlElement.Attributes[languageType].Value;
                    break;
                }

            return str;
        }
    }
}