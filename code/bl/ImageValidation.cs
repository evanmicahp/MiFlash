using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;

namespace QFlashKit.code.bl
{
    public class ImageValidation
    {
        public static string Validate(string path)
        {
            var flag = true;
            var str1 = "";
            var strArray = new string[3]
            {
                "system.img",
                "userdata.img",
                "cust.img"
            };
            var hashtable1 = new Hashtable();
            var directoryInfo = new DirectoryInfo(path);
            foreach (var directory in directoryInfo.GetDirectories())
                if (directory.Name.ToLower() == "images")
                {
                    directoryInfo = directory;
                    break;
                }

            var fullName = directoryInfo.FullName;
            foreach (var str2 in strArray.ToList())
            {
                var fileName = directoryInfo.FullName + string.Format("\\{0}", str2);
                hashtable1[str2] = Utility.Utility.GetMD5HashFromFile(fileName);
            }

            var hashtable2 = new Hashtable();
            var str3 = directoryInfo.FullName + "\\md5sum.xml";
            if (File.Exists(str3))
            {
                var xmlDocument = new XmlDocument();
                var reader = XmlReader.Create(str3, new XmlReaderSettings
                {
                    IgnoreComments = true
                });
                xmlDocument.Load(reader);
                foreach (XmlElement childNode in xmlDocument.SelectSingleNode("data").ChildNodes)
                {
                    foreach (XmlAttribute attribute in childNode.Attributes)
                        if (attribute.Name.ToLower() == "name")
                            foreach (var str2 in strArray.ToList())
                                if (attribute.Value.ToLower() == str2)
                                {
                                    flag &= hashtable1[str2].ToString() == childNode.Value;
                                    if (!flag)
                                        str1 = string.Format("{0} md5 validate failed!", str2);
                                }

                    if (!flag)
                        break;
                }
            }

            return str1;
        }
    }
}