using Newtonsoft.Json;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Json
{
    public class Config
    {
        // Folder
        public static string CurrentlyPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
        public static string DataConfig { get; } = Path.Combine(CurrentlyPath, "data");
        public static string Modfolder { get; } = Path.Combine(CurrentlyPath, "mod");

        // File Config
        public static string ConfigPath { get; } = Path.Combine(DataConfig, "config.json");

        public string profile_default = "Default";
        public List<Profile> Profile { get; set; } = new List<Profile>();

        public static Config LoadConfig(string load_file = "")
        {
            // Create missing folder
            Directory.CreateDirectory(DataConfig);
            Directory.CreateDirectory(Modfolder);

            Config config = new();

            if (string.IsNullOrEmpty(load_file))
            {
                load_file = ConfigPath;
            }

            if (File.Exists(load_file))
            {
                string data = File.ReadAllText(load_file);
                try
                {
                    var tmp_configdata = JsonConvert.DeserializeObject<Config>(data);
                    if (tmp_configdata != null)
                    {
                        config = tmp_configdata;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Config", "Error load config: " + ex.Message);
                }
            }
            else
            {
                Logger.Warning("Config", "No config file found. Creating a new default profile.");
            }

            if (config.Profile.Count == 0)
            {
                config.Profile.Add(new Profile() { name = "Default" });
            }

            return config;
        }
    }
}
