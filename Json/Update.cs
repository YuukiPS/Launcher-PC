using System.Text.Json.Serialization;

namespace YuukiPS_Launcher.Json
{
    public class Assets
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public required string BrowserDownloadUrl { get; set; }
    }

    public class Update
    {
        [JsonPropertyName("tag_name")]
        public required string TagName { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("assets")]
        public required List<Assets> Assets { get; set; }

        [JsonPropertyName("body")]
        public required string Body { get; set; }
    }
}
