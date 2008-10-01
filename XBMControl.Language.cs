using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XBMControl.Language
{
    class XBMCLanguage
    {
        private string languageFilePath;
        private XmlDocument languageFile;
        private XmlNode languageNode;
        private DirectoryInfo languageDir;

        public XBMCLanguage()
        {
            this.languageFile = new XmlDocument();
            this.languageDir  = new DirectoryInfo("language/");
        }

        public string[] GetAvailableLanguages()
        {
            int counter              = 0;
            FileInfo[] languageFiles = this.languageDir.GetFiles("*.xml");
            string[] languages       = new string[languageFiles.Length];

            foreach (FileInfo f in languageFiles)
                languages[counter++] = f.Name.Replace(".xml", "");

            return languages;
        }

        public void SetLanguage(string language)
        {
            this.languageFilePath = "language/" + language + ".xml";
            this.languageFile.Load(languageFilePath);
        }

        public string GetString(string node)
        {
            this.languageNode = this.languageFile.DocumentElement.SelectSingleNode("/language/" + node);
            return this.languageNode.InnerText.Replace("\\n", "\n");
        }
    }
}
