using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TransmissionAdd
{
    public class UserSettings
    {
        [JsonIgnore]
        public string CryptKey { get; set; }

        public List<ServerInfo> Servers { get; set; }

        public List<DomainInfo> Domains { get; set; }

        public string LastServerId { get; set; }
    }

    public class ServerInfo
    {
        public string ServerId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class DomainInfo
    {
        public string Domain { get; set; }

        public string ServerId { get; set; }
    }
}
