using System;
using System.Collections.Generic;

namespace TransmissionAdd
{
    public class UserSettings
    {
        public List<ServerInfo> Servers { get; set; }

        public List<DomainInfo> Domains { get; set; }
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
        public string Url { get; set; }

        public string ServerId { get; set; }
    }
}
