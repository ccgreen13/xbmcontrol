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
using System.Net;
using System.IO;
using XBMControl.Properties;
using System.Windows.Forms;
using System.Drawing;

namespace XBMC.Communicator
{
    class XBMCcomm
    {
        private string _ApiPath             = "/xbmcCmds/xbmcHttp";
        private string[,] maNowPlayingInfo  = new string[50, 2];

        public XBMCcomm()
        {
        }

        public bool IsConnected(string ip)
        {
            bool connected           = true;
            string xbmcIp            = (ip == null) ? Settings.Default.Ip : ip;
            HttpWebResponse response = null;

            HttpWebRequest connection = (HttpWebRequest)WebRequest.Create("http://" + xbmcIp);
            connection.Method = "GET";
            connection.Timeout = XBMControl.Properties.Settings.Default.ConnectionTimeout;
            try
            {
                response = (HttpWebResponse)connection.GetResponse();
            }
            catch (Exception e)
            {
                connected = false;
            }

            if (response != null) response.Close();

            return connected;
        }

        public bool IsConnected()
        { 
            return IsConnected(null);
        }

        public bool IsPlaying()
        {
            return (this.GetNowPlayingInfo("playstatus", true) == "Playing") ? true : false;
        }

        public bool IsPaused()
        {
            return (this.GetNowPlayingInfo("playstatus", true) == "Paused") ? true : false;
        }

        public bool IsMuted()
        {
            string[] aGetVolume = this.Request("GetVolume");
            return (aGetVolume[1] == "0") ? true : false;
        }

        public string[] Request(string command, string parameter)
        {
            HttpWebResponse response    = null;
            StreamReader reader         = null;
            string[] pageContent        = null;
            string param                = "?command=" + command;
            if (parameter != null) param += "&parameter=" + parameter;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Settings.Default.Ip + this._ApiPath + param);
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                pageContent = reader.ReadToEnd().Replace("<li>", "|").Replace("\n", "").Replace("<html>", "").Replace("</html>", "").Split('|');
            }
            catch (Exception e)
            {
            }
            finally
            {
                if(response.StatusCode == HttpStatusCode.OK) response.Close();
                reader.Close();
            }

            return pageContent;
        }

        public string[] Request(string command)
        {
            return this.Request(command, null);
        }

        public string GetNowPlayingInfo(string field, bool refresh)
        {
            string returnValue = null;
            if (refresh)
            {
                string[] aNowPlayingTemp = this.Request("GetCurrentlyPlaying");
                for (int x = 0; x < aNowPlayingTemp.Length; x++)
                {
                    int splitIndex = aNowPlayingTemp[x].IndexOf(':') + 1;
                    if (splitIndex > 1)
                    {
                        this.maNowPlayingInfo[x, 0] = aNowPlayingTemp[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                        this.maNowPlayingInfo[x, 1] = aNowPlayingTemp[x].Substring(splitIndex, aNowPlayingTemp[x].Length - splitIndex);
                        if (this.maNowPlayingInfo[x, 0] == field) returnValue = this.maNowPlayingInfo[x, 1];
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.maNowPlayingInfo.GetLength(0); x++)
                    if (this.maNowPlayingInfo[x, 0] == field) returnValue = this.maNowPlayingInfo[x, 1];
            }

            return returnValue;
        }

        public string GetGuiDescription(string field)
        {
            string returnValue = null;
            string[] aGuiDescription = this.Request("GetGUIDescription");
            for (int x = 0; x < aGuiDescription.Length; x++)
            {
                int splitIndex = aGuiDescription[x].IndexOf(':') + 1;
                if (splitIndex > 1)
                {
                    string resultField = aGuiDescription[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                    if( resultField == field) returnValue = aGuiDescription[x].Substring(splitIndex, aGuiDescription[x].Length - splitIndex);
                }
            }

            return returnValue;
        }

        public string GetNowPlayingInfo(string field)
        {
            return this.GetNowPlayingInfo(field, false);
        }

        public MemoryStream GetImageFromXbmc(string xbmcFilePath)
        {
            //Image imageFile       = null;
            MemoryStream stream   = null;
            WebClient client      = new WebClient();
            Uri xbmcUri           = new Uri("http://" + Settings.Default.Ip + "/xbmcCmds/xbmcHttp?command=FileDownload&parameter=" + xbmcFilePath);

            try
            {
                stream = new MemoryStream(client.DownloadData(xbmcUri));
            }
            catch (Exception e)
            { 
            }
            finally
            {
                client.Dispose();
            }

            return stream;
        }

        public Image GetNowPlayingCoverArt()
        {
            MemoryStream stream = null;
            Image thumbnail = null;
            WebClient client = new WebClient();
            Uri xbmcUri = new Uri("http://" + Settings.Default.Ip + "/thumb.jpg");
            this.Request("GetCurrentlyPlaying", "q:\\web\\thumb.jpg");

            try
            {
                stream    = new MemoryStream(client.DownloadData(xbmcUri));
                thumbnail = new Bitmap(Image.FromStream(stream));
            }
            catch (Exception e)
            {
            }
            finally
            {
                client.Dispose();
            }

            return thumbnail;   
        }

        public Image Base64StringToImage(string base64String, string fileType)
        {
            Bitmap file = null;

            byte[] bytes = Convert.FromBase64String(base64String);
            MemoryStream stream = new MemoryStream(bytes);

            if (base64String != null && base64String != "")
            {
                if (fileType == "image") file = new Bitmap(Image.FromStream(stream));
            }
                
            return file;
        }

        public Image TakeScreenshot()
        {
            string[] aRequestData = this.Request("takescreenshot", "screenshot.jpg;false;0;" + this.GetGuiDescription("width") + ";" + this.GetGuiDescription("height") + ";90;true;");
            Image screenshot      = this.Base64StringToImage(aRequestData[0], "image");

            return screenshot;
        }
    }
}
