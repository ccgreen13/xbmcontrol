// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 2.0)
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
            connection.Timeout = 1000;
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
            return (this.GetNowPlayingInfo("filename", true) == "[Nothing Playing]") ? false : true;
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
                response.Close();
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

        public string GetNowPlayingInfo(string field)
        {
            return this.GetNowPlayingInfo(field, false);
        }

        public MemoryStream GetThumbnail()
        {
            MemoryStream thumb = null;
            WebClient client   = new WebClient();
            string thumbUrl    = "http://" + Settings.Default.Ip + "/thumb.jpg";
            this.Request("GetCurrentlyPlaying", "q:\\web\\thumb.jpg");

            try
            {
                thumb = new MemoryStream(client.DownloadData(thumbUrl));
            }
            catch (Exception e)
            { 
            }
            finally
            {
                client.Dispose();
            }

            return thumb;
        }
    }
}
