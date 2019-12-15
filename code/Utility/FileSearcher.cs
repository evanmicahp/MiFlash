using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace QFlashKit.code.Utility
{
    public class FileSearcher
    {
        public static string[] SearchFiles(string destinationDic, string pattern)
        {
            var stringList = new List<string>();
            foreach (var file in new DirectoryInfo(destinationDic).GetFiles())
            {
                var match = new Regex(pattern).Match(file.Name);
                if (match.Groups.Count > 0 && match.Groups[0].Value == file.Name)
                    stringList.Add(file.FullName);
            }

            return stringList.ToArray();
        }
    }
}