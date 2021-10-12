using System;
using CredentialManagement;
using System.Collections.Generic;
using System.Linq;

namespace TransmissionAdd.Utility
{
    public class CredentialManagementHelper
    {
        const string _target = "TransmissionAdd";

        private CredentialManagementHelper()
        {

        }

        public static CredentialUser GetCredential()
        {
            var cm = new Credential
            {
                Target = _target
            };
            if (!cm.Load())
            {
                return null;
            }

            string url;
            string username;
            string urlAndUsername = cm.Username;
            int pos = urlAndUsername.IndexOf("@", 0);
            if (urlAndUsername.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                url = String.Format("http://{0}", urlAndUsername.Substring(pos + 1));
                username = urlAndUsername.Substring(7, pos - 7);
            }
            else
            {
                url = String.Format("https://{0}", urlAndUsername.Substring(pos + 1));
                username = urlAndUsername.Substring(8, pos - 8);
            }

            return new CredentialUser()
            {
                Url = url,
                Username = username,
                Password = cm.Password
            };
        }

        public static bool SetCredentials(string url, string username, string password, PersistanceType persistenceType = PersistanceType.LocalComputer)
        {
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return false;

            url = url.TrimEnd('/');
            if (!url.EndsWith("/rpc", StringComparison.OrdinalIgnoreCase))
            {
                List<string> urlList = url.Split('/').ToList();
                if (urlList[urlList.Count - 1].Equals("web", StringComparison.OrdinalIgnoreCase)
                    && urlList[urlList.Count - 2].Equals("transmission", StringComparison.OrdinalIgnoreCase))
                {
                    urlList[urlList.Count - 1] = "rpc";
                }
                else
                {
                    if (!urlList[urlList.Count - 1].Equals("rpc", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!urlList[urlList.Count - 2].Equals("transmission", StringComparison.OrdinalIgnoreCase))
                            urlList.Add("transmission");
                        urlList.Add("rpc");
                    }
                }
                url = String.Join('/', urlList);
            }

            username = url.ToLower().StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                ? String.Format("http://{0}@{1}", username, url.Substring(7))
                : String.Format("https://{0}@{1}", username, url.Substring(8));

            return new Credential()
            {
                Target = _target,
                Username = username,
                Password = password,
                PersistanceType = persistenceType
            }.Save();
        }

        public static bool RemoveCredentials(string target)
        {
            return new Credential { Target = target }.Delete();
        }
    }

    public class CredentialUser
    {
        public string Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
