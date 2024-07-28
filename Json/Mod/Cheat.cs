using System.Text.Json.Serialization;

namespace YuukiPS_Launcher.Json.Mod
{
    public class Archive
    {
        [JsonPropertyName("url")]
        public required string Url { get; set; }
        [JsonPropertyName("md5")]
        public required string Md5 { get; set; }
        [JsonPropertyName("support")]
        public required string Support { get; set; }
        [JsonPropertyName("channel")]
        public required List<int> Channel { get; set; }
        public required Config Config { get; set; }
        [JsonPropertyName("comment")]
        public required string Comment { get; set; }
    }

    public class Config
    {
        [JsonPropertyName("launcher")]
        public required string Launcher { get; set; }
        [JsonPropertyName("save")]
        public required string Save { get; set; }
        [JsonPropertyName("format")]
        public int Format { get; set; } // 1-cfg,2-json
    }

    public class Cheat
    {
        [JsonPropertyName("nama")]
        public required string Nama { get; set; }
        [JsonPropertyName("game")]
        public int Game { get; set; }
        [JsonPropertyName("archives")]
        public required List<Archive> Archives { get; set; }
    }

}
