using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using YuukiPS_Launcher.Extra;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Yuuki;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace YuukiPS_Launcher
{
    public partial class Main : Form
    {

        // Main Function
        private Proxy? proxy;
        private Process? progress;

        // Server List
        Thread? thServerList = null;
        List<DataServer> ListServer = new List<DataServer> { new DataServer() };


        Json.Config configdata = new Json.Config();
        Profile default_profile = new Profile();

        // Stats default
        public bool notbootyet = true;
        public string WatchFile = "";
        public string WatchCheat = "melon123";
        public string HostName = "YuukiPS"; // host name
        public bool IsGameRun = false;
        public bool DoneCheck = true;
        // Config basic game
        public string VersionGame = "";
        public int GameChannel = 0;
        public string GamePatchMetode = "";
        //public int GameType = 1; // 1=GS,2=SR

        // Extra
        Extra.Discord discord = new Extra.Discord();

        // Game
        public Game.Genshin.Settings? settings_genshin = null;

        //KeyGS key;
        Patch? get_version = null;

        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Loading....");

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
                Console.WriteLine("Discord RPC enable");
                discord.Ready();
            }
            else
            {
                Console.WriteLine("Discord RPC disable");
            }

            notbootyet = false;
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
            Console.WriteLine("GetProfileServer_SelectedIndexChanged " + get_select_profile);
            LoadProfile(get_select_profile);
        }

        private void GetTypeGame_SelectedIndexChanged(object? sender, EventArgs e)
        {
            default_profile.game.type = (GameType)GetTypeGame.SelectedItem;
        }

        public void LoadConfig(string load_by)
        {
            configdata = Json.Config.LoadConfig();

            Console.WriteLine("load config by " + load_by);

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
                    Console.WriteLine("Set index " + i + " name profile " + configdata.profile_default);
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
                Console.WriteLine("No profile");
                return;
            }

            Console.WriteLine("Profile: " + load_profile);

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
                Console.WriteLine("Server: " + default_profile.server.url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Profile error (" + e.Message + "), use default data");
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
                        Console.WriteLine("Profile save: " + name_save);
                        configdata.profile[indexToUpdate] = tmp_profile;
                    }
                    else
                    {
                        Console.WriteLine("Add new profile: " + name_save);
                        configdata.profile.Add(tmp_profile);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error save config (" + ex.Message + "), so reload it");
                    configdata = new Json.Config() { profile = new List<Profile>() { tmp_profile } };
                }


                configdata.profile_default = name_save;

                File.WriteAllText(Json.Config.ConfigPath, JsonConvert.SerializeObject(configdata));

                Console.WriteLine("Done save config...");

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
            var cst_gamefile = Set_LA_GameFile.Text;
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
                    Console.WriteLine("progress tes");
                }

                // if server is official 
                if (set_server_host == "official")
                {
                    patch = false;
                }
                else
                {
                    if (get_version != null && get_version.nosupport != "")
                    {
                        MessageBox.Show(get_version.nosupport, "Game version not supported");
                        Process.Start(new ProcessStartInfo(API.WEB_LINK) { UseShellExecute = true });
                        return;
                    }
                }

                // run patch
                var tes = PatchGame(patch, true, GamePatchMetode, GameChannel);
                if (!string.IsNullOrEmpty(tes))
                {
                    if (tes.Contains("corrupted"))
                    {
                        MessageBox.Show("Looks like you're using an unsupported version, try updating or downgrade game data to latest version", "Game version not supported");
                        Process.Start(new ProcessStartInfo(API.WEB_LINK) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show(tes, "Error Patch");
                    }
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
                        proxy = new Proxy(set_proxy_port, set_server_host);
                        if (!proxy.Start())
                        {
                            MessageBox.Show("Maybe port is already use or Windows Firewall does not allow using port " + set_proxy_port + " or Windows Update sometimes takes that range", "Failed Start...");
                            proxy = null;
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Proxy is ignored, because you turned it off");
                    }
                }
                else
                {
                    Console.WriteLine("Proxy is ignored, because use official server");
                }
            }
            else
            {
                Console.WriteLine("Proxy is still running...");
            }

            // For Cheat (tmp)
            if (isCheat)
            {
                Console.WriteLine("Cheat enable");
                try
                {
                    var get_file_cheat = API.GetCheat(selectedGame, GameChannel, VersionGame, cst_gamefile);
                    if (get_file_cheat == null)
                    {
                        MessageBox.Show("No update for this version so far or check console");
                        return;
                    }
                    cst_gamefile = get_file_cheat.launcher;
                    WatchCheat = Path.GetFileNameWithoutExtension(cst_gamefile);
                    Console.WriteLine($"RUN: Monitor {WatchCheat} at {cst_gamefile}");

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
                    FileName = cst_gamefile
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
                Console.WriteLine("Progress is still running...");
            }
        }

        public bool CheckVersionGame(GameType game_type)
        {
            var cst_folder_game = Set_LA_GameFolder.Text;

            // If user doesn't have a game config folder, try searching for it automatically
            if (String.IsNullOrEmpty(cst_folder_game))
            {

                var Get_Launcher = GetLauncherPath(game_type);
                Console.WriteLine("Folder Launcher: " + Get_Launcher);

                if (string.IsNullOrEmpty(Get_Launcher))
                {
                    // If there is no launcher
                    Console.WriteLine("Please find game install folder!");
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
                Console.WriteLine("Please find game install folder!");
                return false;
            }
            if (!Directory.Exists(cst_folder_game))
            {
                Console.WriteLine("Please find game install folder! (2)");
                return false;
            }

            Console.WriteLine("Folder Game: " + cst_folder_game);

            string cn = Path.Combine(cst_folder_game, "YuanShen.exe");
            string os = Path.Combine(cst_folder_game, "GenshinImpact.exe");
            if (game_type == GameType.StarRail)
            {
                cn = Path.Combine(cst_folder_game, "StarRail.exe"); // todo
                os = Path.Combine(cst_folder_game, "StarRail.exe");
            }

            // Path
            string PathfileGame;
            string PathMetadata;
            string PathUA;

            if (game_type == GameType.GenshinImpact)
            {
                // Pilih Channel
                if (File.Exists(cn))
                {
                    // Jika game versi cina
                    WatchFile = "YuanShen";
                    GameChannel = 2;
                    PathfileGame = cn;
                    PathMetadata = Path.Combine(cst_folder_game, "YuanShen_Data", "Managed", "Metadata");
                    PathUA = Path.Combine(cst_folder_game, "YuanShen_Data", "Native");
                }
                else if (File.Exists(os))
                {
                    // jika game versi global
                    WatchFile = "GenshinImpact";
                    GameChannel = 1;
                    PathfileGame = os;
                    PathMetadata = Path.Combine(cst_folder_game, "GenshinImpact_Data", "Managed", "Metadata");
                    PathUA = Path.Combine(cst_folder_game, "GenshinImpact_Data", "Native");
                }
                else
                {
                    // jika game versi tidak di dukung atau tidak ada file
                    Console.WriteLine("No game files found!!!");
                    return false;
                }

                // Settings
                try
                {
                    settings_genshin = new Game.Genshin.Settings(GameChannel);
                    if (settings_genshin != null)
                    {
                        Console.WriteLine("Game Text Language: " + settings_genshin.GetGameLanguage());
                        Console.WriteLine("Game Voice Language: " + settings_genshin.GetVoiceLanguageID());
                        //Console.WriteLine("JSON: " + settings_genshin.GetDataGeneralString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error getting game settings: " + ex.ToString());
                }

            }
            else
            {
                // jika game versi global
                WatchFile = "StarRail";
                GameChannel = 1;
                PathfileGame = os;
                PathMetadata = Path.Combine(cst_folder_game, "StarRail_Data", "il2cpp_data", "Metadata");
                PathUA = Path.Combine(cst_folder_game); // maybe GameAssembly?
            }

            // Check MD5 Game
            string Game_LOC_Original_MD5 = Tool.CalculateMD5(PathfileGame);

            // Check MD5 in Server API
            get_version = API.GetMD5Game(Game_LOC_Original_MD5, game_type);
            if (get_version == null)
            {
                //0.0.0
                Console.WriteLine("No Support Game with MD5: " + Game_LOC_Original_MD5 + " (Send this log to admin)");
                return false;
            }

            VersionGame = get_version.version;

            if (VersionGame == "0.0.0")
            {
                Console.WriteLine("Version not supported: MD5 " + Game_LOC_Original_MD5);

                Set_Metadata_Folder.Text = "";
                Set_UA_Folder.Text = "";
                Set_LA_GameFile.Text = "";

                Get_LA_Version.Text = "Version: Unknown";
                Get_LA_CH.Text = "Channel: Unknown";
                Get_LA_REL.Text = "Release: Unknown";
                Get_LA_Metode.Text = "Metode: Unknown";
                Get_LA_MD5.Text = "MD5: Unknown";

                return false;
            }

            var get_channel = get_version.channel;
            var get_metode = "None"; // no need patch
            if (get_version.patched != null)
            {
                get_metode = get_version.patched.metode;
            }

            // Set Folder Patch
            Set_Metadata_Folder.Text = PathMetadata;
            Set_UA_Folder.Text = PathUA;
            Set_LA_GameFile.Text = PathfileGame;

            // IF ALL OK
            Set_LA_GameFolder.Text = cst_folder_game;

            // Set Version
            Get_LA_Version.Text = "Version: " + get_version.version;
            Get_LA_CH.Text = "Channel: " + get_channel;
            Get_LA_REL.Text = "Release: " + get_version.release;
            Get_LA_Metode.Text = "Metode: " + get_metode;

            var md5_ori = "?";

            // Pilih Metode
            if (get_version.original != null)
            {
                if (get_metode == "Metadata")
                {
                    if (get_channel == "CN")
                    {
                        md5_ori = get_version.original.md5_check.cn.metadata;
                    }
                    if (get_channel == "OS")
                    {
                        md5_ori = get_version.original.md5_check.os.metadata;
                    }
                }
                else if (get_metode == "UserAssembly")
                {
                    if (get_channel == "CN")
                    {
                        md5_ori = get_version.original.md5_check.cn.userassembly;
                    }
                    if (get_channel == "OS")
                    {
                        md5_ori = get_version.original.md5_check.os.userassembly;
                    }
                }
            }

            Get_LA_MD5.Text = "MD5: " + md5_ori;

            Console.WriteLine("Currently using version game " + VersionGame);

            Console.WriteLine("Folder PathMetadata: " + PathMetadata);
            Console.WriteLine("File Game: " + PathfileGame);

            Console.WriteLine("MD5 Game Currently: " + Game_LOC_Original_MD5);

            GamePatchMetode = get_metode;

            return true;
        }

        public string PatchGame(bool patchit = true, bool online = true, string metode = "", int ch = 1)
        {
            // Check Folder Game
            var cst_folder_game = Set_LA_GameFolder.Text;
            if (String.IsNullOrEmpty(cst_folder_game))
            {
                return "No game folder found (1)";
            }
            if (!Directory.Exists(cst_folder_game))
            {
                return "No game folder found (2)";
            }

            if (get_version == null)
            {
                return "Can't find version, try clicking 'Get Key' in config tab";
            }

            // Check version
            if (VersionGame == "0.0.0")
            {
                return "This Game Version is not compatible with this method patch";
            }

            /*
            if (get_version.patched == null)
            {
                return "Can't find config patch cloud";
            }

            if (get_version.original == null)
            {
                return "Can't find config original cloud";
            }
            */

            // API STUFF
            var use_metode = "";
            if (get_version.patched != null)
            {
                use_metode = get_version.patched.metode;
            }

            var use_channel = get_version.channel;

            // LOCALHOST            
            string MD5_UA_API_Original = "";
            string MD5_UA_API_Patched = "";
            string MD5_Metadata_API_Original = "";
            string MD5_Metadata_API_Patched = "";
            string MD5_API_Patched = "";

            var DL_Patch = "";
            if (get_version.patched != null)
            {
                DL_Patch = get_version.patched.resources + "Patch/";
            }

            var DL_Original = "";
            if (get_version.original != null)
            {
                DL_Original = get_version.original.resources;
            }

            var Original_file_MA = "";
            var Original_file_UA = "";

            var key_to_patch = "";
            var key_to_find = "";

            if (!String.IsNullOrEmpty(use_metode))
            {
                // Select Metode (via API Cloud)
                if (use_channel == "OS")
                {
                    MD5_UA_API_Original = get_version.original?.md5_check.os.userassembly.ToUpper() ?? string.Empty;
                    MD5_UA_API_Patched = get_version.patched?.md5_vaild.os.ToUpper() ?? string.Empty;

                    MD5_Metadata_API_Original = get_version.original?.md5_check.os.metadata ?? string.Empty;
                    MD5_Metadata_API_Patched = get_version.patched?.md5_vaild.os.ToUpper() ?? string.Empty;

                    key_to_patch = get_version.patched?.key_patch;
                    key_to_find = get_version.original?.key_find.os;

                    Original_file_MA = DL_Original + "GenshinImpact_Data/Managed/Metadata/global-metadata.dat";
                    Original_file_UA = DL_Original + "GenshinImpact_Data/Native/UserAssembly.dll";
                    MD5_API_Patched = get_version.patched?.md5_vaild.os.ToUpper() ?? string.Empty;

                }
                else if (use_channel == "CN")
                {
                    MD5_UA_API_Original = get_version.original?.md5_check.cn.userassembly.ToUpper() ?? string.Empty;
                    MD5_UA_API_Patched = get_version.patched?.md5_vaild.cn.ToUpper() ?? string.Empty;

                    MD5_Metadata_API_Original = get_version.original?.md5_check.cn.metadata ?? string.Empty;
                    MD5_Metadata_API_Patched = get_version.patched?.md5_vaild.cn.ToUpper() ?? string.Empty;

                    key_to_patch = get_version.patched?.key_patch;
                    key_to_find = get_version.original?.key_find.cn;

                    Original_file_MA = DL_Original + "YuanShen_Data/Managed/Metadata/global-metadata.dat";
                    Original_file_UA = DL_Original + "YuanShen_Data/Native/UserAssembly.dll";
                    MD5_API_Patched = get_version.patched?.md5_vaild.os.ToUpper() ?? string.Empty;
                }
                else
                {
                    return "This Game Version is not compatible with Any Method Patch";
                }

                // >> Make sure MD5 API is not empty <<

                if (metode != "RSA")
                {
                    if (String.IsNullOrEmpty(MD5_UA_API_Patched))
                    {
                        return "Game version is not supported (3)";
                    }
                    if (String.IsNullOrEmpty(MD5_Metadata_API_Patched))
                    {
                        return "Game version is not supported (1)";
                    }
                }

                if (String.IsNullOrEmpty(MD5_UA_API_Original))
                {
                    return "Game version is not supported (4)";
                }
                if (String.IsNullOrEmpty(MD5_Metadata_API_Original))
                {
                    return "Game version is not supported (2)";
                }

                // >> All <<

                // Check Folder UA
                var cst_folder_UA = Set_UA_Folder.Text;
                var cst_folder_Game = Set_LA_GameFolder.Text;

                if (String.IsNullOrEmpty(cst_folder_UA))
                {
                    return "No UserAssembly folder found (1)";
                }
                if (!Directory.Exists(cst_folder_UA))
                {
                    return "No UserAssembly folder found (2)";
                }
                // Check file UserAssembly
                string PathfileUA_Currently = Path.Combine(cst_folder_UA, "UserAssembly.dll");
                string PathfileUA_Patched = Path.Combine(cst_folder_UA, "UserAssembly-patched.dll");
                string PathfileUA_Original = Path.Combine(cst_folder_UA, "UserAssembly-original.dll");
                // Check MD5 local (First time)
                string MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                string MD5_UA_LOC_Patched = Tool.CalculateMD5(PathfileUA_Patched);
                string MD5_UA_LOC_Original = Tool.CalculateMD5(PathfileUA_Original);

                // Check folder Metadata
                var cst_folder_metadata = Set_Metadata_Folder.Text;
                if (String.IsNullOrEmpty(cst_folder_metadata))
                {
                    return "No MetaData folder found (1)";
                }
                if (!Directory.Exists(cst_folder_metadata))
                {
                    return "No MetaData folder found (2)";
                }
                // Check file MetaData
                string PathfileMetadata_Currently = Path.Combine(cst_folder_metadata, "global-metadata.dat");
                string PathfileMetadata_Patched = Path.Combine(cst_folder_metadata, "global-metadata-patched.dat");
                string PathfileMetadata_Original = Path.Combine(cst_folder_metadata, "global-metadata-original.dat");
                // Get MD5 local Metadata (First time)
                string MD5_Metadata_LOC_Currently = Tool.CalculateMD5(PathfileMetadata_Currently);
                string MD5_Metadata_LOC_Original = Tool.CalculateMD5(PathfileMetadata_Original);
                string MD5_Metadata_LOC_Patched = Tool.CalculateMD5(PathfileMetadata_Patched);

                // Two-method verification even when using offline mode

                // >> If UserAssembly is broken <<
                var download_ua = false;
                if (!File.Exists(PathfileUA_Currently))
                {
                    // Check if found file original
                    if (File.Exists(PathfileUA_Original))
                    {
                        // Check if API Original same with Original LOC
                        if (MD5_UA_API_Original == MD5_UA_LOC_Original)
                        {
                            try
                            {
                                File.Copy(PathfileUA_Original, PathfileUA_Currently, true);
                                MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                                Console.WriteLine("We copy PathfileUA_Original to PathfileUA_Currently (UserAssembly) (33)");
                            }
                            catch (Exception)
                            {
                                // skip
                                return "Error copy (1)";
                            }
                        }
                        else
                        {
                            Console.WriteLine("Download UserAssembly, because PathfileUA_Original with md5 " + MD5_UA_LOC_Original + " does not match " + MD5_UA_API_Original + " (5)");
                            download_ua = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Download UserAssembly, because file PathfileUA_Original was not found");
                        download_ua = true;
                    }
                }
                else
                {
                    // If file is found and original file doesn't match currently (Make sure current data is really original before patch)
                    if (MD5_UA_API_Original != MD5_UA_LOC_Currently)
                    {
                        // Check if found file original
                        if (File.Exists(PathfileUA_Original))
                        {
                            // Check if API Original same with Original LOC
                            if (MD5_UA_API_Original == MD5_UA_LOC_Original)
                            {
                                try
                                {
                                    File.Copy(PathfileUA_Original, PathfileUA_Currently, true);
                                    MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                                    Console.WriteLine("We copy PathfileUA_Original to PathfileUA_Currently (UserAssembly) (6)");
                                }
                                catch (Exception)
                                {
                                    // skip
                                    return "Error copy (2)";
                                }
                            }
                            else
                            {
                                // download if Original file unvaild
                                Console.WriteLine("Download UserAssembly in 'Currently', because 'PathfileUA_Original' it doesn't match " + MD5_UA_API_Original + " with " + MD5_UA_LOC_Original + " (7)");
                                download_ua = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Download UserAssembly in 'Currently' because file PathfileUA_Original not found and it doesn't match " + MD5_UA_API_Original + " with " + MD5_UA_LOC_Currently + " Currently file (8)");
                            download_ua = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skip download UserAssembly, it's up-to-date (4)");
                    }
                }
                // if need download ua
                if (download_ua)
                {
                    try
                    {
                        if (!patchit)
                        {
                            return "Unable to download files " + Original_file_UA + " in a closed game state, please try again or download manual and put it in " + PathfileUA_Currently;
                        }
                        var CEKDL1 = new Download(Original_file_UA, PathfileUA_Currently);
                        if (CEKDL1.ShowDialog() != DialogResult.OK)
                        {
                            return "Get Original UserAssembly failed";
                        }
                        else
                        {
                            MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                            Console.WriteLine("Currently UserAssembly: " + MD5_UA_LOC_Currently);
                        }
                    }
                    catch (Exception exx)
                    {
                        return "Error Get Original UserAssembly: " + exx.ToString();
                    }
                }
                else
                {
                    Console.WriteLine("Currently UserAssembly: " + MD5_UA_LOC_Currently);
                }

                // here current file should match so if original file is not found use current file to copy to original file
                if (!File.Exists(PathfileUA_Original))
                {
                    try
                    {
                        File.Copy(PathfileUA_Currently, PathfileUA_Original, true);
                        MD5_UA_LOC_Original = Tool.CalculateMD5(PathfileUA_Original);
                        Console.WriteLine("We copy file in PathfileUA_Currently to PathfileUA_Original files (22)");
                    }
                    catch (Exception)
                    {
                        // skip
                        return "Error copy PathfileUA_Currently to PathfileUA_Original (1)";
                    }
                }
                else
                {
                    // if file found (skip)               
                }


                // >> If Metadata is broken <<
                var download_metadata = false;
                if (!File.Exists(PathfileMetadata_Currently))
                {
                    // Check if found file original
                    if (File.Exists(PathfileMetadata_Original))
                    {
                        // Check if API Original same with Original LOC
                        if (MD5_Metadata_API_Original == MD5_Metadata_LOC_Original)
                        {
                            try
                            {
                                File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                MD5_Metadata_LOC_Currently = Tool.CalculateMD5(PathfileMetadata_Currently);
                                Console.WriteLine("We copy PathfileMetadata_Original to PathfileMetadata_Currently");
                            }
                            catch (Exception)
                            {
                                // skip
                                return "Error copy PathfileMetadata_Original to PathfileMetadata_Currently (111)";
                            }
                        }
                        else
                        {
                            // file not vaild so download
                            download_metadata = true;
                            Console.WriteLine("Download Metadata in Currently,because file Original with md5 " + MD5_Metadata_API_Original + " doesn't match " + MD5_Metadata_LOC_Original + " (5)");
                        }
                    }
                    else
                    {
                        // file not found, so download
                        download_metadata = true;
                        Console.WriteLine("Download Metadata, because file PathfileMetadata_Original was not found");
                    }
                }
                else
                {
                    // If file is found and original file doesn't match currently (Make sure current data is really original before patch)
                    if (MD5_Metadata_API_Original != MD5_Metadata_LOC_Currently)
                    {
                        // Check if found file original
                        if (File.Exists(PathfileMetadata_Original))
                        {
                            // Check if API Original same with Original LOC
                            if (MD5_Metadata_API_Original == MD5_Metadata_LOC_Original)
                            {
                                try
                                {
                                    File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                    MD5_Metadata_LOC_Currently = Tool.CalculateMD5(PathfileMetadata_Currently);
                                    Console.WriteLine("We copy file PathfileMetadata_Original to PathfileMetadata_Currently (6)");
                                }
                                catch (Exception)
                                {
                                    // skip
                                    return "Error copy PathfileMetadata_Original to PathfileMetadata_Currently (111)";
                                }
                            }
                            else
                            {
                                // file not vaild so download
                                download_metadata = true;
                                Console.WriteLine("Download Metadata in Currently, because PathfileMetadata_Original file does not match " + MD5_Metadata_API_Original + " with " + MD5_Metadata_LOC_Original + " (7)");
                            }
                        }
                        else
                        {
                            // file not found, so download
                            download_metadata = true;
                            Console.WriteLine("Download Metadata in Currently, because file PathfileMetadata_Original was not found (8)");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skip download Metadata, it's up-to-date (4)");
                    }
                }
                // if need download
                if (download_metadata)
                {
                    try
                    {
                        if (!patchit)
                        {
                            return "Unable to download files " + Original_file_MA + " in a closed game state, please try again or download manual and put it in " + PathfileMetadata_Currently;
                        }
                        var CEKDL2 = new Download(Original_file_MA, PathfileMetadata_Currently);
                        if (CEKDL2.ShowDialog() != DialogResult.OK)
                        {
                            return "Get Original Metadata failed";
                        }
                        else
                        {
                            MD5_Metadata_LOC_Currently = Tool.CalculateMD5(PathfileMetadata_Currently);
                            Console.WriteLine("Currently Metadata: " + MD5_Metadata_LOC_Currently);
                        }
                    }
                    catch (Exception exx)
                    {
                        return "Error Get Original Metadata: " + exx.ToString();
                    }
                }
                else
                {
                    Console.WriteLine("Currently Metadata: " + MD5_Metadata_LOC_Currently);
                }
                // here current file should match so if original file is not found use current file to copy to original file
                if (!File.Exists(PathfileMetadata_Original))
                {
                    try
                    {
                        File.Copy(PathfileMetadata_Currently, PathfileMetadata_Original, true);
                        MD5_Metadata_LOC_Original = Tool.CalculateMD5(PathfileMetadata_Original);
                        Console.WriteLine("We copy file in PathfileMetadata_Currently to PathfileMetadata_Original files (22)");
                    }
                    catch (Exception)
                    {
                        return "Error copy PathfileMetadata_Currently to PathfileMetadata_Original (1)";
                    }
                }
                else
                {
                    // if file found (skip)
                }

                // >>> Final Tes <<<

                //UA
                MD5_UA_LOC_Original = Tool.CalculateMD5(PathfileUA_Original);
                MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                if (MD5_UA_API_Original != MD5_UA_LOC_Original && MD5_UA_API_Original != MD5_UA_LOC_Currently)
                {
                    try
                    {
                        File.Delete(PathfileUA_Original);
                    }
                    catch (Exception)
                    {
                        // skip
                    }
                    return MD5_UA_LOC_Original + " and " + MD5_UA_LOC_Currently + " value should be " + MD5_UA_API_Original + ", This might happen because file is corrupted, try again or manual update. (g1)";
                }
                // Metadata
                MD5_Metadata_LOC_Original = Tool.CalculateMD5(PathfileMetadata_Original);
                MD5_Metadata_LOC_Currently = Tool.CalculateMD5(PathfileMetadata_Currently);
                if (MD5_Metadata_API_Original != MD5_Metadata_LOC_Original && MD5_Metadata_API_Original != MD5_Metadata_LOC_Currently)
                {
                    try
                    {
                        File.Delete(PathfileMetadata_Original);
                    }
                    catch (Exception)
                    {
                        // skip
                    }
                    return MD5_Metadata_LOC_Original + " and " + MD5_Metadata_LOC_Currently + " value should be " + MD5_Metadata_API_Original + ", This might happen because file is corrupted, try again or manual update. (g1)";
                }

                // It should be here that all files already verified, unless you want to check other vaild files again.

                if (use_metode == "UserAssembly")
                {
                    // >> UA <<

                    // debug
                    //online = false;                

                    if (online)
                    {
                        // If original backup file is not found.
                        if (!File.Exists(PathfileUA_Original))
                        {
                            return "Why does this pass (1)";
                        }

                        // If current UA file doesn't exist use UserAssembly-Original.dll
                        if (!File.Exists(PathfileUA_Currently))
                        {
                            return "Why does this pass (2)";
                        }

                        if (MD5_UA_API_Original != MD5_UA_LOC_Currently)
                        {
                            return "Why does this pass (3)";
                        }
                        else
                        {
                            if (patchit)
                            {
                                // if current file is original and need patch

                                var download_patch = false;
                                if (!File.Exists(PathfileUA_Patched))
                                {
                                    download_patch = true;
                                }
                                else
                                {
                                    // If UA_API_Patches_MD5 (patch file from api) matches UA_LOC_Patched_MD5 (current patch file)
                                    if (MD5_UA_API_Patched != MD5_UA_LOC_Patched)
                                    {
                                        download_patch = true;
                                    }
                                }

                                // If download_patch true, download it
                                if (download_patch)
                                {
                                    var DL2 = new Download(DL_Patch + "UserAssembly-patched.dll", PathfileUA_Patched);
                                    if (DL2.ShowDialog() != DialogResult.OK)
                                    {
                                        return "No Found Patch file....";
                                    }
                                    else
                                    {
                                        MD5_UA_LOC_Patched = Tool.CalculateMD5(PathfileUA_Patched);
                                    }
                                }

                                if (MD5_UA_API_Patched != MD5_UA_LOC_Patched)
                                {
                                    // Failed because file doesn't match from md5 api
                                    return "(UA) Your version Game is not supported, or it needs latest update or file is corrupted.";
                                }
                                else
                                {
                                    // Patch file                           
                                    try
                                    {
                                        File.Copy(PathfileUA_Patched, PathfileUA_Currently, true);
                                        MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);

                                        Console.WriteLine("Patch PathfileUA_Patched to PathfileUA_Currently done");
                                        download_patch = false;
                                    }
                                    catch (Exception x)
                                    {
                                        return "Failed Patch: " + x.ToString();
                                    }
                                }
                            }
                            else
                            {
                                // No Patch
                                Console.WriteLine("Skip, because file is original (x1)");
                            }
                        }
                    }
                    else
                    {
                        // Offline Mode UserAssembly
                        if (patchit)
                        {
                            // Use PathfileUA_Currently to backup PathfileUA_Original file, but since without md5 api we can't check is original or not.
                            if (!File.Exists(PathfileUA_Original))
                            {
                                return "Why does this pass (1-1)";
                            }

                            // Check PathfileMetadata_Currently if no found
                            if (!File.Exists(PathfileUA_Currently))
                            {
                                return "Why does this pass (1-2)";
                            }

                            // keep patch
                            var ManualUA = Game.Genshin.Patch.UserAssembly.Do(PathfileUA_Currently, PathfileUA_Patched, key_to_find!, key_to_patch!);
                            if (!String.IsNullOrEmpty(ManualUA))
                            {
                                return "Error Patch UserAssembly: " + ManualUA;
                            }

                            // If patch is successful
                            if (File.Exists(PathfileUA_Patched))
                            {
                                MD5_UA_LOC_Patched = Tool.CalculateMD5(PathfileUA_Patched);
                                try
                                {
                                    File.Copy(PathfileUA_Patched, PathfileUA_Currently, true);
                                    MD5_UA_LOC_Currently = Tool.CalculateMD5(PathfileUA_Currently);
                                    Console.WriteLine("Patch UserAssembly...");
                                }
                                catch (Exception ex)
                                {
                                    return "Failed copy PathfileUA_Patched to PathfileUA_Currently: " + ex.ToString();
                                }
                            }
                            else
                            {
                                return "Why does this pass (1-3)";
                            }
                        }
                        else
                        {
                            // No Patch
                            Console.WriteLine("Skip, because file is original (x2)");
                        }
                    }

                    Console.WriteLine("MD5 UA Currently: " + MD5_UA_LOC_Currently);
                    Console.WriteLine("MD5 UA Original: " + MD5_UA_LOC_Original);
                    Console.WriteLine("MD5 UA Patched: " + MD5_UA_LOC_Patched);
                }
                else if (metode == "Metadata")
                {
                    return "I'm tired Checking Metadata";
                }
                else if (metode == "None")
                {
                    Console.WriteLine("Skip patch...");
                }
                else if (metode == "RSA")
                {

                    var fileRSA = cst_folder_Game + "/version.dll";
                    var fileRSA_BK = cst_folder_Game + "/version.bk";

                    var fileRSA_Public = cst_folder_Game + "/PublicKey.txt";

                    // TODO: ADD KEY SERVER
                    File.WriteAllText(fileRSA_Public, "<RSAKeyValue><Modulus>xbbx2m1feHyrQ7jP+8mtDF/pyYLrJWKWAdEv3wZrOtjOZzeLGPzsmkcgncgoRhX4dT+1itSMR9j9m0/OwsH2UoF6U32LxCOQWQD1AMgIZjAkJeJvFTrtn8fMQ1701CkbaLTVIjRMlTw8kNXvNA/A9UatoiDmi4TFG6mrxTKZpIcTInvPEpkK2A7Qsp1E4skFK8jmysy7uRhMaYHtPTsBvxP0zn3lhKB3W+HTqpneewXWHjCDfL7Nbby91jbz5EKPZXWLuhXIvR1Cu4tiruorwXJxmXaP1HQZonytECNU/UOzP6GNLdq0eFDE4b04Wjp396551G99YiFP2nqHVJ5OMQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

                    Console.WriteLine("Find file: " + fileRSA);

                    var dl_rsa = false;
                    var RSAMD5 = "";

                    if (patchit)
                    {
                        if (File.Exists(fileRSA))
                        {
                            // check if source true
                            RSAMD5 = Tool.CalculateMD5(fileRSA);
                            if (RSAMD5 == MD5_API_Patched)
                            {
                                // if not found backup rsa key, copy dll to bk
                                if (!File.Exists(fileRSA_BK))
                                {
                                    Console.WriteLine("No found backup file rsa key so copy");
                                    File.Copy(fileRSA, fileRSA_BK, true);
                                }

                                Console.WriteLine("Skip download RSAKEY");
                            }
                            else
                            {
                                Console.WriteLine("file not same " + RSAMD5 + " download rsa key " + MD5_API_Patched);
                                dl_rsa = true;
                            }

                        }
                        else
                        {
                            if (File.Exists(fileRSA_BK))
                            {
                                if (RSAMD5 == MD5_API_Patched)
                                {
                                    Console.WriteLine("found backup file rsa key so copy");
                                    try
                                    {
                                        File.Copy(fileRSA_BK, fileRSA, true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        return "error1";
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("key rsa not same so just download it");
                                    dl_rsa = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No found file so just download it");
                                dl_rsa = true;
                            }
                        }
                    }
                    else
                    {
                        // remove file
                        try
                        {
                            if (File.Exists(fileRSA))
                            {
                                Console.WriteLine("Remove file rsa key");
                                File.Delete(fileRSA);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return "error";
                        }
                    }

                    if (dl_rsa)
                    {
                        var DL2 = new Download(DL_Patch + "RSAPatch.dll", fileRSA);
                        if (DL2.ShowDialog() != DialogResult.OK)
                        {
                            return "No Found Patch file....";
                        }
                        else
                        {
                            RSAMD5 = Tool.CalculateMD5(fileRSA);
                        }
                    }

                    Console.WriteLine("RSA MD5: " + RSAMD5);
                    //return "TODO: "+ DL_Patch;
                }
                else
                {
                    return "No other method found: " + metode;
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
                if (!CheckVersionGame(default_profile.game.type))
                {
                    MessageBox.Show("You have set folder manually but we can't detect this game version yet, maybe because it's not supported yet so please download it on our official website with the currently supported version.");
                    Process.Start(new ProcessStartInfo(API.WEB_LINK + "/game/" + default_profile.game.type.SEOUrl()) { UseShellExecute = true });
                }
            }
            else
            {
                MessageBox.Show("No game folder found");
            }
        }

        public void CheckUpdate()
        {
            Console.WriteLine("Cek update...");
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
            Console.WriteLine("GetLauncherPath: " + version.GetStringValue());

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
                    StopGame(); // this shouldn't be necessary but just let it be
                    var tes = PatchGame(false, true, GamePatchMetode, GameChannel);
                    if (!string.IsNullOrEmpty(tes))
                    {
                        Console.WriteLine(tes);
                    }
                    DoneCheck = true;

                    if (Config_Discord_Enable.Checked)
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

                if (Config_Discord_Enable.Checked)
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
            if (proxy != null)
            {
                proxy.Stop();
                proxy = null;
                Console.WriteLine("Proxy Stop....");
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
                        Console.WriteLine("EndTask Game: " + WatchFile);
                    }
                    else
                    {
                        Console.WriteLine("EndTask not supported");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
                            stIsRunProxy.Text = "Status: ON (Internal)";
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
                    Console.WriteLine("Does not support proxy check!");
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
            Console.WriteLine("Still PR :)");
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

        private void Server_DL_DB_Click(object sender, EventArgs e)
        {

        }

        private void Server_DL_GC_Click(object sender, EventArgs e)
        {

        }

        private void Extra_Cheat_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Extra_Enable_RPC_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_RPC.Checked)
            {
                Console.WriteLine("Enable RPC");
                discord.Ready();
            }
            else
            {
                Console.WriteLine("Disable RPC. This may take a few seconds");
                discord.Stop();
            }
        }

        private void btchooseupdate_Click(object sender, EventArgs e)
        {
            string gpath = Set_LA_GameFolder.Text;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (!string.IsNullOrEmpty(gpath))
            {
                openFileDialog.InitialDirectory = gpath;
            }
            else
            {
                openFileDialog.InitialDirectory = @"C:\";
            }

            openFileDialog.Filter = "Update Files (*.zip)|*.zip";

            openFileDialog.Title = "Select an update file (HDIFF)";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;

                if (!string.IsNullOrEmpty(selectedFileName))
                {
                    tbx_update.Text = selectedFileName;
                    string gamePath = Set_LA_GameFile.Text;
                }
                else
                {
                    MessageBox.Show("Select a valid file!");
                }
            }
        }
        private void ExtractZip(string zipFilePath, string extractionPath, ProgressBar progressBar)
        {
            try
            {
                using (var fileStream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                using (var zipInputStream = new ZipInputStream(fileStream))
                {
                    long totalSize = fileStream.Length;
                    long bytesRead = 0;

                    ZipEntry entry;
                    while ((entry = zipInputStream.GetNextEntry()) != null)
                    {
                        if (!entry.IsDirectory)
                        {
                            string entryFileName = Path.Combine(extractionPath, entry.Name);
                            string entryDirectory = Path.GetDirectoryName(entryFileName);

                            if (!Directory.Exists(entryDirectory))
                            {
                                Directory.CreateDirectory(entryDirectory);
                            }

                            using (var entryStream = new FileStream(entryFileName, FileMode.Create, FileAccess.Write))
                            {
                                byte[] buffer = new byte[4096];
                                int count;
                                while ((count = zipInputStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    entryStream.Write(buffer, 0, count);
                                    bytesRead += count;

                                    // this took so much to figure out (i dont know sh*t 'bout math)
                                    int progress = (int)Math.Min(100, ((double)bytesRead / totalSize * 100));
                                    progressBar.Invoke((MethodInvoker)delegate { progressBar.Value = progress; });
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Successfully extracted zip");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting update file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        static bool HasContent(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            if (files.Length > 0)
            {
                return true;
            }


            string[] subdirectories = Directory.GetDirectories(folderPath);
            return subdirectories.Length > 0;
        }

        async private void btnstartUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbx_update.Text))
            {
                btnstartUpdate.Enabled = false;
                Console.WriteLine("[Hdiff] Start!");
                txt_statusUpd.Text = "Status: Extracting...";
                string updPath = tbx_update.Text;
                string tempPath = Path.Combine(Set_LA_GameFolder.Text, "hdiff_temp");

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(Path.Combine(Set_LA_GameFolder.Text, "hdiff_temp"));
                }

                if (!HasContent(tempPath))
                {
                    // run asynchronously this function, to prevent the stupid freezing >:(
                    await Task.Run(() => ExtractZip(updPath, tempPath, progressBar1));
                }


                txt_statusUpd.Text = "Status: Analyzing...";

                string deleteFilesPath = Path.Combine(tempPath, "deletefiles.txt");

                if (!Path.Exists(deleteFilesPath))
                {
                    MessageBox.Show("Error: corrupt or invalid update.");
                    txt_statusUpd.Text = "Status: Error.";
                    return;
                }

                try
                {
                    txt_statusUpd.Text = "Status: Step 1 | Deletion";

                    string[] filesToDelete = File.ReadAllLines(deleteFilesPath);

                    foreach (string filePath in filesToDelete)
                    {
                        string fullPathToDelete = Path.Combine(Set_LA_GameFolder.Text, filePath);

                        if (File.Exists(fullPathToDelete))
                        {
                            File.Delete(fullPathToDelete);
                            Console.WriteLine($"[Hdiff] Deleted: {fullPathToDelete}");
                        }
                        else
                        {
                            Console.WriteLine($"[Hdiff] File not found: {fullPathToDelete}");
                        }
                    }

                    Console.WriteLine("[Hdiff] File deletion process completed.");

                    txt_statusUpd.Text = "Status: Step 2 | Replacement";
                    try
                    {
                        string gamePath = Set_LA_GameFolder.Text;
                        string subdirectoryPath = Path.Combine(gamePath, "hdiff_temp");
                        Console.WriteLine(subdirectoryPath);

                        string[] subdirectoryFiles = Directory.GetFiles(subdirectoryPath);

                        string[] mainDirectoryFiles = Directory.GetFiles(gamePath);

                        var commonFiles = subdirectoryFiles.Intersect(mainDirectoryFiles);

                        foreach (var commonFile in commonFiles)
                        {
                            File.Delete(Path.Combine(gamePath, Path.GetFileName(commonFile)));
                        }

                        foreach (var file in subdirectoryFiles)
                        {
                            File.Copy(file, Path.Combine(gamePath, Path.GetFileName(file)), true);
                        }

                        txt_statusUpd.Text = "Status: Complete.";
                        Console.WriteLine("[Hdiff] Done replacing.");
<<<<<<< HEAD
                        progressBar1.Value = 0;
=======
                        Directory.Delete(subdirectoryPath, true);
>>>>>>> 5890426935983e5deb48c2b823141e4b1675bb71
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Hdiff] Error: {ex.Message}");
                        progressBar1.Value = 0;
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
