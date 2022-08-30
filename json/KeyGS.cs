namespace YuukiPS_Launcher.json
{
    public class MetaData
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string version { get; set; }
        public string url_os { get; set; }
        public string url_cn { get; set; }
        public string md5_os { get; set; }
        public string md5_cn { get; set; }
        public string key2_os { get; set; }
        public string key2_cn { get; set; }
        public string api_os { get; set; }
        public string api_cn { get; set; }
    }

    public class Original
    {
        public MetaData MetaData { get; set; }
        public UserAssembly UserAssembly { get; set; }
    }

    public class Patched
    {
        public MetaData MetaData { get; set; }
        public UserAssembly UserAssembly { get; set; }
    }

    public class KeyGS
    {
        public Patched Patched { get; set; }
        public Original Original { get; set; }
    }

    public class UserAssembly
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string version { get; set; }
        public string url_os { get; set; }
        public string url_cn { get; set; }
        public string md5_os { get; set; }
        public string md5_cn { get; set; }
    }
}
