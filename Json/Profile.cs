using YuukiPS_Launcher.Yuuki;

namespace YuukiPS_Launcher.Json
{
    public class Profile
    {
        public string name = "Default";

        public override string ToString()
        {
            return name;
        }

        public class Server
        {
            public string url = API.WebLink;
            public Proxy proxy = new();
            public class Proxy
            {
                public int port = 2242; // proxy port
                public bool enable = true;
            }
        }

        public class Game
        {
            public string path = ""; // if blank set auto
            public GameType type = GameType.GenshinImpact;
            public bool wipeLogin = false;

            public Extra extra { get; set; } = new();

            public class Extra
            {
                public bool Akebi = false;
                public bool RPC = false;
            }
        }

        public Server ServerConfig { get; set; } = new();

        public Game GameConfig { get; set; } = new();
    }
}
