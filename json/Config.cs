namespace YuukiPS_Launcher.Json
{
    public class Config
    {
        public string Game_Path = "";

        public int ProxyPort = 2242;
        public string Hostdefault = "localhost";
        public bool HostHTTPS = true;

        public bool MetodeOnline = false;

        public class Extra
        {
            public bool Akebi = false;
        }
        public Extra extra { get; set; } = new();

        public List<List> server { get; set; } = new();

    }
}
