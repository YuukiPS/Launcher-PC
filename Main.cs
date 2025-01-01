using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Yuuki;
using YuukiPS_Launcher.Utils;
using System.Security.Cryptography.X509Certificates;

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
        public bool isGameRunning = false;
        public bool DoneCheck = true;

        // Config basic game
        public string VersionGame = "";
        public int GameChannel = 0;
        public string PathfileGame = "";

        // Extra
        readonly Extra.Discord discord = new();

        // Game
        public Game.Genshin.Settings? settingsGenshin = null;

        // Patch
        Patch? getPatch = null;

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

        private void BTLoadClick(object? sender, EventArgs e)
        {
            var getSelectProfile = GetProfileServer.Text;
            LoadProfile(getSelectProfile);
        }

        private void SetLASaveClick(object? sender, EventArgs e)
        {
            var getSelectProfile = GetProfileServer.Text;
            SaveProfile(getSelectProfile);
        }

        private void GetProfileServer_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var getSelectProfile = GetProfileServer.Text;
            Logger.Info("Profiles", "GetProfileServer_SelectedIndexChanged " + getSelectProfile);
            LoadProfile(getSelectProfile);
        }

        private void GetTypeGame_SelectedIndexChanged(object? sender, EventArgs e)
        {
            DefaultProfile.GameConfig.type = (GameType)GetTypeGame.SelectedItem;
        }

        public void LoadConfig(string LoadBy)
        {
            ConfigData = Config.LoadConfig();

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

        public void LoadProfile(string loadProfile = "")
        {

            if (string.IsNullOrEmpty(loadProfile))
            {
                Logger.Info("Profiles", "No profile specified. Using default settings.");
                return;
            }

            Logger.Info("Profiles", $"Loading profile: '{loadProfile}'");

            try
            {
                var tmpProfile = ConfigData.Profile.Find(p => p.name == loadProfile);
                if (tmpProfile != null)
                {
                    DefaultProfile = tmpProfile;
                }
                else
                {
                    // use default data
                }
                Logger.Info("Profiles", $"Server URL: {DefaultProfile.ServerConfig.url}");
            }
            catch (Exception e)
            {
                Logger.Error("Profiles", $"Failed to load profile: {e.Message}. Using default data.");
            }

            // Data Set

            // Game
            Set_LA_GameFolder.Text = DefaultProfile.GameConfig.path;
            GetTypeGame.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(GameType)), DefaultProfile.GameConfig.type);

            // Server
            CheckProxyEnable.Checked = DefaultProfile.ServerConfig.proxy.enable;
            GetServerHost.Text = DefaultProfile.ServerConfig.url;
            // Extra
            ExtraCheat.Checked = DefaultProfile.GameConfig.extra.Cheat;
            Enable_RPC.Checked = DefaultProfile.GameConfig.extra.RPC;

            // Get Data Game
            var typeGame = DefaultProfile.GameConfig.type;
            if (!CheckVersionGame(typeGame))
            {
                var message = "Game folder not found please download correct version or set game folder manually.";
                var getLauncher = GetLauncherPath(typeGame);
                if (getLauncher != "")
                {
                    message = $"No suitable game version found ({GetGameVersion(GetGamePath(getLauncher))}) for private server or game folder not found please download the correct version or set the game folder manually.";
                    string url = API.WebLink + "/game/" + typeGame.SEOUrl();
                    Logger.Info("Browser", $"Opening URL for game support: {url}");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                Logger.Warning("Game", message);
                MessageBox.Show(message, "Game Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void SaveProfile(string NameSave = "Default")
        {
            try
            {
                var tmpProfile = new Profile();

                // Game
                tmpProfile.GameConfig.path = Set_LA_GameFolder.Text;
                tmpProfile.GameConfig.type = (GameType)GetTypeGame.SelectedItem;
                tmpProfile.GameConfig.wipeLogin = Enable_WipeLoginCache.Checked;

                // Server
                tmpProfile.ServerConfig.url = GetServerHost.Text;
                bool isValid = int.TryParse(GetProxyPort.Text, out int myInt);
                if (isValid)
                {
                    tmpProfile.ServerConfig.proxy.port = myInt;
                }

                // Extra
                tmpProfile.GameConfig.extra.Cheat = ExtraCheat.Checked;
                tmpProfile.GameConfig.extra.RPC = Enable_RPC.Checked;

                // Nama Profile
                tmpProfile.name = NameSave;

                try
                {
                    int indexToUpdate = ConfigData.Profile.FindIndex(profile => profile.name == NameSave);
                    if (indexToUpdate != -1)
                    {
                        Logger.Info("Profiles", $"Updating existing profile: {NameSave}");
                        ConfigData.Profile[indexToUpdate] = tmpProfile;
                    }
                    else
                    {
                        Logger.Info("Profiles", $"Creating new profile: {NameSave}");
                        ConfigData.Profile.Add(tmpProfile);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Profiles", $"Failed to save profile '{NameSave}'. Error: {ex.Message}. Reinitializing configuration.");
                    ConfigData = new Config() { Profile = new List<Profile>() { tmpProfile } };
                }


                ConfigData.profile_default = NameSave;

                File.WriteAllText(Config.ConfigPath, JsonConvert.SerializeObject(ConfigData));

                Logger.Info("Config", "Configuration saved successfully.");

                LoadConfig("SaveProfile");
            }
            catch (Exception ex)
            {
                Logger.Error("Profiles", $"Failed to save profile: {ex.Message}. Reverting to default configuration.");
            }
        }

        private void BTStartOfficialServer_Click(object sender, EventArgs e)
        {
            GetServerHost.Text = "official";
            CheckProxyEnable.Checked = false;
            DoStart();
        }

        private void BTStartYuukiServer_Click(object sender, EventArgs e)
        {
            GetServerHost.Text = API.WebLink;
            CheckProxyEnable.Checked = true;
            DoStart();
        }

        private void BTStartNormal_Click(object sender, EventArgs e)
        {
            DoStart();
        }

        public void DoStart()
        {
            // Jika game berjalan...
            if (isGameRunning)
            {
                AllStop();
                return;
            }

            // Setup
            bool isCheat = ExtraCheat.Checked;
            bool isProxyNeed = CheckProxyEnable.Checked;
            bool isSendLog = EnableSendLog.Checked;
            bool isShowLog = EnableShowLog.Checked;

            GameType selectedGame = (GameType)GetTypeGame.SelectedItem;

            // Get Host
            string setServerHost = GetServerHost.Text;
            if (string.IsNullOrEmpty(setServerHost))
            {
                MessageBox.Show("Please select a server first, you can click on one in server list");
                return;
            }

            // Get Proxy
            int setProxyPort = int.Parse(GetProxyPort.Text);

            // Get Game
            var cstGameFile = PathfileGame;
            if (string.IsNullOrEmpty(cstGameFile))
            {
                MessageBox.Show("No game file config found");
                return;
            }
            if (!File.Exists(cstGameFile))
            {
                MessageBox.Show("Please find game install folder!");
                return;
            }

            bool patch = true;

            // Check progress
            if (!isGameRunning)
            {
                // if game is not running
                if (progress != null)
                {
                    Logger.Info("Game", "progress tes");
                }

                // if server is official 
                if (setServerHost == "official")
                {
                    patch = false;
                }
                else
                {
                    if (getPatch != null && getPatch.NoSupport != "")
                    {
                        MessageBox.Show(getPatch.NoSupport, "Game version not supported");
                        Process.Start(new ProcessStartInfo(API.WebLink) { UseShellExecute = true });
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
                if (setServerHost != "official")
                {
                    if (isProxyNeed)
                    {
                        proxy = new Proxy(setProxyPort, setServerHost, isSendLog, isShowLog);
                        if (!proxy.Start())
                        {
                            MessageBox.Show($"Unable to start proxy on port {setProxyPort}. Possible reasons:\n\n" +
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
                            if (setServerHost.Contains("yuuki.me"))
                            {
                                if (!API.IsYuuki(setProxyPort))
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
                    var getFileCheat = API.GetCheat(selectedGame, GameChannel, VersionGame, cstGameFile);
                    if (getFileCheat == null)
                    {
                        MessageBox.Show("No cheats are available for this game version. Please disable the cheat feature in the settings to launch the game.", "Cheat Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ExtraCheat.Checked = false;
                        return;
                    }
                    cstGameFile = getFileCheat.Launcher;
                    WatchCheat = Path.GetFileNameWithoutExtension(cstGameFile);
                    Logger.Info("Cheat", $"RUN: Monitor {WatchCheat} at {cstGameFile}");

                }
                catch (Exception x)
                {
                    Logger.Error("Cheat", $"Error: {x.Message}");
                }
            }

            // For Game
            if (progress == null)
            {
                progress = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = cstGameFile,
                        //UseShellExecute = true,
                        Arguments = "-server=" + setServerHost, // TODO: custom mod
                        WorkingDirectory = Path.GetDirectoryName(cstGameFile),
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

        public static void InstallCert()
        {
            bool installationSucceeded = false;
            while (!installationSucceeded)
            {
                try
                {
                    // Load the certificate from the file
                    X509Certificate2 certificate = new X509Certificate2("rootCert.pfx");

                    // Open the Root certificate store for the current user
                    X509Store store = new(StoreName.Root, StoreLocation.CurrentUser);
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

        public bool CheckVersionGame(GameType gameType)
        {
            var cstFolderGame = Set_LA_GameFolder.Text;

            // If user doesn't have a game config folder, try searching for it automatically
            if (string.IsNullOrEmpty(cstFolderGame))
            {

                var getLauncher = GetLauncherPath(gameType);
                Logger.Info("Launcher", "Folder Launcher: " + (getLauncher == "" ? "Not Found" : getLauncher));

                if (string.IsNullOrEmpty(getLauncher))
                {
                    // If there is no launcher
                    Logger.Info("Game", "Please find game install folder!");
                    return false;
                }
                else
                {
                    // If there is no launcher, try searching the game folder
                    cstFolderGame = GetGamePath(getLauncher);
                }
            }

            // Check one more time
            if (string.IsNullOrEmpty(cstFolderGame))
            {
                Logger.Info("Game", "Please find game install folder!");
                return false;
            }
            if (!Directory.Exists(cstFolderGame))
            {
                Logger.Info("Game", "Please find game install folder! (2)"); // TODO
                return false;
            }

            Logger.Info("Game", "Folder Game: " + cstFolderGame);

            string cn = Path.Combine(cstFolderGame, "YuanShen.exe");
            string os = Path.Combine(cstFolderGame, "GenshinImpact.exe");
            if (gameType == GameType.StarRail)
            {
                cn = Path.Combine(cstFolderGame, "StarRail.exe"); // todo
                os = Path.Combine(cstFolderGame, "StarRail.exe");
            }

            // Path
            if (gameType == GameType.GenshinImpact)
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
                    Logger.Error("Game", $"No game executable found in the specified folder: {cstFolderGame}. Please ensure the game is properly installed.");
                    return false;
                }

                // Settings
                try
                {
                    settingsGenshin = new Game.Genshin.Settings(GameChannel);
                    if (settingsGenshin != null)
                    {
                        Logger.Info("Game", $"Game Settings - Text Language: {settingsGenshin.GetGameLanguage()}, Voice Language: {settingsGenshin.GetVoiceLanguageID()}, Server: {settingsGenshin.GetRegServerNameID()}");
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
            string gameLOCOriginalMD5 = Tool.CalculateMD5(PathfileGame);

            // Check MD5 in Server API
            getPatch = API.GetMD5Game(gameLOCOriginalMD5, gameType);
            if (getPatch == null)
            {
                Logger.Error("Game", $"Unsupported game version detected. MD5: {gameLOCOriginalMD5}. Please report this to the admin.");
                return false;
            }

            VersionGame = getPatch.Version;

            if (VersionGame == "0.0.0")
            {
                Logger.Error("Game", $"Unsupported game version detected. MD5: {gameLOCOriginalMD5}.");

                Get_LA_Version.Text = "Version: Unknown";
                Get_LA_CH.Text = "Channel: Unknown";
                Get_LA_REL.Text = "Release: Unknown";
                Get_LA_MD5.Text = "MD5: Unknown";

                return false;
            }

            var get_channel = getPatch.Channel;

            // IF ALL OK
            Set_LA_GameFolder.Text = cstFolderGame;

            // Set Version
            Get_LA_Version.Text = "Version: " + getPatch.Version;
            Get_LA_CH.Text = "Channel: " + get_channel;
            Get_LA_REL.Text = "Release: " + getPatch.Release;

            Logger.Info("Game", $"Game version: {VersionGame}");
            Logger.Info("Game", $"Game executable path: {PathfileGame}");
            Logger.Info("Game", $"Game executable MD5 hash: {gameLOCOriginalMD5}");

            Get_LA_MD5.Text = "MD5: " + gameLOCOriginalMD5;

            return true;
        }

        public bool PatchGame(bool patchIt = true)
        {
            // check folder game (root)
            var rootFolder = Set_LA_GameFolder.Text;
            if (string.IsNullOrEmpty(rootFolder))
            {
                Logger.Error("PatchGame", "Game folder path is empty or null");
                return false;
            }
            if (!Directory.Exists(rootFolder))
            {
                Logger.Error("PatchGame", $"Game folder not found at path: {rootFolder}");
                return false;
            }

            // check version
            if (getPatch == null)
            {
                Logger.Error("PatchGame", "Unable to determine game version. Please click 'Get Key' in the config tab to retrieve version information.");
                return false;
            }
            if (VersionGame == "0.0.0")
            {
                Logger.Error("PatchGame", "The current game version is not compatible with this patching method. Please ensure you have a supported game version.");
                return false;
            }

            if (patchIt)
            {
                // for patch
                if (getPatch.Patched != null && getPatch.Patched.Any())
                {
                    foreach (var data in getPatch.Patched)
                    {
                        var iss = PatchCopy(rootFolder, data.File, data.Location, data.MD5, "patch", getPatch.Version);
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
                if (getPatch.Patched != null && getPatch.Patched.Any())
                {
                    foreach (var data in getPatch.Patched)
                    {
                        var iss = PatchCopy(rootFolder, data.File, data.Location, data.MD5, "unpatch", getPatch.Version);
                        if (!string.IsNullOrEmpty(iss))
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
                if (getPatch.Original != null && getPatch.Original.Any())
                {
                    foreach (var data in getPatch.Original)
                    {
                        var iss = PatchCopy(rootFolder, data.File, data.Location, data.MD5, "original", getPatch.Version);
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

            Logger.Info("PatchGame", $"Game {(patchIt ? "patched" : "unpatched")} successfully");
            return true;
        }

        private void Set_LA_Select_Click(object sender, EventArgs e)
        {
            var selectedGameFolder = SelectGamePath();
            if (!string.IsNullOrEmpty(selectedGameFolder))
            {
                Set_LA_GameFolder.Text = selectedGameFolder;
                Logger.Info("Game Folder", $"Selected game folder: {selectedGameFolder}");
                var typeGame = DefaultProfile.GameConfig.type;
                if (!CheckVersionGame(typeGame))
                {
                    string message = $"The game version in {selectedGameFolder} ({GetGameVersion(selectedGameFolder)}) may not be supported. Please check the console for more details.";
                    Logger.Warning("Game Version", message);
                    MessageBox.Show(message, "Game Version", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    string url = API.WebLink + "/game/" + typeGame.SEOUrl();
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

        public static string PatchCopy(string rootFolder, string urlFile, string fileName, string fileMD5, string isCopy, string version)
        {
            string fileSave = Path.Combine(rootFolder, fileName);

            if (isCopy == "unpatch")
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
                if (md5_file_raw == fileMD5)
                {
                    Logger.Info("Patch", $"File '{fileSave}' already exists with matching MD5. No action needed for '{isCopy}' operation.");
                    return "";
                }
            }

            var backupPatch = Path.Combine(Config.Modfolder, "i", version, isCopy, fileName);
            if (File.Exists(backupPatch))
            {
                var backupPatchMd5 = Tool.CalculateMD5(backupPatch);
                if (backupPatchMd5 == fileMD5)
                {
                    Logger.Info("Patch", $"Found backup {isCopy} > {backupPatch} > {fileSave}");

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

            Logger.Info("Patch", $"Initiating download for {isCopy}: URL: {urlFile}, Destination: {fileSave}");

            var CEKDL1 = new Download(urlFile, fileSave);
            if (CEKDL1.ShowDialog() != DialogResult.OK)
            {
                return $"Error download ${isCopy} file: {urlFile} to {fileSave}";
            }
            else
            {
                var md5_file = Tool.CalculateMD5(fileSave);
                if (md5_file == fileMD5)
                {
                    string backupDir = Path.GetDirectoryName(backupPatch) ?? string.Empty;
                    if (!string.IsNullOrEmpty(backupDir) && !Directory.Exists(backupDir))
                    {
                        Directory.CreateDirectory(backupDir);
                    }
                    Logger.Info("Game", $"MD5 Patch File {urlFile}: " + md5_file);
                    File.Copy(fileSave, backupPatch, overwrite: true);
                }
                else
                {
                    return $"Error patch file {urlFile}, md5 file mismatch {md5_file}";
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
                var versionLauncher = "";
                if (version == null)
                {
                    Text = "YuukiPS Launcher " + "(Version: Unknown)";
                    return;
                }

                string ver = version.ToString();
                versionLauncher = "Version: " + ver;
                Text = "YuukiPS Launcher " + versionLauncher;
                var getDataUpdate = API.GetUpdate();
                if (getDataUpdate == null) return;

                var nameVersion = getDataUpdate.TagName;
                if (!Version.TryParse(nameVersion, out var version1) || !Version.TryParse(ver, out var version2))
                {
                    Logger.Error("Update", "Unable to compare version numbers. This might be due to an unexpected version format.");
                    MessageBox.Show("We encountered an issue while checking for updates. The version numbers couldn't be compared correctly.", "Update Check Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = version1.CompareTo(version2);

                if (result > 0)
                {
                    versionLauncher = $"Version: {ver} (New Update: {nameVersion})";
                    var tes = MessageBox.Show(getDataUpdate.Body, $"New Update: {getDataUpdate.Name}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (tes == DialogResult.Yes)
                    {
                        PerformUpdate(getDataUpdate.Assets);
                    }
                }
                else if (result < 0)
                {
                    versionLauncher = $"Version: {ver} (latest nightly) (Official: {nameVersion})";
                }
                else
                {
                    versionLauncher = $"Version: {ver} (latest public)";
                }

                Text = "YuukiPS Launcher " + versionLauncher;
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

                var updateZipPath = Path.Combine(Config.CurrentlyPath, "update.zip");
                var DL1 = new Download(url_dl, updateZipPath);
                if (DL1.ShowDialog() != DialogResult.OK) return;

                var fileUpdate = Path.Combine(Config.CurrentlyPath, "update.bat");
                using (var w = new StreamWriter(fileUpdate))
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

                Process.Start(fileUpdate);
            }
            catch (Exception ex)
            {
                Logger.Error("Update", $"Error performing update: {ex.Message}");
                MessageBox.Show($"An error occurred during the update process: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Check Launcher
        private static string GetLauncherPath(GameType type)
        {
            RegistryKey key = Registry.LocalMachine;
            if (key != null)
            {
                var subKey = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + type.GetStringValue());
                if (subKey != null)
                {
                    var installPathValue = subKey.GetValue("InstallPath");
                    if (installPathValue != null)
                    {
                        var installPathString = installPathValue.ToString();
                        if (installPathString != null)
                        {
                            Logger.Info("Launcher", "Launcher Path: " + installPathString);
                            return installPathString;
                        }
                    }
                }
            }
            return "";
        }

        // Check Game Install
        private static string GetGamePath(string launcherPath = "")
        {
            string startPath = "";

            if (launcherPath == "")
            {
                return "";
            }

            string cfgPath = Path.Combine(launcherPath, "config.ini");
            if (File.Exists(launcherPath) || File.Exists(cfgPath))
            {
                // read config
                using StreamReader reader = new(cfgPath);
                string[] abc = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (var item in abc)
                {
                    // search line install patch
                    if (item.Contains("game_install_path", StringComparison.CurrentCulture))
                    {
                        startPath += item[(item.IndexOf("=") + 1)..];
                    }
                }
            }

            return startPath;
        }

        private static string GetGameVersion(string gamePath = "")
        {
            if (string.IsNullOrEmpty(gamePath))
            {
                return "?1";
            }

            string cfgPath = Path.Combine(gamePath, "config.ini");
            if (!File.Exists(cfgPath))
            {
                return "?2";
            }

            string gameVersion = "?V";

            // Read the configuration file
            using StreamReader reader = new(cfgPath);
            string[] configLines = reader.ReadToEnd().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in configLines)
            {
                // Look for the "game_version" key
                if (line.StartsWith("game_version", StringComparison.CurrentCulture))
                {
                    int equalsIndex = line.IndexOf('=');
                    if (equalsIndex >= 0 && equalsIndex + 1 < line.Length)
                    {
                        gameVersion = line[(equalsIndex + 1)..].Trim();
                    }
                    break;
                }
            }

            return gameVersion;
        }

        // Select Folder Game
        private static string SelectGamePath()
        {
            string foldPath = "";
            FolderBrowserDialog dialog = new()
            {
                Description = "Select Game Folder"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
            }
            return foldPath;
        }

        public void IsAccess(bool isEnabled)
        {
            GetTypeGame.Enabled = isEnabled;
            btStartOfficialServer.Enabled = isEnabled;
            btStartYuukiServer.Enabled = isEnabled;
            GetServerHost.Enabled = isEnabled;

            grProxy.Enabled = isEnabled;
            grExtra.Enabled = isEnabled;
            grConfigGameLite.Enabled = isEnabled;
            grProfile.Enabled = isEnabled;
        }

        private void CheckGameRun_Tick(object sender, EventArgs e)
        {
            var isRunning = Process.GetProcesses().Where(pr => pr.ProcessName == WatchFile || pr.ProcessName == WatchCheat);
            if (!isRunning.Any())
            {
                // If Game doesn't run....
                isGameRunning = false;
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
                // if game is running
                isGameRunning = true;
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
            if (isGameRunning)
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

        private void LinkDiscordLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://discord.yuuki.me/") { UseShellExecute = true });
        }

        private void LinkGithubLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/YuukiPS/") { UseShellExecute = true });
        }

        private void LinkWebLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(API.WebLink) { UseShellExecute = true });
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (isGameRunning)
            {
                MessageBox.Show("Can't close program while game is still running.");
                e.Cancel = true;
            }
        }

        private void CheckProxyRunTick(object sender, EventArgs e)
        {
            CheckProxy(false);
        }

        void CheckProxy(bool forceDisable = false)
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
                                forceDisable = true;
                            }
                        }

                        if (forceDisable)
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

        private void ExtraEnableRPCCheckedChanged(object sender, EventArgs e)
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

        private void WipeLoginCacheInfoClick(object sender, EventArgs e)
        {
            MessageBox.Show("This deletes the login cache every time the game closes (logs you out).\nThis is useful if you use the guest account on HSR servers, since you don't have to remember to log out.", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
