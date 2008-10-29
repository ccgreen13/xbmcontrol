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
using System.Web;

namespace XBMC
{
    public class XBMC_Communicator
    {
        public XBMC_Database Database = null;
        public XBMC_Playlist Playlist = null;
        public XBMC_Controls Controls = null;
        public XBMC_NowPlaying NowPlaying = null;
        public XBMC_Status Status = null;
        public XBMC_Media Media = null;

        private string configuredXbmcIp = null;
        private string xbmcUsername = null;
        private string xbmcPassword = null;
        private string apiPath = "/xbmcCmds/xbmcHttp";
        private string logFile = "log/xbmcontrol.log";

        public XBMC_Communicator()
        {
            Database = new XBMC_Database(this);
            Playlist = new XBMC_Playlist(this);
            Controls = new XBMC_Controls(this);
            NowPlaying = new XBMC_NowPlaying(this);
            Status = new XBMC_Status(this);
            Media = new XBMC_Media(this);
        }

        //START REQUEST
        public string[] Request(string command, string parameter, string ip)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            string[] pageContent = null;
            bool isQuery = (command.ToLower() == "querymusicdatabase" || command.ToLower() == "queryvideodatabase")? true : false ;

            string ipAddress = (ip == null) ? this.configuredXbmcIp : ip;
            parameter = (parameter == null) ? "" : parameter;
            command = "?command=" + Uri.EscapeDataString(command);
            command += (parameter == null || parameter == "") ? "" : "&parameter=" + Uri.EscapeDataString(parameter);
            string uri = "http://" + ipAddress + this.apiPath + command;
            
            //WriteToLog(uri);

            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Method = "GET";
                request.Timeout = XBMControl.Properties.Settings.Default.ConnectionTimeout;
                if (xbmcUsername != "" && xbmcPassword != "") request.Credentials = new NetworkCredential(xbmcUsername, xbmcPassword);
                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                if (isQuery)
                    pageContent = reader.ReadToEnd().Replace("<field>", "|").Replace("</field>", "").Replace("\n", "").Replace("<html>", "").Replace("</html>", "").Split('|');
                else
                    pageContent = reader.ReadToEnd().Replace("<li>", "|").Replace("\n", "").Replace("<html>", "").Replace("</html>", "").Split('|');
                this.Status.isConnected = (pageContent == null) ? false : true;
            }
            catch (WebException e)
            {
                WriteToLog("ERROR - " + e.Message);
            }
            finally
            {
                if (response != null) response.Close();
                if (reader != null) reader.Close();
            }

            return pageContent;
        }

        public string[] Request(string command, string parameter)
        {
            return this.Request(command, parameter, null);
        }

        public string[] Request(string command)
        {
            return this.Request(command, null, null);
        }
        //END REQUEST

        //START Helper functions
        private void WriteToLog(string message)
        {

            StreamWriter sw = null;
            string error = null;

            try
            {
                sw = new StreamWriter(logFile, true);
                sw.WriteLine(DateTime.Now + " : " + message);
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            if (sw != null)
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void SetXbmcIp(string ip)
        {
            configuredXbmcIp = ip;
        }

        public string GetXbmcIp()
        {
            return configuredXbmcIp;
        }

        public void SetCredentials(string username, string password)
        {
            xbmcUsername = username;
            xbmcPassword = password;
        }
        //END Helper functions
    }
}
