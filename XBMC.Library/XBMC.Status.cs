using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBMC
{
    public class XBMC_Status
    {
        XBMC_Communicator parent = null;

        //XBMC Properties
        internal bool isConnected = false;
        private bool isPlaying = false;
        private bool isNotPlaying = true;
        private bool isPlayingLastFm = false;
        private bool isPaused = false;
        private bool isMuted = false;
        private int volume = 0;
        private int progress = 1;
        private string mediaNowPlaying = null;
        private bool newMediaPlaying = true;

        public XBMC_Status(XBMC_Communicator p)
        {
            parent = p;
        }

        //START Collect all info with one function to improve performance
        public void Refresh()
        {
            if (isConnected)
            {
                if (mediaNowPlaying != parent.NowPlaying.Get("filename", true) || mediaNowPlaying == null)
                {
                    mediaNowPlaying = parent.NowPlaying.Get("filename");
                    newMediaPlaying = true;
                }
                else
                    newMediaPlaying = false;

                isPlaying = (parent.NowPlaying.Get("playstatus", true) == "Playing") ? true : false;
                isPaused = (parent.NowPlaying.Get("playstatus", true) == "Paused") ? true : false;
                isNotPlaying = (mediaNowPlaying == "[Nothing Playing]" || mediaNowPlaying == null) ? true : false;

                if (mediaNowPlaying == null || isNotPlaying)
                    isPlayingLastFm = false;
                else
                    isPlayingLastFm = (mediaNowPlaying.Substring(0, 6) == "lastfm") ? true : false;

                string[] aVolume = parent.Request("GetVolume");
                string[] aProgress = parent.Request("GetPercentage");

                if (aVolume == null)
                    volume = 0;
                else if (aVolume.Length > 1)
                    volume = (aVolume[1] == null || aVolume[1] == "Error") ? 0 : Convert.ToInt32(aVolume[1]);
                else
                    volume = 0;

                if (aProgress == null)
                    progress = 1;
                else if (aProgress.Length > 1)
                    progress = (aProgress[1] == null || aProgress[1] == "Error" || aProgress[1] == "0" || Convert.ToInt32(aProgress[1]) > 99) ? 1 : Convert.ToInt32(aProgress[1]);
                else
                    progress = 1;

                isMuted = (volume == 0) ? true : false;
            }
        }

        public bool IsConnected()
        {
            string ip = parent.GetXbmcIp();
            if (ip != null && ip != "")
                parent.Request("SetResponseFormat", null, ip);
            else
                isConnected = false;

            return isConnected;
        }

        public bool WebServerEnabled()
        {
            string[] webserverEnabled = parent.Request("WebServerStatus()");

            if (webserverEnabled == null)
                return false;
            else
                return (webserverEnabled[1] == "On") ? true : false;
        }

        public bool IsNewMediaPlaying()
        {
            return newMediaPlaying;
        }

        public bool IsPlaying(string lastfm)
        {
            return (lastfm != null) ? isPlayingLastFm : isPlaying;
        }

        public bool IsPlaying()
        {
            return this.IsPlaying(null);
        }

        public bool IsNotPlaying()
        {
            return isNotPlaying;
        }

        public bool IsPaused()
        {
            return isPaused;
        }

        public bool IsMuted()
        {
            return isMuted;
        }

        public int GetVolume()
        {
            return volume;
        }

        public int GetProgress()
        {
            return progress;
        }

        public bool LastFmEnabled()
        {
            string[] aLastFmUsername = parent.Request("GetGuiSetting(3;lastfm.username)");
            string[] aLastFmPassword = parent.Request("GetGuiSetting(3;lastfm.password)");

            if (aLastFmUsername == null || aLastFmPassword == null)
                return false;
            else
                return (aLastFmUsername[1] == "" || aLastFmPassword[1] == "") ? false : true;
        }

        public bool RepeatEnabled()
        {
            string[] aRepeatEnabled = parent.Request("GetGuiSetting(1;musicfiles.repeat)");
            if (aRepeatEnabled == null)
                return false;
            else
                return (aRepeatEnabled[1] == "False") ? false : true;
        }
    }
}
