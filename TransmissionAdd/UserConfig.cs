using System;
using Newtonsoft.Json;

namespace TransmissionAdd
{
    public class UserConfig
    {
        private static UserSettings _userSettings = null;

        public static UserSettings Settings
        {
            get
            {
                return _userSettings;
            }
        }

        private static string GetDataFolder()
        {
            return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\TransmissionAdd");
        }

        private static string GetFilepath()
        {
            return String.Format("{0}\\user.json", GetDataFolder());
        }

        public static void Load()
        {
            string filepath = GetFilepath();

            try
            {
                string jsonString = System.IO.File.ReadAllText(filepath);
                _userSettings = JsonConvert.DeserializeObject<UserSettings>(jsonString);
            }
            catch (System.IO.DirectoryNotFoundException dirEx)
            {
                string message = dirEx.Message;
                System.IO.Directory.CreateDirectory(GetDataFolder());
                System.IO.File.CreateText(GetFilepath());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("환경설정 파일을 읽는 중 오류가 발생했습니다. ({0})", filepath), ex);
            }

            if (_userSettings == null)
            {
                _userSettings = new UserSettings()
                {
                    Servers = new System.Collections.Generic.List<ServerInfo>(),
                    Domains = new System.Collections.Generic.List<DomainInfo>()
                };
            }

            string cryptKey = null;
            var user = Utility.CredentialManagementHelper.GetCredential();
            if (user == null)
            {
                cryptKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32);
                bool ret = Utility.CredentialManagementHelper.SetCredentials("TransmissionAdd", cryptKey);
                if (!ret)
                {
                    throw new ApplicationException("설정값을 가져오는데 실패했습니다.");
                }
            }
            else
            {
                cryptKey = user.Password;
            }
            _userSettings.CryptKey = cryptKey;
        }

        public static void Save()
        {
            string filepath = GetFilepath();

            try
            {
                string jsonString = JsonConvert.SerializeObject(_userSettings, Formatting.Indented);
                System.IO.File.WriteAllText(filepath, jsonString);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("환경설정 파일을 저장 중 오류가 발생했습니다. ({0})", filepath), ex);
            }
        }
    }
}
