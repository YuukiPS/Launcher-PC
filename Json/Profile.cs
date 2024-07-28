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
            public string url = API.WEB_LINK;
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

            //public int patch_metode = 1; // 1=NO PATCH, 2=RSA ()this should not be necessary because it is controlled by game server
        }

        public Server ServerConfig { get; set; } = new();

        public Game GameConfig { get; set; } = new();
    }
}
