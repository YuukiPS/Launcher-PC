using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuukiPS_Launcher.Json.Mod
{
    public class Cn
    {
        public string? url { get; set; }
        public string? md5 { get; set; }
    }

    public class Os
    {
        public string? url { get; set; }
        public string? md5 { get; set; }
    }

    public class Akebi
    {
        public string? description { get; set; }
        public string? version { get; set; }
        public Package? package { get; set; }
    }

    public class Package
    {
        public Os? os { get; set; }
        public Cn? cn { get; set; }
    }

}
