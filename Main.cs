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

        Json.Config configdata = new Json.Config();
        Profile default_profile = new Profile();

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
        Extra.Discord discord = new Extra.Discord();

        // Game
        public Game.Genshin.Settings? settings_genshin = null;

        // Patch
        Patch? get_patch = null;

        Logger logger = new Logger();

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

            logger.initLogging($"Platform: {os.Platform}\nPlatform Version: {os.Version}\nService pack: {os.ServicePack}\n\n", logFilePath);

            Logger.Info("Boot", "Loading....");

            // Before starting make sure proxy is turned off
            CheckProxy(true);

            // Check Update
            CheckUpdate();

            LoadConfig("Boot"); // if found config

            // Load Profile by profile_default
            LoadProfile(configdata.profile_default);

            // Extra
            if (Enable_RPC.Checked)
            {
                Logger.Info("Boot", "Discord RPC enable");
                discord.Ready();
            }
            else
            {
                Logger.Info("Boot", "Discord RPC disable");
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
            default_profile.game.type = (GameType)GetTypeGame.SelectedItem;
        }

        public void LoadConfig(string load_by)
        {
            configdata = Json.Config.LoadConfig();

            Logger.Info("Config", "load config by " + load_by);

            // Unsubscribe from SelectedIndexChanged event
            GetProfileServer.SelectedIndexChanged -= GetProfileServer_SelectedIndexChanged;

            // Profile
            GetProfileServer.DisplayMember = "name";
            GetProfileServer.DataSource = configdata.profile;

            // GameType
            GetTypeGame.DataSource = Enum.GetValues(typeof(GameType));

            // Find the index of the desired profile
            for (int i = 0; i < GetProfileServer.Items.Count; i++)
            {
                Profile profile = (Profile)GetProfileServer.Items[i];
                if (profile.name == configdata.profile_default)
                {
                    Logger.Info("Profiles", "Set index " + i + " name profile " + configdata.profile_default);
                    GetProfileServer.SelectedIndex = i;
                    break;
                }
            }

            // Subscribe back to SelectedIndexChanged event
            GetProfileServer.SelectedIndexChanged += GetProfileServer_SelectedIndexChanged;
        }

        public void LoadProfile(string load_profile = "")
        {

            if (string.IsNullOrEmpty(load_profile))
            {
                Logger.Info("Profiles", "No profile");
                return;
            }

            Logger.Info("Profiles", "Profile: " + load_profile);

            try
            {
                var tmp_profile = configdata.profile.Find(p => p.name == load_profile);
                if (tmp_profile != null)
                {
                    default_profile = tmp_profile;
                }
                else
                {
                    // use default data
                }
                Logger.Info("Profiles", "Server: " + default_profile.server.url);
            }
            catch (Exception e)
            {
                Logger.Error("Profiles", "Profile error (" + e.Message + "), use default data");
            }

            // Data Set

            // Game
            Set_LA_GameFolder.Text = default_profile.game.path;
            GetTypeGame.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(GameType)), default_profile.game.type);

            // Server
            CheckProxyEnable.Checked = default_profile.server.proxy.enable;
            GetServerHost.Text = default_profile.server.url;
            // Extra
            Extra_Cheat.Checked = default_profile.game.extra.Akebi;
            Enable_RPC.Checked = default_profile.game.extra.RPC;

            // Get Data Game
            if (!CheckVersionGame(default_profile.game.type))
            {
                MessageBox.Show("No game folder detected, please manually input game folder then play");
            }

        }
        public void SaveProfile(string name_save = "Default")
        {
            try
            {
                var tmp_profile = new Profile();

                // Game
                tmp_profile.game.path = Set_LA_GameFolder.Text;
                tmp_profile.game.type = (GameType)GetTypeGame.SelectedItem;
                tmp_profile.game.wipeLogin = Enable_WipeLoginCache.Checked;

                // Server
                tmp_profile.server.url = GetServerHost.Text;
                int myInt;
                bool isValid = int.TryParse(GetProxyPort.Text, out myInt);
                if (isValid)
                {
                    tmp_profile.server.proxy.port = myInt;
                }

                // Extra
                tmp_profile.game.extra.Akebi = Extra_Cheat.Checked;
                tmp_profile.game.extra.RPC = Enable_RPC.Checked;

                // Nama Profile
                tmp_profile.name = name_save;

                try
                {
                    int indexToUpdate = configdata.profile.FindIndex(profile => profile.name == name_save);
                    if (indexToUpdate != -1)
                    {
                        Logger.Info("Profiles", "Profile save: " + name_save);
                        configdata.profile[indexToUpdate] = tmp_profile;
                    }
                    else
                    {
                        Logger.Info("Profiles", "Add new profile: " + name_save);
                        configdata.profile.Add(tmp_profile);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Info("Profiles", "Error save config (" + ex.Message + "), so reload it");
                    configdata = new Json.Config() { profile = new List<Profile>() { tmp_profile } };
                }


                configdata.profile_default = name_save;

                File.WriteAllText(Json.Config.ConfigPath, JsonConvert.SerializeObject(configdata));

                Logger.Info("Config", "Done save config...");

                LoadConfig("SaveProfile");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                    if (get_patch != null && get_patch.nosupport != "")
                    {
                        MessageBox.Show(get_patch.nosupport, "Game version not supported");
                        Process.Start(new ProcessStartInfo(API.WEB_LINK) { UseShellExecute = true });
                        return;
                    }
                }

                // run patch
                var startPatch = PatchGame(patch);
                if (!string.IsNullOrEmpty(startPatch))
                {
                    MessageBox.Show(startPatch, "Error Patch");
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
                            MessageBox.Show("Maybe port is already use or Windows Firewall does not allow using port " + set_proxy_port + " or Windows Update sometimes takes that range, or try again it might magically work again.", "Proxy port cannot be used");
                            try
                            {
                                Process.Start(new ProcessStartInfo("cmd", $"/c net stop winnat") { CreateNoWindow = true, UseShellExecute = false });
                            }
                            catch
                            {
                                // skip
                            }
                            StopProxy();
                            return;
                        }
                        else
                        {
                            if (set_server_host.Contains("yuuki.me"))
                            {
                                if (!API.isYuuki(set_proxy_port))
                                {
                                    StopProxy();
                                    InstallCert();
                                    MessageBox.Show("Try closing this program then opening it again, if there is still an error, please report it to admin with a screenshot console", "Not yet connected to YuukiPS server");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.Info("Proxy", "Proxy is ignored, because you turned it off");
                    }
                }
                else
                {
                    Logger.Info("Proxy", "Proxy is ignored, because use official server");
                }
            }
            else
            {
                Logger.Info("Proxy", " Proxy is still running...");
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
                        MessageBox.Show("No cheats found for this version, please turn off the cheat feature to run the game.");
                        Extra_Cheat.Checked = false;
                        return;
                    }
                    cst_gamefile = get_file_cheat.launcher;
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
                progress = new Process();
                progress.StartInfo = new ProcessStartInfo
                {
                    FileName = cst_gamefile,
                    //UseShellExecute = true,
                    Arguments = "-server=" + set_server_host, // TODO: custom mod
                    WorkingDirectory = Path.GetDirectoryName(cst_gamefile),
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
                Logger.Info("Game", "Progress is still running...");
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

                    Console.WriteLine("Certificate installed successfully.");
                    installationSucceeded = true; // Set flag to true to exit the loop
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
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
                Logger.Info("Launcher", "Folder Launcher: " + Get_Launcher);

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
                    Logger.Error("Game", "No game files found!!!");
                    return false;
                }

                // Settings
                try
                {
                    settings_genshin = new Game.Genshin.Settings(GameChannel);
                    if (settings_genshin != null)
                    {
                        Logger.Info("Game", "Game Text Language: " + settings_genshin.GetGameLanguage());
                        Logger.Info("Game", "Game Voice Language: " + settings_genshin.GetVoiceLanguageID());
                        Logger.Info("Game", "Game Server: " + settings_genshin.GetRegServerNameID());
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
                Logger.Error("Game", "No Support Game with MD5: " + Game_LOC_Original_MD5 + " (Send this log to admin)");
                return false;
            }

            VersionGame = get_patch.version;

            if (VersionGame == "0.0.0")
            {
                Logger.Error("Game", "Version not supported: MD5 " + Game_LOC_Original_MD5);

                Get_LA_Version.Text = "Version: Unknown";
                Get_LA_CH.Text = "Channel: Unknown";
                Get_LA_REL.Text = "Release: Unknown";
                Get_LA_MD5.Text = "MD5: Unknown";

                return false;
            }

            var get_channel = get_patch.channel;

            // IF ALL OK
            Set_LA_GameFolder.Text = cst_folder_game;

            // Set Version
            Get_LA_Version.Text = "Version: " + get_patch.version;
            Get_LA_CH.Text = "Channel: " + get_channel;
            Get_LA_REL.Text = "Release: " + get_patch.release;            

            Logger.Info("Game", "Currently using version game " + VersionGame);
            Logger.Info("Game", "File Game: " + PathfileGame);
            Logger.Info("Game", "MD5 Game Currently: " + Game_LOC_Original_MD5);

            Get_LA_MD5.Text = "MD5: " + Game_LOC_Original_MD5;

            return true;
        }

        public string PatchGame(bool patchit = true)
        {
            // check folder game (root)
            var root_folder = Set_LA_GameFolder.Text;
            if (String.IsNullOrEmpty(root_folder))
            {
                return "No game folder found (1)";
            }
            if (!Directory.Exists(root_folder))
            {
                return "No game folder found (2)";
            }

            // check version
            if (get_patch == null)
            {
                return "Can't find version, try clicking 'Get Key' in config tab";
            }            
            if (VersionGame == "0.0.0")
            {
                return "This Game Version is not compatible with this method patch";
            }

            if (patchit)
            {
                // for patch
                if (get_patch.patched != null && get_patch.patched.Any())
                {
                    foreach (var data in get_patch.patched)
                    {
                        var iss = PatchCopy(root_folder, data.file, data.location, data.md5, "patch", get_patch.version);
                        if (!String.IsNullOrEmpty(iss))
                        {
                            return iss;
                        }
                    }
                }
                else
                {
                    Logger.Info("Patch", "no need for any patch");
                }
            }
            else
            {
                // for unpatch
                if (get_patch.patched != null && get_patch.patched.Any())
                {
                    foreach (var data in get_patch.patched)
                    {
                        var iss = PatchCopy(root_folder, data.file, data.location, data.md5, "unpatch", get_patch.version);
                        if (!String.IsNullOrEmpty(iss))
                        {
                            return iss;
                        }
                    }
                }
                else
                {
                    Logger.Info("Patch", "no files deleted");
                }
                if (get_patch.original != null && get_patch.original.Any())
                {
                    foreach (var data in get_patch.original)
                    {
                        var iss = PatchCopy(root_folder, data.file, data.location, data.md5, "original", get_patch.version);
                        if (!String.IsNullOrEmpty(iss))
                        {
                            return iss;
                        }
                    }
                }
                else
                {
                    Logger.Info("Patch", "no files restored to original");
                }
            }

            return "";
        }

        private void Set_LA_Select_Click(object sender, EventArgs e)
        {
            var Folder_Game_Now = SelectGamePath();
            if (!string.IsNullOrEmpty(Folder_Game_Now))
            {
                Set_LA_GameFolder.Text = Folder_Game_Now;
                if (CheckVersionGame(default_profile.game.type))
                {
                    MessageBox.Show("This game version may not be supported please check your console");
                    Process.Start(new ProcessStartInfo(API.WEB_LINK + "/game/" + default_profile.game.type.SEOUrl()) { UseShellExecute = true });
                }
            }
            else
            {
                MessageBox.Show("No game folder found");
            }
        }

        public string PatchCopy(string root_folder, string url_file, string file_name, string file_md5, string iscopy, string version)
        {
            string fileSave = Path.Combine(root_folder, file_name);

            if (iscopy == "unpatch")
            {
                try
                {
                    File.Delete(fileSave);
                    Logger.Info("Patch", $"File remove {fileSave}");
                    return "";
                }
                catch (Exception e)
                {
                    Logger.Error("Patch", $"File remove error {fileSave} > {e.Message}");
                    return e.Message;
                }
            }

            if (File.Exists(fileSave))
            {
                var md5_file_raw = Tool.CalculateMD5(fileSave);
                if(md5_file_raw == file_md5)
                {
                    Logger.Info("Patch", $"File {fileSave} this already exists and md5 is accurate so no action is needed for {iscopy}");
                    return "";
                }
            }                

            var backupPatch = Path.Combine(Config.Modfolder, "i", version, iscopy, file_name);
            if (File.Exists(backupPatch))
            {
                var md5_file_raw = Tool.CalculateMD5(backupPatch);
                if(md5_file_raw == file_md5)
                {
                    Logger.Info("Patch", $"Found backup {iscopy} > {backupPatch} > {fileSave}");

                    string saveDir = Path.GetDirectoryName(fileSave);
                    if (!Directory.Exists(saveDir))
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

            Logger.Info("Patch", $"Start download {url_file} and save to {fileSave} for {iscopy}");

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
                    string backupDir = Path.GetDirectoryName(backupPatch);
                    if (!Directory.Exists(backupDir))
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
            Logger.Info("Game", "Check update...");
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            string ver = "";
            if (version != null)
            {
                ver = version.ToString();
                Set_Version.Text = "Version: " + ver;
                var GetDataUpdate = API.GetUpdate();
                if (GetDataUpdate != null)
                {
                    var judul = "New Update: " + GetDataUpdate.name; // is dev or nightly or name update
                    var name_version = GetDataUpdate.tag_name; // version name
                    var infobody = GetDataUpdate.body; // info

                    var version1 = new Version(name_version);
                    var version2 = new Version(ver);

                    var result = version1.CompareTo(version2);

                    if (result > 0)
                    {
                        // versi 1 lebih besar
                        Set_Version.Text = "Version: " + ver + " (New Update: " + name_version + " )";
                        var tes = MessageBox.Show(infobody, judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (tes == DialogResult.Yes)
                        {
                            var url_dl = "";
                            foreach (var file in GetDataUpdate.assets)
                            {
                                if (file.name == "YuukiPSLauncherPC.zip" || file.name == "YuukiPS.zip" || file.name == "update.zip")
                                {
                                    url_dl = file.browser_download_url;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(url_dl))
                            {
                                var DL1 = new Download(url_dl, Json.Config.CurrentlyPath + @"\update.zip");
                                if (DL1.ShowDialog() == DialogResult.OK)
                                {
                                    // update
                                    var file_update = Json.Config.CurrentlyPath + @"\update.bat";
                                    try
                                    {
                                        //buat bat
                                        var w = new StreamWriter(file_update);
                                        w.WriteLine("@echo off");

                                        //kill all
                                        w.WriteLine("Taskkill /IM YuukiPS.exe /F");
                                        //w.WriteLine("Taskkill /IM YuukiPS.vshost.exe /F");

                                        // Unzip file
                                        w.WriteLine("echo unzip file...");
                                        w.WriteLine("tar -xf update.zip");

                                        //delete file old
                                        w.WriteLine("echo delete file old");
                                        w.WriteLine("del update.zip");

                                        //start bot
                                        w.WriteLine("echo Update done, start back...");
                                        w.WriteLine("timeout 5 > NUL");
                                        w.WriteLine("start YuukiPS.exe");
                                        w.WriteLine("del Update.bat");
                                        w.Close();
                                        //open bat
                                        Process.Start(file_update);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                // update batal
                            }

                        }
                    }
                    else if (result < 0)
                    {
                        Set_Version.Text = "Version: " + ver + " (latest nightly) (Official: " + name_version + ")";
                    }
                    else
                    {
                        Set_Version.Text = "Version: " + ver + " (latest public)";
                    }
                }
            }
            else
            {
                Set_Version.Text = "Version: Unknown";
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

        public void IsAcess(bool onoff)
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
                IsAcess(true);

                AllStop();

                // Revert to original version every game close
                if (!DoneCheck)
                {
                    Console.WriteLine("Game detected stopped");
                    DoneCheck = true;
                    StopGame(); // this shouldn't be necessary but just let it be
                    
                    var unpatch = PatchGame(false);
                    if (!string.IsNullOrEmpty(unpatch))
                    {
                        Logger.Info("Game", unpatch);
                    }

                    if (Enable_WipeLoginCache.Checked)
                    {
                        Tool.WipeLogin(default_profile.game.type);
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
                IsAcess(false);

                if (Enable_RPC.Checked)
                {
                    discord.UpdateStatus($"Server: {HostName} Version: {VersionGame}", "In Game", "on", 1);
                }

            }
        }

        public void AllStop()
        {
            StopProxy();
            StopGame();
        }

        public void StopProxy()
        {
            try
            {
                proxy.Stop();
                proxy = null;
                Logger.Info("Proxy", "Proxy Stop....");
            }
            catch (Exception ex) { 
                // skip
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
    }
}
