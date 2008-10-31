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
using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_Database
    {
        XBMC_Communicator parent = null;

        public XBMC_Database(XBMC_Communicator p) 
        {
            parent = p;
        }

        public string[] GetArtists(string searchString)
        {
            string condition = (searchString == null) ? "" : " WHERE strArtist LIKE '%%" + searchString + "%%'";
            string[] aArtists = parent.Request("QueryMusicDatabase", "SELECT strArtist FROM artist" + condition + " ORDER BY strArtist");

            return aArtists;
        }

        public string[] GetArtists()
        {
            return this.GetArtists(null);
        }

        public string[] GetArtistIds(string searchString)
        {
            string condition = (searchString == null) ? "" : " WHERE strArtist LIKE '%%" + searchString + "%%'";
            string[] aArtistIds = parent.Request("QueryMusicDatabase", "SELECT idArtist FROM artist" + condition + " ORDER BY strArtist");

            return aArtistIds;
        }

        public string[] GetArtistIds()
        {
            return this.GetArtistIds(null);
        }

        public string[] GetAlbums(string searchString)
        {
            string condition = (searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + searchString + "%%'";
            string[] aAlbums = parent.Request("QueryMusicDatabase", "SELECT strAlbum FROM album" + condition);

            return aAlbums;
        }

        public string[] GetAlbums()
        {
            return this.GetAlbums(null);
        }

        public string[] GetAlbumIds(string searchString)
        {
            string condition = (searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + searchString + "%%'";
            string[] aAlbumIds = parent.Request("QueryMusicDatabase", "SELECT idAlbum FROM album" + condition);

            return aAlbumIds;
        }

        public string[] GetAlbumIds()
        {
            return this.GetAlbumIds(null);
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

        public string GetPathById(string pathId)
        {
            string path = null;
            string[] aPath = parent.Request("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + pathId + "'");

            if (aPath != null)
                if (aPath.Length > 1) path = aPath[1];

            return path;
        }

        public string GetAlbumPath(string albumId)
        {
            string albumPath = null;
            string[] aPathId = parent.Request("QueryMusicDatabase", "SELECT idPath FROM song WHERE idAlbum='" + albumId + "'");

            if (aPathId != null)
                if (aPathId.Length > 1) albumPath = this.GetPathById(aPathId[1]);

            return albumPath;
        }

        public string GetSongPath(string songId)
        {
            string songPath = null;
            string[] aPathId = parent.Request("QueryMusicDatabase", "SELECT idPath FROM song WHERE idSong='" + songId + "'");

            if (aPathId != null)
                if (aPathId.Length > 1) songPath = this.GetPathById(aPathId[1]);

            return songPath;
        }

        public string[] GetAlbumsByArtist(string artist)
        {
            string artistId = this.GetArtistId(artist);
            return (artistId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT strAlbum FROM album WHERE idArtist='" + artistId + "' ORDER BY strAlbum");
        }

        public string[] GetAlbumsByArtistId(string artistId)
        {
            return (artistId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT strAlbum FROM album WHERE idArtist='" + artistId + "' ORDER BY strAlbum");
        }

        public string[] GetAlbumIdsByArtistId(string artistId)
        {
            return (artistId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT idAlbum FROM album WHERE idArtist='" + artistId + "' ORDER BY strAlbum");
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

        public string[] GetTitlesByAlbumId(string albumId)
        {
            return (albumId == null) ? null : parent.Request("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idAlbum='" + albumId + "'");
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

        public string[] GetPathsByAlbumId(string albumId)
        {
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

        public string[] GetTitlesByArtist(string artist)
        {
            string artistId = this.GetArtistId(artist);

            if (artistId == null)
                return null;
            else
                return parent.Request("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idArtist='" + artistId + "'");
        }

        public string[] GetTitlesByArtistId(string artistId)
        {
            return (artistId == null)? null : parent.Request("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idArtist='" + artistId + "'");
        }

        public string[] GetPathsByArtist(string artist)
        {
            string[] aPaths = null;
            string[] aSongPathIds = null;
            string[] aFileNames = null;
            string artistId = this.GetArtistId(artist);

            if (artistId != null)
            {
                aSongPathIds = parent.Request("QueryMusicDatabase", "SELECT idPath FROM song WHERE idArtist='" + artistId + "'");
                aFileNames = parent.Request("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idArtist='" + artistId + "'");

                if (aFileNames != null)
                {
                    if (aFileNames.Length > 1)
                    {
                        aPaths = new string[aFileNames.Length];
                        for (int x = 1; x < aFileNames.Length; x++)
                            aPaths[x] = this.GetPathById(aSongPathIds[x]) + aFileNames[x];
                    }
                }
            }

            return aPaths;
        }

        public string[] GetPathsByArtistId(string artistId)
        {
            string[] aPaths = null;
            string[] aSongPathIds = null;
            string[] aFileNames = null;

            if (artistId != null)
            {
                aSongPathIds = parent.Request("QueryMusicDatabase", "SELECT idPath FROM song WHERE idArtist='" + artistId + "'");
                aFileNames = parent.Request("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idArtist='" + artistId + "'");

                if (aFileNames != null)
                {
                    if (aFileNames.Length > 1)
                    {
                        aPaths = new string[aFileNames.Length];
                        for (int x = 1; x < aFileNames.Length; x++)
                            aPaths[x] = this.GetPathById(aSongPathIds[x]) + aFileNames[x];
                    }
                }
            }

            return aPaths;
        }
    }
}
