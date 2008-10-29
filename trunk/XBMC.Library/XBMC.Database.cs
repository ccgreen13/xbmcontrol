using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBMC
{
    public class XBMC_Database
    {
        XBMC_Communicator parent = null;

        public XBMC_Database(XBMC_Communicator p) 
        {
            parent = p;
        }

        //START QUERY DATABASE

        public string[] GetArtist(string searchString)
        {
            string condition = (searchString == null) ? "" : " WHERE strArtist LIKE '%%" + searchString + "%%'";
            string[] aArtists = parent.Request("QueryMusicDatabase", "SELECT strArtist FROM artist" + condition);

            return aArtists;
        }

        public string[] GetArtist()
        {
            return this.GetArtist(null);
        }

        public string GetArtistId(string artist)
        {
            string[] aArtistId = parent.Request("QueryMusicDatabase", "SELECT idArtist FROM artist WHERE strArtist='" + artist + "'");
            if (aArtistId == null)
                return null;
            else
                return (aArtistId.Length > 1) ? aArtistId[1] : null;
        }

        public string GetAlbumId(string artist, string album)
        {
            string conditions = " WHERE strAlbum='" + album + "'";
            if (artist != null) conditions += " AND idArtist='" + this.GetArtistId(artist) + "'";
            string[] aArtistId = parent.Request("QueryMusicDatabase", "SELECT idAlbum FROM album" + conditions);

            if (aArtistId == null)
                return null;
            else
                return (aArtistId.Length > 1) ? aArtistId[1] : null;
        }

        public string GetAlbumId(string album)
        {
            return this.GetAlbumId(null, album);
        }

        public string GetAlbumPath(string albumId)
        {
            string[] aPathId = parent.Request("QueryMusicDatabase", "SELECT idPath FROM song WHERE idAlbum='" + albumId + "'");

            if (aPathId == null)
                return null;
            else
            {
                if (aPathId.Length > 1)
                {
                    string[] aPath = parent.Request("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + aPathId[1] + "'");
                    if (aPath == null)
                        return null;
                    else
                        return (aPath.Length > 1) ? aPath[1] : null;
                }
                else
                    return null;
            }
        }

        public string[] GetAlbumsByArtist(string artist)
        {
            string artistId = this.GetArtistId(artist);
            return (artistId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT strAlbum FROM album WHERE idArtist='" + artistId + "'");
        }

        public string[] GetTitlesByAlbum(string artist, string album)
        {
            string albumId = this.GetAlbumId(artist, album);
            return (albumId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idAlbum='" + albumId + "'");
        }

        public string[] GetTitlesByAlbum(string album)
        {
            return this.GetTitlesByAlbum(null, album);
        }

        public string[] GetTitlesByArtist(string artist)
        {
            string artistId = this.GetArtistId(artist);

            if (artistId == null)
                return null;
            else
                return parent.Request("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idArtist='" + artistId + "'");
        }

        public string[] GetPathsByArtist(string artist)
        {
            string artistId = this.GetArtistId(artist);

            if (artistId == null)
                return null;
            else
            {
                string[] aFileName = parent.Request("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idAlbum='" + artistId + "'");

                if (aFileName == null)
                    return null;
                else
                {
                    string[] aPath = new string[aFileName.Length];

                    for (int x = 1; x < aFileName.Length; x++)
                        aPath[x] = this.GetAlbumPath(artistId) + aFileName[x];

                    return aPath;
                }
            }
        }

        public string[] GetPathsByAlbum(string artist, string album)
        {
            string albumId = this.GetAlbumId(artist, album);

            if (albumId == null)
                return null;
            else
            {
                string[] aFileName = parent.Request("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idAlbum='" + albumId + "'");

                if (aFileName == null)
                    return null;
                else
                {
                    string[] aPath = new string[aFileName.Length];

                    for (int x = 1; x < aFileName.Length; x++)
                        aPath[x] = this.GetAlbumPath(albumId) + aFileName[x];

                    return aPath;
                }
            }
        }

        public string[] GetPathsByAlbum(string album)
        {
            return this.GetPathsByAlbum(null, album);
        }

        //END QUERY DATABASE
    }
}
