namespace YuukiPS_Launcher.Json.GameClient
{
    public class Cn
    {
        public string userassembly { get; set; }
        public string metadata { get; set; }
    }

    public class KeyFind
    {
        public string cn { get; set; }
        public string os { get; set; }
    }

    public class Md5Check
    {
        public Os os { get; set; }
        public Cn cn { get; set; }
    }

    public class Md5Vaild
    {
        public string os { get; set; }
        public string cn { get; set; }
    }

    public class Original
    {
        public string resources { get; set; }
        public KeyFind key_find { get; set; }
        public Md5Check md5_check { get; set; }
    }

    public class Os
    {
        public string userassembly { get; set; }
        public string metadata { get; set; }
    }

    public class Patched
    {
        public string metode { get; set; }
        public string resources { get; set; }
        public string key_patch { get; set; }
        public Md5Vaild md5_vaild { get; set; }
    }

    public class Patch
    {
        public string version { get; set; } = "0.0.0";
        public string channel { get; set; } = "Global";
        public string release { get; set; } = "Official";
        public Patched? patched { get; set; }
        public Original? original { get; set; }
    }
}
