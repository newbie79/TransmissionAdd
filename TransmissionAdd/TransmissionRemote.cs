using System;
using Transmission.API.RPC;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TransmissionAdd
{
    public class TransmissionRemote
    {
        /// <summary>
        /// 토렌트 링크를 등록한다.
        /// </summary>
        /// <param name="url">토렌트 URL</param>
        /// <returns>결과코드 (0:성공, 10:URL 누락, 11:이미 등록됨, 20:계정 미등록)</returns>
        public bool Add(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("URL을 입력해주세요.", 
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            char torrentType = GetTorrentType(url);

            if (torrentType != 'M' && torrentType != 'T')
            {
                MessageBox.Show(String.Format("\"{0}\"은 올바른 Magnet Link나 Torrent file이 아닙니다.", url),
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            UserConfig.Load();

            return Add_Inner(torrentType, url);
        }

        /// <summary>
        /// torrent 종류를 반환합니다.
        /// </summary>
        /// <param name="url">Magnet link 또는 Torrent file</param>
        /// <returns>결과코드 (M:Magnet link, T:Torrent file, U:알 수 없음)</returns>
        private char GetTorrentType(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
                return 'U';
            else if (url.StartsWith("magnet:?", StringComparison.OrdinalIgnoreCase))
                return 'M';
            else if (System.IO.File.Exists(url) && url.EndsWith(".torrent", StringComparison.OrdinalIgnoreCase))
                return 'T';
            else
                return 'U';
        }

        /// <summary>
        /// 토렌트 링크를 등록한다.
        /// </summary>
        /// <param name="torrentType">토렌트 종류 (M:Magnet link, T:torrent file)</param>
        /// <param name="url">토렌트 URL</param>
        /// <returns>결과코드 (0:성공, 10:URL 누락, 11:이미 등록됨, 20:계정 미등록)</returns>
        private bool Add_Inner(char torrentType, string url, int retry = 1)
        { 
            if (retry == 0 || UserConfig.Settings == null || UserConfig.Settings.Servers == null || UserConfig.Settings.Servers.Count == 0)
            {
                using (var form = new ConfigForm())
                {
                    form.ShowServerConfigForm = true;
                    form.ShowDialog();
                }
            }

            if (UserConfig.Settings == null || UserConfig.Settings.Servers == null || UserConfig.Settings.Servers.Count == 0)
            {
                MessageBox.Show("등록된 Transmission 사이트가 없습니다.",
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ServerInfo serverInfo = null;
            if (UserConfig.Settings.Servers.Count == 1)
            {
                serverInfo = UserConfig.Settings.Servers[0];
            }
            else
            {
                using (var form = new ServerSelectForm())
                {
                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        serverInfo = form.ServerInfo;
                    }
                    else
                    {
                        MessageBox.Show("작업을 취소하셨습니다.",
                            "안내", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }

            UserConfig.Settings.LastServerId = serverInfo.ServerId;
            UserConfig.Save();

            string transmissionUrl = String.Format("{0}/rpc", serverInfo.Url);
            string password = Utility.Crypto.Decrypt(serverInfo.Password, UserConfig.Settings.CryptKey);
            string name = null;
            string errorMessage = null;

            int ret = 99;
            if (torrentType == 'M')
            {
                ret = AddMagnetLink(transmissionUrl, serverInfo.Username, password, url, out name, out errorMessage);
            }
            else if (torrentType == 'T')
            {
                ret = AddFile(transmissionUrl, serverInfo.Username, password, url, out name, out errorMessage);
            }
            else
            {
                throw new ArgumentException("'{0}'은 올바른 torrent type이 아닙니다.", torrentType.ToString());
            }

            if (ret == 0)
            {
                ToastNotificationForm toastForm = new ToastNotificationForm(name, 5);
                toastForm.ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show(errorMessage,
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (ret == 20) // 설정 오류 
                {
                    if (retry-- > 0)
                    {
                        return Add_Inner(torrentType, url, retry);
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 마그넷 링크를 등록한다.
        /// </summary>
        /// <param name="transmissionUrl">transmission url</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="magnetLink">magnet link</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>결과코드 (0:성공, 11:이미 등록된 마그넷 링크, 20:환경설정 오류, 99:오류 )</returns>
        private int AddMagnetLink(string transmissionUrl, string username, string password, string magnetLink, out string name, out string errorMessage)
        {
            name = null;
            errorMessage = null;

            try
            {
                var client = new Client(transmissionUrl, string.Empty, username, password);

                string[] fields = { "magnetLink" };
                var transmissionList = client.TorrentGet(fields);
                foreach (var item in transmissionList.Torrents)
                {
                    if (!String.IsNullOrWhiteSpace(item.MagnetLink) && item.MagnetLink.Equals(magnetLink))
                    {
                        errorMessage = "이미 등록된 Magnet Link입니다.";
                        return 11;
                    }
                }

                var info = client.TorrentAdd(new Transmission.API.RPC.Entity.NewTorrent()
                {
                    Filename = magnetLink
                });

                name = info.Name;

                return 0;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    errorMessage = webEx.Message;
                    return 20;
                }
                else
                {
                    errorMessage = webEx.Message;
                    return 99;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 99;
            }
        }

        /// <summary>
        /// torrent 파일을 등록한다.
        /// </summary>
        /// <param name="transmissionUrl">transmission url</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="filepath">torrent filepath</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>결과코드 (0:성공, 20:환경설정 오류, 99:오류 )</returns>
        private int AddFile(string transmissionUrl, string username, string password, string filepath, out string name, out string errorMessage)
        {
            name = null;
            errorMessage = null;

            try
            {
                if (!System.IO.File.Exists(filepath))
                {
                    throw new ApplicationException(String.Format("{0} 파일이 존재하지 않습니다.", filepath));
                }

                string filename = System.IO.Path.GetFileName(filepath);

                var client = new Client(transmissionUrl, string.Empty, username, password);

                var info = client.TorrentAdd(new Transmission.API.RPC.Entity.NewTorrent()
                {
                    Metainfo = ToBase64String(filepath)
                });

                name = info.Name;

                return 0;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    errorMessage = webEx.Message;
                    return 20;
                }
                else
                {
                    errorMessage = webEx.Message;
                    return 99;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 99;
            }
        }

        private string ToBase64String(string filepath)
        {
            using (FileStream reader = new FileStream(filepath, FileMode.Open))
            {
                byte[] buffer = new byte[reader.Length];
                reader.Read(buffer, 0, (int)reader.Length);
                return Convert.ToBase64String(buffer);
            }
        }
    }
}
