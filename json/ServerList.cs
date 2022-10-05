namespace YuukiPS_Launcher.Json
{
    public class ServerList
    {
        public string time { get; set; } = "123";
        public List<List> list { get; set; } = new List<List>();
    }

    public class List
    {
        public string name { get; set; } = "localhost";
        public string host { get; set; } = "localhost";
        public string game { get; set; } = "GS";
        public int port { get; set; } = 80;
        public bool https { get; set; } = false;
        public string version { get; set; } = "unknown";
    }
}
