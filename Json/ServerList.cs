using YuukiPS_Launcher.Yuuki;

namespace YuukiPS_Launcher.Json
{
    public class ServerList
    {
        public string time { get; set; } = "123";
        public List<DataServer> list { get; set; } = new List<DataServer>();
    }

    public class DataServer
    {
        public string name { get; set; } = "YuukiPS";
        public string host { get; set; } = API.WEB_LINK;
        public GameType game { get; set; } = GameType.GenshinImpact;
    }
}
