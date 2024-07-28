using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Yuuki;
using YuukiPS_Launcher.Utils;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace YuukiPS_Launcher
{
    public partial class Main : Form
    {
        // Main Function
        private Proxy? proxy;
        private Process? progress;

        Config ConfigData = new();
        Profile DefaultProfile = new();

        // Stats default
        public string WatchFile = "";
        public string WatchCheat = "melon123";
        public string HostName = "YuukiPS"; // host name
        public bool IsGameRun = false;
        public bool DoneCheck = true;

        // Config basic game
        public string VersionGame = "";
        public int GameChannel = 0;
        public string PathfileGame = "";

        // Extra
        readonly Extra.Discord discord = new();

        // Game
        public Game.Genshin.Settings? settings_genshin = null;

        // Patch
        Patch? get_patch = null;

        Logger logger = new();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            OperatingSystem os = Environment.OSVersion;

            string logFileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            string logsFolderPath = Path.Combine(Application.StartupPath, "logs");
            string logFilePath = Path.Combine(logsFolderPath, logFileName);

            Directory.CreateDirectory(logsFolderPath);

            Logger.InitLogging($"Platform: {os.Platform}\nPlatform Version: {os.Version}\nService pack: {os.ServicePack}\n\n", logFilePath);

            Logger.Info("Boot", "Loading....");

            // Before starting make sure proxy is turned off
            CheckProxy(true);

            // Check Update
            CheckUpdate();

            LoadConfig("Boot"); // if found config

            // Load Profile by profile_default
            LoadProfile(ConfigData.profile_default);

            // Extra
            if (Enable_RPC.Checked)
            {
                Logger.Info("Boot", "Discord RPC enabled");
                discord.Ready();
            }
            else
            {
                Logger.Info("Boot", "Discord RPC disabled");
            }
        }

        private void btload_Click(object? sender, EventArgs e)
        {
            var get_select_profile = GetProfileServer.Text;
            LoadProfile(get_select_profile);
        }

        private void Set_LA_Save_Click(object? sender, EventArgs e)
        {
            var get_select_profile = GetProfileServer.Text;
            SaveProfile(get_select_profile);
        }

        private void GetProfileServer_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var get_select_profile = GetProfileServer.Text;
            Logger.Info("Profiles", "GetProfileServer_SelectedIndexChanged " + get_select_profile);
            LoadProfile(get_select_profile);
        }

        private void GetTypeGame_SelectedIndexChanged(object? sender, EventArgs e)
        {
            DefaultProfile.GameConfig.type = (GameType)GetTypeGame.SelectedItem;
        }

        public void LoadConfig(string LoadBy)
        {
            ConfigData = Json.Config.LoadConfig();

            Logger.Info("Config", $"Configuration loaded by: {LoadBy}");

            // Unsubscribe from SelectedIndexChanged event
            GetProfileServer.SelectedIndexChanged -= GetProfileServer_SelectedIndexChanged;

            // Profile
            GetProfileServer.DisplayMember = "name";
            GetProfileServer.DataSource = ConfigData.Profile;

            // GameType
            GetTypeGame.DataSource = Enum.GetValues(typeof(GameType));

            // Find the index of the desired profile
            for (int i = 0; i < GetProfileServer.Items.Count; i++)
            {
                Profile profile = (Profile)GetProfileServer.Items[i];
                if (profile.name == ConfigData.profile_default)
                {
                    Logger.Info("Profiles", $"Setting selected profile: '{ConfigData.profile_default}' at index {i}");
                    GetProfileServer.SelectedIndex = i;
                    break;
                }
            }

            // Subscribe back to SelectedIndexChanged event
            GetProfileServer.SelectedIndexChanged += GetProfileServer_SelectedIndexChanged;
        }

        public void LoadProfile(string LoadProfile = "")
        {

            if (string.IsNullOrEmpty(LoadProfile))
            {
                Logger.Info("Profiles", "No profile");
                return;
            }

            Logger.Info("Profiles", "Profile: " + LoadProfile);

            try
            {
                var tmp_profile = ConfigData.Profile.Find(p => p.name == LoadProfile);
                if (tmp_profile != null)
                {
                    DefaultProfile = tmp_profile;
                }
                else
                {
                    // use default data
                }
                Logger.Info("Profiles", "Server: " + DefaultProfile.ServerConfig.url);
            }
            catch (Exception e)
            {
                Logger.Error("Profiles", "Profile error (" + e.Message + "), use default data");
            }

            // Data Set

            // Game
            Set_LA_GameFolder.Text = DefaultProfile.GameConfig.path;
            GetTypeGame.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(GameType)), DefaultProfile.GameConfig.type);

            // Server
            CheckProxyEnable.Checked = DefaultProfile.ServerConfig.proxy.enable;
            GetServerHost.Text = DefaultProfile.ServerConfig.url;
            // Extra
            Extra_Cheat.Checked = DefaultProfile.GameConfig.extra.Akebi;
            Enable_RPC.Checked = DefaultProfile.GameConfig.extra.RPC;

            // Get Data Game
            if (!CheckVersionGame(DefaultProfile.GameConfig.type))
            {
                var message = "No game folder detected. Please manually input the game folder before playing.";
                Logger.Warning("Game", message);
                MessageBox.Show(message, "Game Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void SaveProfile(string NameSave = "Default")
        {
            try
            {
                var tmp_profile = new Profile();

                // Game
                tmp_profile.GameConfig.path = Set_LA_GameFolder.Text;
                tmp_profile.GameConfig.type = (GameType)GetTypeGame.SelectedItem;
                tmp_profile.GameConfig.wipeLogin = Enable_WipeLoginCache.Checked;

                // Server
                tmp_profile.ServerConfig.url = GetServerHost.Text;
                int myInt;
                bool isValid = int.TryParse(GetProxyPort.Text, out myInt);
                if (isValid)
                {
                    tmp_profile.ServerConfig.proxy.port = myInt;
                }

                // Extra
                tmp_profile.GameConfig.extra.Akebi = Extra_Cheat.Checked;
                tmp_profile.GameConfig.extra.RPC = Enable_RPC.Checked;

                // Nama Profile
                tmp_profile.name = NameSave;

                try
                {
                    int indexToUpdate = ConfigData.Profile.FindIndex(profile => profile.name == NameSave);
                    if (indexToUpdate != -1)
                    {
                        Logger.Info("Profiles", $"Updating existing profile: {NameSave}");
                        ConfigData.Profile[indexToUpdate] = tmp_profile;
                    }
                    else
                    {
                        Logger.Info("Profiles", $"Creating new profile: {NameSave}");
                        ConfigData.Profile.Add(tmp_profile);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Profiles", $"Failed to save profile '{NameSave}'. Error: {ex.Message}. Reinitializing configuration.");
                    ConfigData = new Json.Config() { Profile = new List<Profile>() { tmp_profile } };
                }


                ConfigData.profile_default = NameSave;

                File.WriteAllText(Json.Config.ConfigPath, JsonConvert.SerializeObject(ConfigData));

                Logger.Info("Config", "Configuration saved successfully.");

                LoadConfig("SaveProfile");
            }
            catch (Exception ex)
            {
                Logger.Error("Profiles", $"Failed to save profile: {ex.Message}. Reverting to default configuration.");
            }
        }

        private void btStartOfficialServer_Click(object sender, EventArgs e)
        {
            GetServerHost.Text = "official";
            CheckProxyEnable.Checked = false;
            DoStart();
        }

        private void btStartYuukiServer_Click(object sender, EventArgs e)
        {
            GetServerHost.Text = API.WEB_LINK;
            CheckProxyEnable.Checked = true;
            DoStart();
        }

        private void btStartNormal_Click(object sender, EventArgs e)
        {
            DoStart();
        }

        public void DoStart()
        {
            // Jika game berjalan...
            if (IsGameRun)
            {
                AllStop();
                return;
            }

            // Setup
            bool isCheat = Extra_Cheat.Checked;
            bool isProxyNeed = CheckProxyEnable.Checked;
            bool isSendLog = Enable_SendLog.Checked;

            GameType selectedGame = (GameType)GetTypeGame.SelectedItem;

            // Get Host
            string set_server_host = GetServerHost.Text;
            if (string.IsNullOrEmpty(set_server_host))
            {
                MessageBox.Show("Please select a server first, you can click on one in server list");
                return;
            }

            // Get Proxy
            int set_proxy_port = int.Parse(GetProxyPort.Text);

            // Get Game
            var cst_gamefile = PathfileGame;
            if (String.IsNullOrEmpty(cst_gamefile))
            {
                MessageBox.Show("No game file config found");
                return;
            }
            if (!File.Exists(cst_gamefile))
            {
                MessageBox.Show("Please find game install folder!");
                return;
            }

            bool patch = true;

            // Check progress
            if (!IsGameRun)
            {
                // if game is not running
                if (progress != null)
                {
                    Logger.Info("Game", "progress tes");
                }

                // if server is official 
                if (set_server_host == "official")
                {
                    patch = false;
                }
                else
                {
                    if (get_patch != null && get_patch.NoSupport != "")
                    {
                        MessageBox.Show(get_patch.NoSupport, "Game version not supported");
                        Process.Start(new ProcessStartInfo(API.WEB_LINK) { UseShellExecute = true });
                        return;
                    }
                }

                // run patch
                var startPatch = PatchGame(patch);
                if (!startPatch)
                {
                    MessageBox.Show("Failed to patch a game file. See console for more details.");
                    return;
                }


            }

            // For Proxy
            if (proxy == null)
            {
                // skip proxy if official server
                if (set_server_host != "official")
                {
                    if (isProxyNeed)
                    {
                        proxy = new Proxy(set_proxy_port, set_server_host, isSendLog);
                        if (!proxy.Start())
                        {
                            MessageBox.Show($"Unable to start proxy on port {set_proxy_port}. Possible reasons:\n\n" +
                                            "1. The port is already in use by another application.\n" +
                                            "2. Windows Firewall is blocking access to this port.\n" +
                                            "3. Windows Update may be using ports in this range.\n\n" +
                                            "Please try the following:\n" +
                                            "- Close any applications that might be using this port.\n" +
                                            "- Check your firewall settings.\n" +
                                            "- Try restarting the application.\n" +
                                            "- If the issue persists, consider using a different port.",
                                            "Proxy Port Error");
                            try
                            {
                                Process.Start(new ProcessStartInfo("cmd", $"/c net stop winnat") { CreateNoWindow = true, UseShellExecute = false });
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("Proxy", $"Error stopping WinNAT service: {ex.Message}");
                            }
                            proxy.Stop();
                            return;
                        }
                        else
                        {
                            if (set_server_host.Contains("yuuki.me"))
                            {
                                if (!API.isYuuki(set_proxy_port))
                                {
                                    proxy.Stop();
                                    InstallCert();
                                    MessageBox.Show("Unable to connect to YuukiPS server. Please try the following steps:\n\n1. Close this program completely\n2. Reopen the program and try again\n\nIf the issue persists, please report it to an admin and include a screenshot of the console.", "Connection Error");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.Info("Proxy", "Proxy is disabled as per user settings");
                    }
                }
                else
                {
                    Logger.Info("Proxy", "Proxy is bypassed when using the official server");
                }
            }
            else
            {
                Logger.Info("Proxy", "Proxy is currently active and running");
            }

            // For Cheat (tmp)
            if (isCheat)
            {
                Logger.Info("Cheat", "Cheat enabled");
                try
                {
                    var get_file_cheat = API.GetCheat(selectedGame, GameChannel, VersionGame, cst_gamefile);
                    if (get_file_cheat == null)
                    {
                        MessageBox.Show("No cheats are available for this game version. Please disable the cheat feature in the settings to launch the game.", "Cheat Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Extra_Cheat.Checked = false;
                        return;
                    }
                    cst_gamefile = get_file_cheat.Launcher;
                    WatchCheat = Path.GetFileNameWithoutExtension(cst_gamefile);
                    Logger.Info("Cheat", $"RUN: Monitor {WatchCheat} at {cst_gamefile}");

                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }

            // For Game
            if (progress == null)
            {
                progress = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = cst_gamefile,
                        //UseShellExecute = true,
                        Arguments = "-server=" + set_server_host, // TODO: custom mod
                        WorkingDirectory = Path.GetDirectoryName(cst_gamefile),
                    }
                };
                try
                {
                    progress.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    AllStop();
                }
            }
            else
            {
                Logger.Info("Game", "Game process is already running. Skipping launch.");
            }
        }

        public void InstallCert()
        {
            bool installationSucceeded = false;
            while (!installationSucceeded)
            {
                try
                {
                    // Load the certificate from the file
                    X509Certificate2 certificate = new X509Certificate2("rootCert.pfx");

                    // Open the Root certificate store for the current user
                    X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadWrite);

                    // Add the certificate to the store
                    store.Add(certificate);

                    // Close the store
                    store.Close();

                    Logger.Info("Certificate", "Certificate installed successfully.");
                    installationSucceeded = true; // Set flag to true to exit the loop
                }
                catch (Exception ex)
                {
                    Logger.Error("Certificate", "Error: " + ex.Message);
                }
            }
        }

        public bool CheckVersionGame(GameType game_type)
        {
            var cst_folder_game = Set_LA_GameFolder.Text;

            // If user doesn't have a game config folder, try searching for it automatically
            if (String.IsNullOrEmpty(cst_folder_game))
            {

                var Get_Launcher = GetLauncherPath(game_type);
                Logger.Info("Launcher", "Folder Launcher: " + (Get_Launcher == "" ? "Not Found" : Get_Launcher));

                if (string.IsNullOrEmpty(Get_Launcher))
                {
                    // If there is no launcher
                    Logger.Info("Game", "Please find game install folder!");
                    return false;
                }
                else
                {
                    // If there is no launcher, try searching the game folder
                    cst_folder_game = GetGamePath(Get_Launcher);
                }
            }

            // Check one more time
            if (string.IsNullOrEmpty(cst_folder_game))
            {
                Logger.Info("Game", "Please find game install folder!");
                return false;
            }
            if (!Directory.Exists(cst_folder_game))
            {
                Logger.Info("Game", "Please find game install folder! (2)"); // TODO
                return false;
            }

            Logger.Info("Game", "Folder Game: " + cst_folder_game);

            string cn = Path.Combine(cst_folder_game, "YuanShen.exe");
            string os = Path.Combine(cst_folder_game, "GenshinImpact.exe");
            if (game_type == GameType.StarRail)
            {
                cn = Path.Combine(cst_folder_game, "StarRail.exe"); // todo
                os = Path.Combine(cst_folder_game, "StarRail.exe");
            }

            // Path
            if (game_type == GameType.GenshinImpact)
            {
                // Pilih Channel
                if (File.Exists(cn))
                {
                    // Jika game versi cina
                    WatchFile = "YuanShen";
                    GameChannel = 2;
                    PathfileGame = cn;
                }
                else if (File.Exists(os))
                {
                    // jika game versi global
                    WatchFile = "GenshinImpact";
                    GameChannel = 1;
                    PathfileGame = os;
                }
                else
                {
                    // jika game versi tidak di dukung atau tidak ada file
                    Logger.Error("Game", $"No game executable found in the specified folder: {cst_folder_game}. Please ensure the game is properly installed.");
                    return false;
                }

                // Settings
                try
                {
                    settings_genshin = new Game.Genshin.Settings(GameChannel);
                    if (settings_genshin != null)
                    {
                        Logger.Info("Game", $"Game Settings - Text Language: {settings_genshin.GetGameLanguage()}, Voice Language: {settings_genshin.GetVoiceLanguageID()}, Server: {settings_genshin.GetRegServerNameID()}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning("Game", "Error getting game settings: " + ex.ToString());
                }

            }
            else
            {
                // jika game versi global
                WatchFile = "StarRail";
                GameChannel = 1;
                PathfileGame = os;
            }

            // Check MD5 Game
            string Game_LOC_Original_MD5 = Tool.CalculateMD5(PathfileGame);

            // Check MD5 in Server API
            get_patch = API.GetMD5Game(Game_LOC_Original_MD5, game_type);
            if (get_patch == null)
            {
                Logger.Error("Game", $"Unsupported game version detected. MD5: {Game_LOC_Original_MD5}. Please report this to the admin.");
                return false;
            }

            VersionGame = get_patch.Version;

            if (VersionGame == "0.0.0")
            {
                Logger.Error("Game", $"Unsupported game version detected. MD5: {Game_LOC_Original_MD5}.");

                Get_LA_Version.Text = "Version: Unknown";
                Get_LA_CH.Text = "Channel: Unknown";
                Get_LA_REL.Text = "Release: Unknown";
                Get_LA_MD5.Text = "MD5: Unknown";

                return false;
            }

            var get_channel = get_patch.Channel;

            // IF ALL OK
            Set_LA_GameFolder.Text = cst_folder_game;

            // Set Version
            Get_LA_Version.Text = "Version: " + get_patch.Version;
            Get_LA_CH.Text = "Channel: " + get_channel;
            Get_LA_REL.Text = "Release: " + get_patch.Release;

            Logger.Info("Game", $"Game version: {VersionGame}");
            Logger.Info("Game", $"Game executable path: {PathfileGame}");
            Logger.Info("Game", $"Game executable MD5 hash: {Game_LOC_Original_MD5}");

            Get_LA_MD5.Text = "MD5: " + Game_LOC_Original_MD5;

            return true;
        }

        public bool PatchGame(bool patchit = true)
        {
            // check folder game (root)
            var root_folder = Set_LA_GameFolder.Text;
            if (string.IsNullOrEmpty(root_folder))
            {
                Logger.Error("PatchGame", "Game folder path is empty or null");
                return false;
            }
            if (!Directory.Exists(root_folder))
            {
                Logger.Error("PatchGame", $"Game folder not found at path: {root_folder}");
                return false;
            }

            // check version
            if (get_patch == null)
            {
                Logger.Error("PatchGame", "Unable to determine game version. Please click 'Get Key' in the config tab to retrieve version information.");
                return false;
            }
            if (VersionGame == "0.0.0")
            {
                Logger.Error("PatchGame", "The current game version is not compatible with this patching method. Please ensure you have a supported game version.");
                return false;
            }

            if (patchit)
            {
                // for patch
                if (get_patch.Patched != null && get_patch.Patched.Any())
                {
                    foreach (var data in get_patch.Patched)
                    {
                        var iss = PatchCopy(root_folder, data.File, data.Location, data.MD5, "patch", get_patch.Version);
                        if (!string.IsNullOrEmpty(iss))
                        {
                            Logger.Error("PatchGame", $"Failed to patch file: {data.File}. Error: {iss}");
                            return false;
                        }
                    }
                    Logger.Info("PatchGame", "Successfully patched all files");
                }
                else
                {
                    Logger.Info("PatchGame", "No files needed patching");
                }
            }
            else
            {
                // for unpatch
                if (get_patch.Patched != null && get_patch.Patched.Any())
                {
                    foreach (var data in get_patch.Patched)
                    {
                        var iss = PatchCopy(root_folder, data.File, data.Location, data.MD5, "unpatch", get_patch.Version);
                        if (!String.IsNullOrEmpty(iss))
                        {
                            Logger.Error("PatchGame", $"Failed to unpatch file: {data.File}. Error: {iss}");
                            return false;
                        }
                    }
                    Logger.Info("PatchGame", "Successfully unpatched all files");
                }
                else
                {
                    Logger.Info("PatchGame", "No files needed unpatching");
                }
                if (get_patch.Original != null && get_patch.Original.Any())
                {
                    foreach (var data in get_patch.Original)
                    {
                        var iss = PatchCopy(root_folder, data.File, data.Location, data.MD5, "original", get_patch.Version);
                        if (!string.IsNullOrEmpty(iss))
                        {
                            Logger.Error("PatchGame", $"Failed to restore original file: {data.File}. Error: {iss}");
                            return false;
                        }
                    }
                    Logger.Info("PatchGame", "Successfully restored all original files");
                }
                else
                {
                    Logger.Info("PatchGame", "No original files needed restoring");
                }
            }

            Logger.Info("PatchGame", $"Game {(patchit ? "patched" : "unpatched")} successfully");
            return true;
        }

        private void Set_LA_Select_Click(object sender, EventArgs e)
        {
            var Folder_Game_Now = SelectGamePath();
            if (!string.IsNullOrEmpty(Folder_Game_Now))
            {
                Set_LA_GameFolder.Text = Folder_Game_Now;
                Logger.Info("Game Folder", $"Selected game folder: {Folder_Game_Now}");
                if (!CheckVersionGame(DefaultProfile.GameConfig.type))
                {
                    string message = $"The game version in {Folder_Game_Now} may not be supported. Please check the console for more details.";
                    Logger.Warning("Game Version", message);
                    MessageBox.Show(message, "Game Version", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    string url = API.WEB_LINK + "/game/" + DefaultProfile.GameConfig.type.SEOUrl();
                    Logger.Info("Browser", $"Opening URL for game support: {url}");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else
                {
                    Logger.Info("Game Version", "Game version check passed successfully.");
                }
            }
            else
            {
                Logger.Error("Game Folder", "No game folder was selected or found.");
                MessageBox.Show("No game folder found. Please select a valid game folder.");
            }
        }

        public static string PatchCopy(string root_folder, string url_file, string file_name, string file_md5, string iscopy, string version)
        {
            string fileSave = Path.Combine(root_folder, file_name);

            if (iscopy == "unpatch")
            {
                try
                {
                    File.Delete(fileSave);
                    Logger.Info("Patch", $"Successfully removed file: {fileSave}");
                    return "";
                }
                catch (Exception e)
                {
                    Logger.Error("Patch", $"Failed to remove file: {fileSave}. Error: {e.Message}");
                    return e.Message;
                }
            }

            if (File.Exists(fileSave))
            {
                var md5_file_raw = Tool.CalculateMD5(fileSave);
                if (md5_file_raw == file_md5)
                {
                    Logger.Info("Patch", $"File '{fileSave}' already exists with matching MD5. No action needed for '{iscopy}' operation.");
                    return "";
                }
            }

            var backupPatch = Path.Combine(Config.Modfolder, "i", version, iscopy, file_name);
            if (File.Exists(backupPatch))
            {
                var md5_file_raw = Tool.CalculateMD5(backupPatch);
                if (md5_file_raw == file_md5)
                {
                    Logger.Info("Patch", $"Found backup {iscopy} > {backupPatch} > {fileSave}");

                    string saveDir = Path.GetDirectoryName(fileSave) ?? string.Empty;
                    if (!string.IsNullOrEmpty(saveDir) && !Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    File.Copy(backupPatch, fileSave, overwrite: true);
                    return "";
                }
                else
                {
                    // skip ?
                }
            }

            Logger.Info("Patch", $"Initiating download for {iscopy}: URL: {url_file}, Destination: {fileSave}");

            var CEKDL1 = new Download(url_file, fileSave);
            if (CEKDL1.ShowDialog() != DialogResult.OK)
            {
                return $"Error download ${iscopy} file: {url_file} to {fileSave}";
            }
            else
            {
                var md5_file = Tool.CalculateMD5(fileSave);
                if (md5_file == file_md5)
                {
                    string backupDir = Path.GetDirectoryName(backupPatch) ?? string.Empty;
                    if (!string.IsNullOrEmpty(backupDir) && !Directory.Exists(backupDir))
                    {
                        Directory.CreateDirectory(backupDir);
                    }
                    Logger.Info("Game", $"MD5 Patch File {url_file}: " + md5_file);
                    File.Copy(fileSave, backupPatch, overwrite: true);
                }
                else
                {
                    return $"Error patch file {url_file}, md5 file mismatch {md5_file}";
                }
            }

            // OK
            return "";
        }

        public void CheckUpdate()
        {
            try
            {
                Logger.Info("Game", "Checking for launcher updates...");
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                if (version == null)
                {
                    SetVersion.Text = "Version: Unknown";
                    return;
                }

                string ver = version.ToString();
                SetVersion.Text = "Version: " + ver;

                var GetDataUpdate = API.GetUpdate();
                if (GetDataUpdate == null) return;

                var name_version = GetDataUpdate.TagName;
                if (!Version.TryParse(name_version, out var version1) || !Version.TryParse(ver, out var version2))
                {
                    Logger.Error("Update", "Unable to compare version numbers. This might be due to an unexpected version format.");
                    MessageBox.Show("We encountered an issue while checking for updates. The version numbers couldn't be compared correctly.", "Update Check Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = version1.CompareTo(version2);

                if (result > 0)
                {
                    SetVersion.Text = $"Version: {ver} (New Update: {name_version})";
                    var tes = MessageBox.Show(GetDataUpdate.Body, $"New Update: {GetDataUpdate.Name}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (tes == DialogResult.Yes)
                    {
                        PerformUpdate(GetDataUpdate.Assets);
                    }
                }
                else if (result < 0)
                {
                    SetVersion.Text = $"Version: {ver} (latest nightly) (Official: {name_version})";
                }
                else
                {
                    SetVersion.Text = $"Version: {ver} (latest public)";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Update", $"Error checking for updates: {ex.Message}");
                MessageBox.Show($"An error occurred while checking for updates: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PerformUpdate(IEnumerable<Assets> assets)
        {
            try
            {
                var url_dl = assets.FirstOrDefault(file =>
                    file.Name == "YuukiPSLauncherPC.zip" ||
                    file.Name == "YuukiPS.zip" ||
                    file.Name == "update.zip")?.BrowserDownloadUrl;

                if (string.IsNullOrEmpty(url_dl))
                {
                    MessageBox.Show("Update file not found.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var updateZipPath = Path.Combine(Json.Config.CurrentlyPath, "update.zip");
                var DL1 = new Download(url_dl, updateZipPath);
                if (DL1.ShowDialog() != DialogResult.OK) return;

                var file_update = Path.Combine(Json.Config.CurrentlyPath, "update.bat");
                using (var w = new StreamWriter(file_update))
                {
                    w.WriteLine("@echo off");
                    w.WriteLine("Taskkill /IM YuukiPS.exe /F");
                    Logger.Info("Update", "Extracting update files...");
                    w.WriteLine("tar -xf update.zip");
                    Logger.Info("Update", "Removing temporary update file...");
                    w.WriteLine("del update.zip");
                    Logger.Info("Update", "Update completed. Restarting application...");
                    w.WriteLine("timeout 5 > NUL");
                    w.WriteLine("start YuukiPS.exe");
                    w.WriteLine("del Update.bat");
                }

                Process.Start(file_update);
            }
            catch (Exception ex)
            {
                Logger.Error("Update", $"Error performing update: {ex.Message}");
                MessageBox.Show($"An error occurred during the update process: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Check Launcher
        private static string GetLauncherPath(GameType version)
        {
            Logger.Info("Launcher", "GetLauncherPath: " + version.GetStringValue());

            RegistryKey key = Registry.LocalMachine;
            if (key != null)
            {
                var tes1 = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + version.GetStringValue());
                if (tes1 != null)
                {
                    var testes1 = tes1.GetValue("InstallPath");
                    if (testes1 != null)
                    {
                        var testestestes1 = testes1.ToString();
                        if (testestestes1 != null)
                        {
                            return testestestes1;
                        }

                    }
                }
            }
            return "";
        }

        // Check Game Install
        private static string GetGamePath(String launcherpath = "")
        {
            string startpath = "";

            if (launcherpath == "")
            {
                return "";
            }

            string cfgPath = Path.Combine(launcherpath, "config.ini");
            if (File.Exists(launcherpath) || File.Exists(cfgPath))
            {
                // baca file config
                using (StreamReader reader = new StreamReader(cfgPath))
                {
                    string[] abc = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (var item in abc)
                    {
                        // cari line install patch
                        if (item.IndexOf("game_install_path") != -1)
                        {
                            startpath += item.Substring(item.IndexOf("=") + 1);
                        }
                    }
                }
            }

            return startpath;
        }

        // Pilih Folder
        private static string SelectGamePath()
        {
            string foldPath = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select Game Folder";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
            }
            return foldPath;
        }

        public void IsAccess(bool onoff)
        {
            GetTypeGame.Enabled = onoff;
            btStartOfficialServer.Enabled = onoff;
            btStartYuukiServer.Enabled = onoff;
            GetServerHost.Enabled = onoff;

            grProxy.Enabled = onoff;
            grExtra.Enabled = onoff;
            grConfigGameLite.Enabled = onoff;
            grProfile.Enabled = onoff;
        }

        private void CheckGameRun_Tick(object sender, EventArgs e)
        {
            var isrun = Process.GetProcesses().Where(pr => pr.ProcessName == WatchFile || pr.ProcessName == WatchCheat);
            if (!isrun.Any())
            {
                // Jika Game tidak berjalan....
                IsGameRun = false;
                btStartNormal.Text = "Launch";
                IsAccess(true);
                // AllStop();

                // Revert to original version every game close
                if (!DoneCheck)
                {
                    Console.WriteLine("Game detected stopped");
                    DoneCheck = true;
                    StopGame(); // this shouldn't be necessary but just let it be
                    StopProxy();

                    PatchGame(false);

                    if (Enable_WipeLoginCache.Checked)
                    {
                        Tool.WipeLogin(DefaultProfile.GameConfig.type);
                    }

                    if (Enable_RPC.Checked)
                    {
                        discord.UpdateStatus("Not playing", "Stop", "sleep");
                    }
                }
            }
            else
            {
                // jika game jalan
                IsGameRun = true;
                btStartNormal.Text = "Stop";
                DoneCheck = false;
                IsAccess(false);

                if (Enable_RPC.Checked)
                {
                    discord.UpdateStatus($"Server: {HostName} Version: {VersionGame}", "In Game", "on", 1);
                }

            }
        }

        public void AllStop()
        {
            try
            {
                StopProxy();
                StopGame();
            }
            catch (Exception e)
            {
                Logger.Error("AllStop", "There was an error occurred when stopping the game and proxy: " + e.Message);
            }
        }


        public void StopProxy()
        {
            try
            {
                if (proxy == null) return;
                proxy.Stop();
                proxy = null;
                Logger.Info("Proxy", "Proxy stopped");
            }
            catch (Exception e)
            {
                Logger.Error("Proxy", "There was an error stopping the proxy: " + e.Message);
            }
        }

        public void StopGame()
        {
            if (progress != null)
            {
                try
                {
                    // Normal Kill
                    progress.Kill();
                    progress.Close();
                    progress.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                progress = null;
            }
            if (IsGameRun)
            {
                try
                {
                    // Foce Kill
                    if (!string.IsNullOrEmpty(WatchFile))
                    {
                        Tool.EndTask(WatchFile);
                        Logger.Info("Game", "EndTask Game: " + WatchFile);
                    }
                    else
                    {
                        Logger.Info("Game", "EndTask not supported");
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("Game", e.Message);
                }
            }
        }

        private void linkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://discord.yuuki.me/") { UseShellExecute = true });
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/YuukiPS/") { UseShellExecute = true });
        }

        private void linkWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(API.WEB_LINK) { UseShellExecute = true });
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsGameRun)
            {
                MessageBox.Show("Can't close program while game is still running.");
                e.Cancel = true;
            }
        }

        private void CheckProxyRun_Tick(object sender, EventArgs e)
        {
            CheckProxy(false);
        }

        void CheckProxy(bool force_off = false)
        {
            try
            {
                // Metode 1
                RegistryKey? registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                if (registry != null)
                {
                    object? v = registry.GetValue("ProxyEnable");
                    if (v != null && (int)v == 1)
                    {
                        // Metode 2
                        if (proxy != null)
                        {
                            object? id = registry.GetValue("ProxyServer");
                            stIsRunProxy.Text = "Status: ON (Internal): " + id;
                        }
                        else
                        {
                            stIsRunProxy.Text = "Status: ON (External)";
                            // If external is on and proxy app is enabled, make sure external proxy is off
                            if (CheckProxyEnable.Checked)
                            {
                                force_off = true;
                            }
                        }

                        if (force_off)
                        {
                            registry.SetValue("ProxyEnable", 0);
                        }
                    }
                    else
                    {
                        StopProxy();
                        stIsRunProxy.Text = "Status: OFF";
                    }
                }
                else
                {
                    Logger.Error("Proxy", "Does not support proxy check!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Server_Config_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = Server.Serverfolder,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void Server_Start_Click(object sender, EventArgs e)
        {
            Logger.Warning("Server", "Still PR :)");
        }

        private void Server_DL_JAVA_Click(object sender, EventArgs e)
        {
            var dl_java = Server.DLJava();
            if (!string.IsNullOrEmpty(dl_java))
            {
                MessageBox.Show(dl_java);
            }
            else
            {
                MessageBox.Show("Download is successful");
            }
        }

        private void Extra_Enable_RPC_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_RPC.Checked)
            {
                Logger.Info("RPC", "Enable RPC");
                discord.Ready();
            }
            else
            {
                Logger.Info("RPC", "Disable RPC. This may take a few seconds");
                discord.Stop();
            }
        }

        private void wipeLoginCacheInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This deletes the login cache every time the game closes (logs you out).\nThis is useful if you use the guest account on HSR servers, since you don't have to remember to log out.", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void Get_LA_MD5_Click(object sender, EventArgs e)
        {
            string md5 = Tool.CalculateMD5(PathfileGame);
            if (string.IsNullOrEmpty(md5))
            {
                MessageBox.Show("Failed to get MD5 hash. Please try again.");
                return;
            };

            using var md5Form = new Form
            {
                Text = "Game Executable MD5",
                Size = new Size(400, 200),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            var label = new Label
            {
                Text = "MD5 Hash",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };

            var textBox = new TextBox
            {
                Text = md5,
                Font = new Font("Consolas", 12),
                ForeColor = Color.LightGreen,
                BackColor = Color.FromArgb(30, 30, 30),
                BorderStyle = BorderStyle.None,
                Size = new Size(360, 30),
                ReadOnly = true
            };

            var button = new Button
            {
                Text = "Copy to Clipboard",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 122, 204),
                Size = new Size(360, 40),
                Cursor = Cursors.Hand
            };

            md5Form.Controls.AddRange(new Control[] { label, textBox, button });

            // Center the controls
            int startY = (md5Form.ClientSize.Height - (label.Height + textBox.Height + button.Height + 20)) / 2;
            label.Location = new Point((md5Form.ClientSize.Width - label.Width) / 2, startY);
            textBox.Location = new Point((md5Form.ClientSize.Width - textBox.Width) / 2, label.Bottom + 10);
            button.Location = new Point((md5Form.ClientSize.Width - button.Width) / 2, textBox.Bottom + 10);

            button.Click += (_, _) =>
            {
                Clipboard.SetText(md5);
                MessageBox.Show("MD5 hash copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            md5Form.ShowDialog();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
