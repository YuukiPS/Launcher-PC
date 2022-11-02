namespace YuukiPS_Launcher.Json.GameClient
{
    public class Data
    {
        public Game game { get; set; }
        public Plugin plugin { get; set; }
        public string web_url { get; set; }
        public object force_update { get; set; }
        public object pre_download_game { get; set; }
        public List<DeprecatedPackage> deprecated_packages { get; set; }
        public object sdk { get; set; }
        public List<DeprecatedFile> deprecated_files { get; set; }
    }

    public class DeprecatedFile
    {
        public string name { get; set; }
        public string md5 { get; set; }
    }

    public class DeprecatedPackage
    {
        public string name { get; set; }
        public string md5 { get; set; }
    }

    public class Diff
    {
        public string name { get; set; }
        public string version { get; set; } = "0.0.0";
        public string path { get; set; }
        public string size { get; set; }
        public string md5 { get; set; }
        public bool is_recommended_update { get; set; }
        public List<VoicePack> voice_packs { get; set; }
        public string package_size { get; set; }
    }

    public class Game
    {
        public Latest latest { get; set; }
        public List<Diff> diffs { get; set; }
    }

    public class Latest
    {
        public string name { get; set; }
        public string version { get; set; }
        public string path { get; set; }
        public string size { get; set; }
        public string md5 { get; set; }
        public string entry { get; set; }
        public List<VoicePack> voice_packs { get; set; }
        public string decompressed_path { get; set; }
        public List<object> segments { get; set; }
        public string package_size { get; set; }
    }

    public class Plugin
    {
        public List<Plugin> plugins { get; set; }
        public string version { get; set; }
    }

    public class Plugin2
    {
        public string name { get; set; }
        public string version { get; set; }
        public string path { get; set; }
        public string size { get; set; }
        public string md5 { get; set; }
        public string entry { get; set; }
    }

    public class Cient
    {
        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class VoicePack
    {
        public string language { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string size { get; set; }
        public string md5 { get; set; }
        public string package_size { get; set; }
    }
}
