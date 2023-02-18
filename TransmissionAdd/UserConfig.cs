using System;
using Newtonsoft.Json;

namespace TransmissionAdd
{
    public class UserConfig
    {
        private static object syncLock = new object();
        private static UserSettings userSettings = null;

        public static UserSettings Settings
        {
            get
            {
                return userSettings;
            }
        }

        private static string GetDataFolder()
        {
            return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\TransmissionAdd");
        }

        private static string GetFilepath()
        {
            return String.Format("{0}\\data.json", GetDataFolder());
        }

        public static void Load()
        {
            string filepath = GetFilepath();

            lock (syncLock)
            {
                try
                {
                    string jsonString = System.IO.File.ReadAllText(filepath);
                    userSettings = JsonConvert.DeserializeObject<UserSettings>(jsonString);
                }
                catch (System.IO.DirectoryNotFoundException dirEx)
                {
                    System.IO.Directory.CreateDirectory(GetDataFolder());
                    System.IO.File.CreateText(GetFilepath());
                }
                catch (System.IO.FileNotFoundException fileEx)
                {
                    System.IO.File.CreateText(GetFilepath());
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(String.Format("환경설정 파일을 읽는 중 오류가 발생했습니다. ({0})", filepath), ex);
                }
            }
        }

        public static void Save()
        {
            string filepath = GetFilepath();

            lock (syncLock)
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(userSettings);
                    System.IO.File.WriteAllText(filepath, jsonString);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(String.Format("환경설정 파일을 저장 중 오류가 발생했습니다. ({0})", filepath), ex);
                }
            }
        }
    }
}
