using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBMC
{
    public class XBMC_Playlist
    {
        XBMC_Communicator parent = null;
        private string[] aCurrentPlaylist = null;

        public XBMC_Playlist(XBMC_Communicator p)
        {
            parent = p;
        }

        //START Playlist controls
        public string[] Get(bool refresh)
        {
            if (refresh)
            {
                string[] aPlaylistTemp = parent.Request("GetPlaylistContents(GetCurrentPlaylist)");

                if (aPlaylistTemp != null)
                {
                    aCurrentPlaylist = new string[aPlaylistTemp.Length];
                    for (int x = 1; x < aPlaylistTemp.Length; x++)
                    {
                        int i = aPlaylistTemp[x].LastIndexOf(".");
                        if (i > 1)
                        {
                            string extension = aPlaylistTemp[x].Substring(i, aPlaylistTemp[x].Length - i);
                            string[] aPlaylistEntry = aPlaylistTemp[x].Split('/');
                            string playlistEntry = aPlaylistEntry[aPlaylistEntry.Length - 1].Replace(extension, "");
                            aCurrentPlaylist[x] = playlistEntry;
                        }
                        else
                            aCurrentPlaylist[x] = "";
                    }
                }
            }

            return aCurrentPlaylist;
        }

        public void PlaySong(int position)
        {
            parent.Request("SetPlaylistSong(" + position.ToString() + ")");
        }

        public void Remove(int position)
        {
            parent.Request("RemoveFromPlaylist(" + position.ToString() + ")");
        }

        public string GetCurrentIdentifier()
        {
            string[] curPlaylist = parent.Request("GetCurrentPlaylist()");
            return (curPlaylist == null) ? null : curPlaylist[1];
        }

        public void Clear()
        {
            parent.Request("ClearPlayList()");
        }

        public void AddDirectoryContent(string folderPath, string mask, bool recursive)
        {
            string p = "";
            string m = "";
            string r = "";

            if (mask != null)
            {
                m = ";[" + mask + "]";
                p = ";0";
                r = (recursive) ? ";1" : ";0";
            }

            parent.Request("AddToPlayList(" + folderPath + p + m + r + ")");
        }

        public void AddDirectoryContent(string folderPath, string mask)
        {
            this.AddDirectoryContent(folderPath, mask, false);
        }

        public void AddFilesToPlaylist(string filePath)
        {
            this.AddDirectoryContent(filePath, null);
        }

        public void SetSong(int position)
        {
            parent.Request("SetPlaylistSong(" + position.ToString() + ")");
        }

        public void Set(string type)
        {
            string playlistType = (type == "video") ? "1" : "0";
            parent.Request("SetCurrentPlaylist(" + playlistType + ")");
        }
        //END Playlist controls
    }
}
