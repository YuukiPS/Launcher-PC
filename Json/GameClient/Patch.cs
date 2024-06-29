namespace YuukiPS_Launcher.Json.GameClient
{
    public class Original
    {
        public string file { get; set; }
        public string location { get; set; }
        public string md5 { get; set; }
    }

    public class Patched
    {
        public string file { get; set; }
        public string location { get; set; }
        public string md5 { get; set; }
    }

    public class Patch
    {
        public string version { get; set; } = "0.0.0";
        public string channel { get; set; } = "Global";
        public string release { get; set; } = "Official";
        public string method { get; set; } = "copy"; // rare use
        public string nosupport { get; set; } = "";
        public List<Patched> patched { get; set; }
        public List<Original> original { get; set; }
    }
}
