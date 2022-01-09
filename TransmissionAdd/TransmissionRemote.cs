using System;
using Transmission.API.RPC;
using System.Windows.Forms;
using System.Net;

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
            string errorMessage = null;

            if (String.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("URL을 입력해주세요.", 
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!url.StartsWith("magnet:?", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(String.Format("\"{0}\"은 올바른 Magnet Link가 아닙니다.", url),
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Utility.CredentialUser user = Utility.CredentialManagementHelper.GetCredential();
            if (user == null)
            {
                if (!UpdateTransmissionInfo(ref user, out errorMessage))
                {
                    MessageBox.Show(errorMessage,
                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            string name = null;
            int ret = AddMagnetLink(user.Url, user.Username, user.Password, url, out name, out errorMessage);
            if (ret == 0)
            {
                ToastNotificationForm toastForm = new ToastNotificationForm(name, 5);
                toastForm.ShowDialog();
                return true;
            }
            else
            {
                if (ret == 20) // 환경설정 오류 
                {
                    MessageBox.Show(errorMessage,
                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (!UpdateTransmissionInfo(ref user, out errorMessage))
                    {
                        MessageBox.Show(errorMessage,
                            "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    ret = AddMagnetLink(user.Url, user.Username, user.Password, url, out name, out errorMessage);
                    if (ret == 0)
                    {
                        ToastNotificationForm toast = new ToastNotificationForm(name, 5);
                        toast.ShowDialog();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(errorMessage,
                            "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage,
                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private bool UpdateTransmissionInfo(ref Utility.CredentialUser user, out string errorMessage)
        {
            errorMessage = null;

            using (var form = new ConfigForm())
            {
                if (user != null)
                {
                    form.TransmissionUrl = user.Url;
                    form.Username = user.Username;
                }

                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Utility.CredentialManagementHelper.SetCredentials(form.TransmissionUrl, form.Username, form.Password);
                        user = Utility.CredentialManagementHelper.GetCredential();
                        if (user != null)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Transmission 정보를 읽어오는 중 오류가 발생했습니다.";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = String.Format("Transmission 정보를 읽어오는 중 오류가 발생했습니다.\n\n{0}", ex.Message);
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Transmission 정보를 입력하지 않으셨습니다.";
                    return false;
                }
            }
        }

        /// <summary>
        /// 마그넷 링크를 등록한다.
        /// </summary>
        /// <param name="transmissionUrl">transmissoin url</param>
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
    }
}
