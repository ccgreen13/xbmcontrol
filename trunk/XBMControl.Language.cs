// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 3.5)
//    Copyright (C) 2008  Bram van Oploo (bramvano@gmail.com)
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------

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
