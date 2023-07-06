namespace YuukiPS_Launcher.Json.Mod
{
    public class Archive
    {
        public string url { get; set; }
        public string md5 { get; set; }
        public string version { get; set; }
        public string support { get; set; }
        public string comment { get; set; }
    }

    public class Cheat
    {
        public string nama { get; set; }
        public string link { get; set; }
        public string comment { get; set; }
        public int game { get; set; }
        public List<Archive> archives { get; set; }
    }

}
