using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;

namespace XBMC
{
    public class XBMC_NowPlaying
    {
        XBMC_Communicator parent = null;
        private string[,] maNowPlayingInfo = null;
        private string error = null;

        public XBMC_NowPlaying(XBMC_Communicator p)
        {
            parent = p;
        }

        public string Get(string field, bool refresh)
        {
            string returnValue = null;
            if (refresh)
            {
                string[] aNowPlayingTemp = parent.Request("GetCurrentlyPlaying");

                if (aNowPlayingTemp != null)
                {
                    if (aNowPlayingTemp.Length > 1)
                    {
                        maNowPlayingInfo = new string[aNowPlayingTemp.Length, 2];
                        for (int x = 0; x < aNowPlayingTemp.Length; x++)
                        {
                            int splitIndex = aNowPlayingTemp[x].IndexOf(':') + 1;
                            if (splitIndex > 2)
                            {
                                this.maNowPlayingInfo[x, 0] = aNowPlayingTemp[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                                this.maNowPlayingInfo[x, 1] = aNowPlayingTemp[x].Substring(splitIndex, aNowPlayingTemp[x].Length - splitIndex);
                                if (this.maNowPlayingInfo[x, 0] == field)
                                    returnValue = this.maNowPlayingInfo[x, 1];
                            }
                        }
                    }
                    else
                        return null;
                }
            }
            else
            {
                for (int x = 0; x < this.maNowPlayingInfo.GetLength(0); x++)
                {
                    if (this.maNowPlayingInfo[x, 0] == field)
                        returnValue = this.maNowPlayingInfo[x, 1];
                }
            }

            return returnValue;
        }

        public string Get(string field)
        {
            return this.Get(field, false);
        }

        public Image GetCoverArt()
        {
            MemoryStream stream = null;
            Image thumbnail = null;
            WebClient client = new WebClient();
            Uri xbmcThumbUri = new Uri("http://" + parent.GetXbmcIp() + "/thumb.jpg");
            parent.Request("GetCurrentlyPlaying", "q:\\web\\thumb.jpg");

            try
            {
                stream = new MemoryStream(client.DownloadData(xbmcThumbUri));
                thumbnail = new Bitmap(Image.FromStream(stream));
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            finally
            {
                client.Dispose();
            }

            return thumbnail;
        }

        public string GetMediaType()
        {
            return this.Get("type", true);
        }
    }
}
