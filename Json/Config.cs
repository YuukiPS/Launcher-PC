using System.Text.Json;

namespace YuukiPS_Launcher.Json
{
    public class Config
    {
        // Folder
        public static string CurrentlyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
        public static string DataConfig = Path.Combine(CurrentlyPath, "data");
        public static string Modfolder = Path.Combine(CurrentlyPath, "mod");

        // File Config
        public static string ConfigPath = Path.Combine(DataConfig, "config.json");

        public string profile_default = "Default";
        public List<Profile> profile { get; set; } = new List<Profile>();

        public static Config LoadConfig(string load_file = "")
        {
            // Create missing folder
            Directory.CreateDirectory(Config.DataConfig);
            Directory.CreateDirectory(Config.Modfolder);

            Config config = new Config();

            if (string.IsNullOrEmpty(load_file))
            {
                load_file = ConfigPath;
            }

            if (File.Exists(load_file))
            {
                string data = File.ReadAllText(load_file);
                try
                {
                    var tmp_configdata = JsonSerializer.Deserialize<Config>(data);
                    if (tmp_configdata != null)
                    {
                        config = tmp_configdata;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error load config: " + ex.Message + ", so make new profile");
                }
            }
            else
            {
                Console.WriteLine("No config file found, so make new profile");
            }

            if (config.profile.Count == 0)
            {
                config.profile.Add(new Profile() { name = "Default" });
            }

            return config;
        }
    }
}
