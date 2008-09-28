using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
//using System.Windows.Forms;

namespace XBMC.Communicator
{
    class XBMCcomm
    {
        private string _XbmcUrl             = null;
        private string _ApiPath             = "/xbmcCmds/xbmcHttp";
        private string[,] maNowPlayingInfo  = new string[50, 2];

        public XBMCcomm()
        {
        }

        public void SetIp(string ip)
        {
            this._XbmcUrl = "http://" + ip;
        }

        public string GetIp()
        {
            return _XbmcUrl;
        }

        public bool IsConnected()
        {
            HttpWebRequest connection = (HttpWebRequest)WebRequest.Create(this._XbmcUrl);
            connection.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)connection.GetResponse();
            bool connected = (response == null) ? false : true;
            response.Close();
            if (connected) this.Request("SetResponseFormat");
          
            return connected;
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this._XbmcUrl + this._ApiPath + param);
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                pageContent = reader.ReadToEnd().Replace("<li>", "|").Replace("\n", "").Replace("<html>", "").Replace("</html>", "").Split('|');
 
                return pageContent;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                response.Close();
                reader.Close();
            }
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
            string thumbUrl    = this.GetIp() + "/thumb.jpg";
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
