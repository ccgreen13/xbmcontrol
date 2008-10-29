using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBMC
{
    public class XBMC_Media
    {
        XBMC_Communicator parent = null;

        public XBMC_Media(XBMC_Communicator p)
        {
            parent = p;
        }

        public string[] GetShares(string type, bool path)
        {
            string[] aMediaShares = parent.Request("GetShares(" + type + ")");

            if (aMediaShares != null)
            {
                string[] aShareNames = new string[aMediaShares.Length];
                string[] aSharePaths = new string[aMediaShares.Length];

                for (int x = 1; x < aMediaShares.Length; x++)
                {
                    string[] aTmpShare = aMediaShares[x].Split(';');

                    if (aTmpShare != null)
                    {
                        aShareNames[x] = aTmpShare[0];
                        aSharePaths[x] = aTmpShare[1];
                    }
                }

                return (path) ? aSharePaths : aShareNames;
            }
            else
                return null;
        }

        public string[] GetShares(string type)
        {
            return GetShares(type, false);
        }

        public string[] GetDirectoryContentPaths(string directory, string mask)
        {
            mask = (mask == null) ? "" : ";" + mask;

            string[] aDirectoryContent = parent.Request("GetDirectory(" + directory + mask + ")");

            if (aDirectoryContent != null)
            {
                string[] aContentPaths = new string[aDirectoryContent.Length];

                for (int x = 1; x < aDirectoryContent.Length; x++)
                    aContentPaths[x] = (aDirectoryContent[x] == "Error:Not folder" || aDirectoryContent[x] == "Error") ? null : aDirectoryContent[x];

                return aContentPaths;
            }
            else
                return null;
        }

        public string[] GetDirectoryContentPaths(string directory)
        {
            return GetDirectoryContentPaths(directory, null);
        }

        public string[] GetDirectoryContentNames(string directory, string mask)
        {
            string[] aContentPaths = this.GetDirectoryContentPaths(directory, mask);

            if (aContentPaths != null)
            {
                string[] aContentNames = new string[aContentPaths.Length];

                for (int x = 1; x < aContentPaths.Length; x++)
                {
                    if (aContentPaths[x] == null)
                        aContentNames[x] = null;
                    else
                    {
                        string[] aTmpContent = aContentPaths[x].Split('/');
                        aContentNames[x] = (aTmpContent[aTmpContent.Length - 1] == "") ? aTmpContent[aTmpContent.Length - 2] : aTmpContent[aTmpContent.Length - 1];
                    }
                }

                return aContentNames;
            }
            else
                return null;
        }

        public string[] GetDirectoryContentNames(string directory)
        {
            return GetDirectoryContentNames(directory, null);
        }
    }
}
