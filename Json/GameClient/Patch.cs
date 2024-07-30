using System.Text.Json.Serialization;

namespace YuukiPS_Launcher.Json.GameClient
{
    public class Original
    {
        [JsonPropertyName("file")]
        public required string File { get; set; }
        [JsonPropertyName("location")]
        public required string Location { get; set; }
        [JsonPropertyName("md5")]
        public required string MD5 { get; set; }
    }

    public class Patched
    {
        [JsonPropertyName("file")]
        public required string File { get; set; }
        [JsonPropertyName("location")]
        public required string Location { get; set; }
        [JsonPropertyName("md5")]
        public required string MD5 { get; set; }
    }

    public class Patch
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "0.0.0";
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = "Global";
        [JsonPropertyName("release")]
        public string Release { get; set; } = "Official";
        [JsonPropertyName("nosupport")]
        public string NoSupport { get; set; } = "";
        [JsonPropertyName("patched")]
        public List<Patched> Patched { get; set; } = new();
        [JsonPropertyName("original")]
        public List<Original> Original { get; set; } = new();
    }
}
