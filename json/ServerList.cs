namespace YuukiPS_Launcher.json
{
    public class ServerList
    {
        public string time { get; set; }
        public List<List> list { get; set; }
    }

    public class List
    {
        public string name { get; set; }
        public string host { get; set; }
        public string game { get; set; }
        public int port { get; set; }
        public string version { get; set; }
    }
}
