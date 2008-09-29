using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XBMControl.Language
{
    class XBMCLanguage
    {
        private string moduleString;
        private string languageFilePath;
        private XmlDocument languageFile;
        private XmlNode languageNode;

        public XBMCLanguage()
        {
            this.languageFile = new XmlDocument();
        }

        public void SetLanguage(string language)
        {
            this.languageFilePath = "language/" + language + ".xml";
            this.languageFile.Load(languageFilePath);
        }

        public void SetModule(string module)
        {
            this.moduleString = module;  
        }

        public string GetString(string node)
        {
            this.languageNode = this.languageFile.DocumentElement.SelectSingleNode("/language/" +moduleString+ "/" + node);
            return languageNode.InnerText;
        }
    }
}
