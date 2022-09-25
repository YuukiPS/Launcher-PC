using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using YuukiPS_Launcher.json;

namespace YuukiPS_Launcher
{
    public partial class Main : Form
    {
        Config configdata = new Config();

        // Main Function
        private ProxyController? proxy;
        private Process? progress;

        // Server List
        Thread thServerList;
        List<List> ListServer;

        // Folder
        public static string CurrentlyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");

        private static string DataConfig = Path.Combine(CurrentlyPath, "data");
        private static string Modfolder = Path.Combine(CurrentlyPath, "mod");

        public string ConfigPath = Path.Combine(DataConfig, "config.json");

        // Stats default
        public string WatchFile = "";
        public string HostName = "Unknown";
        public bool IsGameRun = false;
        public bool DoneCheck = true;
        public string VersionGame = "";
        public int GameChannel = 0;
        public int GameMetode = 1;

        // Extra
        Extra.Discord discord = new Extra.Discord();

        public void LoadConfig()
        {
            // Create missing folder
            Directory.CreateDirectory(DataConfig);
            Directory.CreateDirectory(Modfolder);

            if (File.Exists(ConfigPath))
            {
                string data = File.ReadAllText(ConfigPath);
                try
                {
                    configdata = JsonConvert.DeserializeObject<Config>(data);
                    if (configdata != null)
                    {
                        // load config
                        Set_LA_GameFolder.Text = configdata.Game_Path;
                        GetHost.Text = configdata.Hostdefault;
                        GetPort.Text = configdata.ProxyPort.ToString();

                        checkModeOnline.Checked = configdata.MetodeOnline;
                        CheckProxyUseHTTPS.Checked = configdata.HostHTTPS;
                        Extra_AkebiGC.Checked = configdata.extra.Akebi;

                        Console.WriteLine("load config...");
                    }
                    else
                    {
                        Console.WriteLine("No config load...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error load config: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No Config file found...");
            }

            // Check Game Version
            CheckVersionGame();
        }
        public void SaveConfig()
        {
            try
            {
                // save config
                configdata.Game_Path = Set_LA_GameFolder.Text;
                configdata.Hostdefault = GetHost.Text;

                int myInt;
                bool isValid = int.TryParse(GetPort.Text, out myInt);
                if (isValid)
                {
                    configdata.ProxyPort = myInt;
                }

                configdata.MetodeOnline = checkModeOnline.Checked;
                configdata.HostHTTPS = CheckProxyUseHTTPS.Checked;
                configdata.extra.Akebi = Extra_AkebiGC.Checked;

                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(configdata));

                Console.WriteLine("Done save config...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error save config: " + ex.Message);
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Main_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Loading....");

            // Load config and check version game
            LoadConfig();

            // Before starting make sure proxy is turned off
            CheckProxy(true);

            // Check Update
            CheckUpdate();

            // Server List
            GetServerList();
            UpdateServerListTimer();

            // Extra
            discord.Ready();
        }

        public bool CheckVersionGame()
        {
            var cst_folder_game = Set_LA_GameFolder.Text;
            // Jika user tidak punya game folder
            if (String.IsNullOrEmpty(cst_folder_game))
            {
                // cari otomatis launcher
                var Get_Launcher = GetLauncherPath();
                Console.WriteLine("Folder Launcher: " + Get_Launcher);

                // Jika tidak ada launcher
                if (string.IsNullOrEmpty(Get_Launcher))
                {
                    Console.WriteLine("Please find game install folder!");
                    return false;
                }
                else
                {
                    // jika ada launcher, cari game path
                    cst_folder_game = GetGamePath(Get_Launcher);
                }
            }

            // Check sekali lagi
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

            // Path
            string PathfileGame;
            string PathMetadata;
            string PathUA;

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

            // Check MD5 Game
            string Game_LOC_Original_MD5 = CalculateMD5(PathfileGame);

            // Check MD5 in Server API
            VersionGenshin get_version = API.GetMD5VersionGS(Game_LOC_Original_MD5);
            if (get_version == null)
            {
                //0.0.0
                Console.WriteLine("Could not find version via API Check");
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

            // Set Folder Patch
            Set_Metadata_Folder.Text = PathMetadata;
            Set_UA_Folder.Text = PathUA;
            Set_LA_GameFile.Text = PathfileGame;

            // IF ALL OK
            Set_LA_GameFolder.Text = cst_folder_game;

            // Set Version
            Get_LA_Version.Text = "Version: " + get_version.version;
            Get_LA_CH.Text = "Channel: " + get_version.channel;
            Get_LA_REL.Text = "Release: " + get_version.release;
            Get_LA_Metode.Text = "Metode: " + get_version.metode;
            Get_LA_MD5.Text = "MD5: " + get_version.md5;

            // Pilih Metode
            if (get_version.metode == "Metadata")
            {
                GameMetode = 1;
            }
            else if (get_version.metode == "UserAssembly")
            {
                GameMetode = 2;
            }

            Console.WriteLine("Currently using version game " + VersionGame);

            Console.WriteLine("Folder PathMetadata: " + PathMetadata);
            Console.WriteLine("File Game: " + PathfileGame);

            Console.WriteLine("MD5 Game Currently: " + Game_LOC_Original_MD5);

            return true;
        }

        public string PatchGame(bool patchit = true, bool online = true, int metode = 1, int ch = 1)
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

            // Get last key
            KeyGS last_key_api = API.GSKEY();
            if (last_key_api == null)
            {
                return "Error Get Key";
            }

            // Check version
            if (VersionGame == "0.0.0")
            {
                return "This Game Version is not compatible with this method patch";
            }

            if (metode == 2)
            {
                // TODO: online method not yet available
                online = false;

                // Check Folder UA
                var cst_folder_UA = Set_UA_Folder.Text;
                if (String.IsNullOrEmpty(cst_folder_UA))
                {
                    return "No UserAssembly folder found (1)";
                }
                if (!Directory.Exists(cst_folder_UA))
                {
                    return "No UserAssembly folder found (2)";
                }

                string PathfileUA_Currently = Path.Combine(cst_folder_UA, "UserAssembly.dll");
                string PathfileUA_Patched = Path.Combine(cst_folder_UA, "UserAssembly-patched.dll");
                string PathfileUA_Original = Path.Combine(cst_folder_UA, "UserAssembly-original.dll");

                // Check MD5 local (First time)
                string MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);
                string MD5_UA_LOC_Patched = CalculateMD5(PathfileUA_Patched);
                string MD5_UA_LOC_Original = CalculateMD5(PathfileUA_Original);

                if (online)
                {
                    // API
                    string MD5_UA_API_Original;
                    string MD5_UA_API_Patched;

                    // Select CH
                    var cno = "Global";
                    if (ch == 1)
                    {
                        // Global
                        MD5_UA_API_Original = last_key_api.Original.UserAssembly.md5_os.ToUpper();
                        MD5_UA_API_Patched = last_key_api.Patched.UserAssembly.md5_os.ToUpper();
                    }
                    else if (ch == 2)
                    {
                        // Chinese
                        MD5_UA_API_Original = last_key_api.Original.UserAssembly.md5_cn.ToUpper();
                        MD5_UA_API_Patched = last_key_api.Patched.UserAssembly.md5_cn.ToUpper();
                        cno = "Chinese";
                    }
                    else
                    {
                        return "This Game Version is not compatible with Method Patch UserAssembly";
                    }

                    return "TODO: not yet available";
                }
                else
                {
                    // Offline Mode UserAssembly
                    if (patchit)
                    {
                        // Use PathfileUA_Currently to backup PathfileUA_Original file, but since without md5 api we can't check is original or not.
                        if (!File.Exists(PathfileUA_Original))
                        {
                            try
                            {
                                File.Copy(PathfileUA_Currently, PathfileUA_Original, true);
                                MD5_UA_LOC_Original = CalculateMD5(PathfileUA_Original);

                                Console.WriteLine("Backup UserAssembly Original");
                            }
                            catch (Exception ex)
                            {
                                return "UserAssembly backup failed because file not found in offline mode: " + ex.ToString();
                            }
                        }

                        // Check PathfileMetadata_Currently if no found
                        if (!File.Exists(PathfileUA_Currently))
                        {
                            if (File.Exists(PathfileUA_Original))
                            {
                                File.Copy(PathfileUA_Original, PathfileUA_Currently, true);
                                MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);

                                Console.WriteLine("We detected that you did not have files (Currently) so we returned them with Original");
                            }
                            else
                            {
                                return "Can't find Original UA file in offline mode (0)";
                            }
                        }
                        else
                        {
                            // jika PathfileMetadata_Currently ada coba cek PathfileMetadata_Original apakah sama
                            if (File.Exists(PathfileUA_Original))
                            {
                                MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);
                                MD5_UA_LOC_Original = CalculateMD5(PathfileUA_Original);
                                if (MD5_UA_LOC_Currently != MD5_UA_LOC_Original)
                                {
                                    File.Copy(PathfileUA_Original, PathfileUA_Currently, true);
                                    MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);

                                    Console.WriteLine("We detect you have non-original (Currently) files so we return them with Original");
                                }
                            }
                            else
                            {
                                return "Can't find Original UA file in offline mode (1)";
                            }
                        }

                        var ManualUA = "???";
                        // Select CH
                        if (ch == 1)
                        {
                            ManualUA = patch.UserAssembly.Do(PathfileUA_Currently, PathfileUA_Patched, last_key_api.Original.MetaData.key2_os, last_key_api.Patched.UserAssembly.key2);
                        }
                        else if (ch == 2)
                        {
                            ManualUA = patch.UserAssembly.Do(PathfileUA_Currently, PathfileUA_Patched, last_key_api.Original.MetaData.key2_cn, last_key_api.Patched.UserAssembly.key2);
                        }
                        if (!String.IsNullOrEmpty(ManualUA))
                        {
                            return "Error Patch UserAssembly: " + ManualUA;
                        }
                        /* 
                        if (!File.Exists(PathfileUA_Patched))
                        {
                            Console.WriteLine("No manual patch found so let's patch");                            
                        }
                        */

                        // Patch PathfileUA_Patched ke PathfileUA_Currently
                        if (File.Exists(PathfileUA_Patched))
                        {
                            try
                            {
                                File.Copy(PathfileUA_Patched, PathfileUA_Currently, true);
                                MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);

                                Console.WriteLine("Patch UserAssembly...");
                            }
                            catch (Exception ex)
                            {
                                return "Failed Patch UserAssembly Original1: " + ex.ToString();
                            }
                        }
                        else
                        {
                            return "Failed Patch UserAssembly (0)";
                        }
                    }
                    else
                    {
                        // Jika PathfileUA_Original ada kembalikan ke PathfileUA_Currently
                        if (File.Exists(PathfileUA_Original))
                        {
                            try
                            {
                                File.Copy(PathfileUA_Original, PathfileUA_Currently, true);
                                MD5_UA_LOC_Currently = CalculateMD5(PathfileUA_Currently);

                                Console.WriteLine("Back to Original UserAssembly Version...");
                            }
                            catch (Exception ex)
                            {
                                return "Failed: Backup UserAssembly Original2: " + ex.ToString();
                            }
                        }

                    }

                }

                Console.WriteLine("MD5 UA Currently: " + MD5_UA_LOC_Currently);
                Console.WriteLine("MD5 UA Original: " + MD5_UA_LOC_Original);
                Console.WriteLine("MD5 UA Patched: " + MD5_UA_LOC_Patched);
            }
            else if (metode == 1)
            {
                // Check folder Metadata
                var cst_folder_metadata = Set_Metadata_Folder.Text;
                if (String.IsNullOrEmpty(cst_folder_metadata))
                {
                    return "No metadata folder found (1)";
                }
                if (!Directory.Exists(cst_folder_metadata))
                {
                    return "No metadata folder found (2)";
                }

                // Check file metadata
                string PathfileMetadata_Currently = Path.Combine(cst_folder_metadata, "global-metadata.dat");
                string PathfileMetadata_Patched = Path.Combine(cst_folder_metadata, "global-metadata-patched.dat");
                string PathfileMetadata_Original = Path.Combine(cst_folder_metadata, "global-metadata-original.dat");

                // Get MD5 local Metadata (First time)
                string MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);
                string MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);
                string MD5_Metadata_LOC_Patched = CalculateMD5(PathfileMetadata_Patched);

                // debug
                //online = false;

                if (online)
                {
                    // API
                    string MD5_Metadata_API_Original;
                    string MD5_Metadata_API_Patched;

                    var cno = "Global";
                    if (ch == 1)
                    {
                        // Global
                        MD5_Metadata_API_Original = last_key_api.Original.MetaData.md5_os.ToUpper();
                        MD5_Metadata_API_Patched = last_key_api.Patched.MetaData.md5_os.ToUpper();
                    }
                    else if (ch == 2)
                    {
                        // Chinese
                        MD5_Metadata_API_Original = last_key_api.Original.MetaData.md5_cn.ToUpper();
                        MD5_Metadata_API_Patched = last_key_api.Patched.MetaData.md5_cn.ToUpper();
                        cno = "Chinese";
                    }
                    else
                    {
                        return "This Game Version is not compatible with this method patch (2)";
                    }

                    var DL_Patch = API.API_DL_OW + "api/public/dl/ZOrLF1E5/GenshinImpact/Data/PC/" + VersionGame + "/Release/" + cno + "/Patch/";

                    // If original backup file is not found, start backup process
                    if (!File.Exists(PathfileMetadata_Original))
                    {
                        // Check if MD5_Metadata_API_Original (original file from api) matches MD5_Metadata_LOC_Currently (file in current use)
                        if (MD5_Metadata_API_Original == MD5_Metadata_LOC_Currently)
                        {
                            try
                            {
                                File.Copy(PathfileMetadata_Currently, PathfileMetadata_Original, true);
                                MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);

                                Console.WriteLine("Backup Metadata Original");
                            }
                            catch (Exception ex)
                            {
                                return "Failed: Backup Metadata Original1: " + ex.ToString();
                            }
                        }
                        else
                        {
                            // Download Original MetaData
                            var DL3 = new Download(DL_Patch + "global-metadata-original.dat", PathfileMetadata_Original);
                            if (DL3.ShowDialog() != DialogResult.OK)
                            {
                                return "Original Backup failed because md5 doesn't match";
                            }
                            else
                            {
                                MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);
                            }
                        }
                    }

                    // Jika file metadata sekarang tidak ada gunakan global-metadata-original.dat
                    if (!File.Exists(PathfileMetadata_Currently))
                    {
                        try
                        {
                            File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                            MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                            Console.WriteLine("Get Backup Original");
                        }
                        catch (Exception exx)
                        {
                            return "Error Get Backup Original: " + exx.ToString();
                        }
                    }

                    // Jik User tidak ingin patch kembalikan ke aslinya. (If MD5_Metadata_API_Original doesn't match MD5_Metadata_LOC_Currently)
                    if (!patchit)
                    {
                        if (MD5_Metadata_API_Original != MD5_Metadata_LOC_Currently)
                        {
                            try
                            {
                                File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                                Console.WriteLine(MD5_Metadata_API_Original + " doesn't match with " + MD5_Metadata_LOC_Currently + ", backup it....");
                            }
                            catch (Exception exx)
                            {
                                return "Failed: Backup Metadata Original: " + exx.ToString();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Skip, it's up-to-date");
                        }
                    }
                    else if (MD5_Metadata_API_Patched != MD5_Metadata_LOC_Currently)
                    {
                        // Jika User pilih patch (MD5_Metadata_API_Patched doesn't match MD5_Metadata_LOC_Currently)
                        if (!File.Exists(PathfileMetadata_Patched))
                        {
                            // If you don't have PathfileMetadata_Patched, download it
                            var DL2 = new Download(DL_Patch + "global-metadata-patched.dat", PathfileMetadata_Patched);
                            if (DL2.ShowDialog() != DialogResult.OK)
                            {
                                // If PathfileMetadata_Patched (Patched file) doesn't exist
                                return "No Found Patch file....";
                            }
                            else
                            {
                                MD5_Metadata_LOC_Patched = CalculateMD5(PathfileMetadata_Patched);
                            }
                        }

                        // If Metadata_API_Patches_MD5 (patch file from api) matches Metadata_LOC_Patched_MD5 (current patch file)
                        if (MD5_Metadata_API_Patched == MD5_Metadata_LOC_Patched)
                        {
                            // Patch to PathfileMetadata_Now                            
                            try
                            {
                                File.Copy(PathfileMetadata_Patched, PathfileMetadata_Currently, true);
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                                Console.WriteLine("Patch done...");
                            }
                            catch (Exception x)
                            {
                                return "Failed Patch: " + x.ToString();
                            }
                        }
                        else
                        {
                            return "Failed because file doesn't match from md5 api";
                        }
                    }
                    else
                    {
                        // If Metadata_API_Patches_MD5 match Metadata_LOC_Now_MD5
                    }
                }
                else
                {
                    // Offline Mode Metadata
                    if (patchit)
                    {
                        // Backup PathfileMetadata_Original
                        if (!File.Exists(PathfileMetadata_Original))
                        {
                            try
                            {
                                if (!File.Exists(PathfileMetadata_Currently))
                                {
                                    return "Cannot find current metadata file in offline mode";
                                }

                                // Use PathfileMetadata_Now to backup PathfileMetadata_Original file, but since without md5 api we can't check is original or not.
                                File.Copy(PathfileMetadata_Currently, PathfileMetadata_Original, true);
                                MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);

                                Console.WriteLine("Backup Metadata Original");
                            }
                            catch (Exception ex)
                            {
                                return ex.ToString();
                            }
                        }

                        // Check PathfileMetadata_Currently if no found
                        if (!File.Exists(PathfileMetadata_Currently))
                        {
                            if (File.Exists(PathfileMetadata_Original))
                            {
                                File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                                Console.WriteLine("We detected that you did not have files (Currently) so we returned them with Original");
                            }
                            else
                            {
                                return "Can't find Original Metadata file in offline mode (0)";
                            }
                        }
                        else
                        {
                            // jika PathfileMetadata_Currently ada coba cek PathfileMetadata_Original apakah sama
                            if (File.Exists(PathfileMetadata_Original))
                            {
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);
                                MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);
                                if (MD5_Metadata_LOC_Currently != MD5_Metadata_LOC_Original)
                                {
                                    File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                    MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                                    Console.WriteLine("We detect you have non-original (Currently) files so we return them with Original");
                                }
                            }
                            else
                            {
                                return "Can't find Original Metadata file in offline mode (1)";
                            }
                        }

                        // Patch (Because we can't check md5 from api to check if this file is original or patched so to make it work we will always patch)
                        // TODO: Looking for another better method
                        var ManualMetadata = "???";
                        // Select CH
                        if (ch == 1)
                        {
                            ManualMetadata = patch.Metadata.Do(PathfileMetadata_Currently, PathfileMetadata_Patched, last_key_api.Original.MetaData.key1, last_key_api.Patched.MetaData.key1, last_key_api.Original.MetaData.key2_os, last_key_api.Patched.MetaData.key2);
                        }
                        else if (ch == 2)
                        {
                            ManualMetadata = patch.Metadata.Do(PathfileMetadata_Currently, PathfileMetadata_Patched, last_key_api.Original.MetaData.key1, last_key_api.Patched.MetaData.key1, last_key_api.Original.MetaData.key2_cn, last_key_api.Patched.MetaData.key2);
                        }
                        if (!String.IsNullOrEmpty(ManualMetadata))
                        {
                            return "Error Patch: " + ManualMetadata;
                        }

                        // Patch PathfileMetadata_Patched ke PathfileMetadata_Currently
                        if (File.Exists(PathfileMetadata_Patched))
                        {
                            try
                            {
                                File.Copy(PathfileMetadata_Patched, PathfileMetadata_Currently, true);
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);

                                Console.WriteLine("Patch Metadata...");
                            }
                            catch (Exception ex)
                            {
                                return "Failed Patch Metadata Original1: " + ex.ToString();
                            }
                        }
                        else
                        {
                            return "Failed Patch Metadata (0)";
                        }

                    }
                    else
                    {
                        // Jika PathfileMetadata_Original, Kembalikan PathfileMetadata_Original ke PathfileMetadata_Currently
                        if (File.Exists(PathfileMetadata_Original))
                        {
                            try
                            {
                                MD5_Metadata_LOC_Original = CalculateMD5(PathfileMetadata_Original);
                                MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);
                                if (MD5_Metadata_LOC_Original == MD5_Metadata_LOC_Currently)
                                {
                                    Console.WriteLine("Current file is Original");
                                }
                                else
                                {
                                    File.Copy(PathfileMetadata_Original, PathfileMetadata_Currently, true);
                                    MD5_Metadata_LOC_Currently = CalculateMD5(PathfileMetadata_Currently);
                                    Console.WriteLine("Back to Original Metadata Version...");
                                }
                            }
                            catch (Exception ex)
                            {
                                return "Failed: Backup Metadata Original2: " + ex.ToString();
                            }
                        }

                    }
                }

                // Make sure MD5 matches
                Console.WriteLine("MD5 Metadata Currently: " + MD5_Metadata_LOC_Currently);
                Console.WriteLine("MD5 Metadata Original: " + MD5_Metadata_LOC_Original);
                Console.WriteLine("MD5 Metadata Patched: " + MD5_Metadata_LOC_Patched);
            }
            else
            {
                return "No other method found";
            }

            return "";
        }

        private void Set_LA_Select_Click(object sender, EventArgs e)
        {
            var Folder_Game_Now = SelectGamePath();
            if (!string.IsNullOrEmpty(Folder_Game_Now))
            {
                Set_LA_GameFolder.Text = Folder_Game_Now;
                CheckVersionGame();// TODO: hapus nanti
            }
            else
            {
                MessageBox.Show("No game folder found");
            }
        }

        public void GetServerList()
        {
            var GetDataServerList = API.ServerList();
            if (GetDataServerList == null)
            {
                MessageBox.Show("Error get server list");
                return;
            }

            ListServer = GetDataServerList.list;

            ServerList.BeginUpdate();
            ServerList.Items.Clear();

            for (int i = 0; i < ListServer.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ListServer[i].name;
                lvi.SubItems.Add(ListServer[i].host);
                lvi.SubItems.Add("N/A");
                lvi.SubItems.Add("N/A");
                lvi.SubItems.Add("N/A");
                ServerList.Items.Add(lvi);
            }

            ServerList.EndUpdate();
        }

        public void UpdateServerListTimer()
        {
            Debug.Print("Start update..");
            if (thServerList != null)
            {
                thServerList.Interrupt();
            }
            thServerList = new Thread(() =>
            {
                if (ListServer == null)
                {
                    return;
                }
                for (int i = 0; i < ListServer.Count; i++)
                {
                    int s = i;
                    new Thread(() =>
                    {
                        var host = ListServer[s].host;
                        try
                        {
                            if (host == "official")
                            {
                                return;
                            }

                            Debug.Print("Start update.. " + host);

                            string url_server_api = "https://" + host + "/status/server";
                            VersionServer? ig = API.GetServerStatus(url_server_api);
                            ServerList.Invoke((Action)delegate
                            {
                                if (ig != null)
                                {
                                    ServerList.Items[s].SubItems[2].Text = ig.status.playerCount.ToString();
                                    ServerList.Items[s].SubItems[3].Text = ig.status.Version.ToString();
                                }
                                else
                                {
                                    ServerList.Items[s].SubItems[2].Text = "N/A";
                                    ServerList.Items[s].SubItems[3].Text = "N/A";
                                }
                                try
                                {
                                    PingReply reply = new Ping().Send(host, 1000);
                                    if (reply.Status == IPStatus.Success)
                                        ServerList.Items[s].SubItems[4].Text = reply.RoundtripTime + "ms";
                                }
                                catch
                                {
                                    ServerList.Items[s].SubItems[4].Text = "N/A";
                                }
                            });
                        }
                        catch (Exception e)
                        {
                            Debug.Print("Error Host " + host + "" + e.Message);
                        }
                        finally
                        {
                            //
                        }
                    }).Start();
                }
            });
            thServerList.Start();
        }

        public void CheckUpdate()
        {
            Console.WriteLine("Cek update...");
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string ver = "";
            if (version != null)
            {
                ver = version.ToString();
                Set_Version.Text = "Version: " + ver;
                var GetDataUpdate = API.GetUpdate();
                if (GetDataUpdate != null)
                {
                    var judul = GetDataUpdate.name; // is dev or nightly or name update
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
                                var DL1 = new Download(url_dl, CurrentlyPath + @"\update.zip");
                                if (DL1.ShowDialog() == DialogResult.OK)
                                {
                                    // update
                                    var file_update = CurrentlyPath + @"\update.bat";
                                    try
                                    {
                                        //buat bat
                                        var w = new StreamWriter(file_update);
                                        w.WriteLine("@echo off");

                                        //kill all
                                        w.WriteLine("Taskkill /IM YuukiPS.exe /F");
                                        w.WriteLine("Taskkill /IM YuukiPS.vshost.exe /F");

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
        private static string GetLauncherPath(String version = "Genshin Impact")
        {
            RegistryKey key = Registry.LocalMachine;
            if (key != null)
            {
                var tes1 = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + version);
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

        // Check MD5
        private static string CalculateMD5(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "");
                    }
                }
            }
            catch (Exception)
            {
                return "Unknown";
            }

        }

        [Obsolete]
        private void btStart_Click(object sender, EventArgs e)
        {
            // Jika game berjalan...
            if (IsGameRun)
            {
                AllStop();
                return;
            }

            // Get Extra
            bool isAkebiGC = Extra_AkebiGC.Checked;
            bool isProxyNeed = CheckProxyEnable.Checked;

            // Get Host
            string set_server_host = GetHost.Text;
            if (string.IsNullOrEmpty(set_server_host))
            {
                MessageBox.Show("Please select a server first, you can click on one in server list");
                return;
            }

            // Get Proxy
            int set_proxy_port = int.Parse(GetPort.Text);
            bool set_server_https = CheckProxyUseHTTPS.Checked;

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

                // run patch
                var tes = PatchGame(patch, checkModeOnline.Checked, GameMetode, GameChannel);
                if (!string.IsNullOrEmpty(tes))
                {
                    if (tes.Contains("Key1") || tes.Contains("Key2"))
                    {
                        MessageBox.Show("This may happen because you have already patched or you are using an unsupported version game. The solution is you can use Online Method (you can find it in Config Tab) to make sure you have right file.", "Error Patch Offline");
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
                        proxy = new ProxyController(set_proxy_port, set_server_host, set_server_https);
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

            if (isAkebiGC)
            {
                var set_AkebiGC = Path.Combine(Modfolder, "AkebiGC");
                Directory.CreateDirectory(set_AkebiGC);
                string get_AkebiGC = Path.Combine(set_AkebiGC, "injector.exe");
                string get_AkebiGC_zip = Path.Combine(set_AkebiGC, "update.zip");
                string get_AkebiGC_md5 = Path.Combine(set_AkebiGC, "md5.txt");

                var Update_AkebiGC = false;

                var cekAkebi = API.GetAkebi(GameChannel);
                if (string.IsNullOrEmpty(cekAkebi))
                {
                    MessageBox.Show("Can't check latest Akebi");
                    return;
                }
                string[] SplitAkebiGC = cekAkebi.Split("|");

                // Check file update, jika tidak ada
                if (!File.Exists(get_AkebiGC_md5))
                {
                    Console.WriteLine("Md5 no found, update!!!");
                    Update_AkebiGC = true;
                }
                else
                {
                    string readText = File.ReadAllText(get_AkebiGC_md5);
                    if (!readText.Contains(SplitAkebiGC[0]))
                    {
                        Console.WriteLine("Found a new version, time to download");
                        Update_AkebiGC = true;
                    }
                }

                if (Update_AkebiGC)//!File.Exists(get_AkebiGC)
                {
                    var DL2 = new Download(SplitAkebiGC[1], get_AkebiGC_zip);
                    if (DL2.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Download Akebi failed");
                        return;
                    }
                    else
                    {
                        // if download done....
                        var file_update_AkebiGC = set_AkebiGC + @"\update.bat";
                        try
                        {
                            // Make bat file for update
                            var w = new StreamWriter(file_update_AkebiGC);
                            w.WriteLine("@echo off");

                            w.WriteLine("cd \"" + set_AkebiGC + "\" ");

                            // Kill Akebi
                            w.WriteLine("Taskkill /IM injector.exe /F");

                            // Unzip file
                            w.WriteLine("echo unzip file...");
                            w.WriteLine("tar -xvf update.zip");

                            //delete file old
                            w.WriteLine("echo delete file zip");
                            w.WriteLine("del /F update.zip");

                            // del update
                            w.WriteLine("del /F update.bat");
                            w.Close();

                            //open bat
                            //Process.Start(file_update_AkebiGC);

                            ProcessStartInfo pInfo = new ProcessStartInfo();
                            pInfo.FileName = file_update_AkebiGC;
                            Process p = Process.Start(pInfo);

                            p.WaitForExit();

                            // Update MD5
                            File.WriteAllText(get_AkebiGC_md5, SplitAkebiGC[0]);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
                cst_gamefile = get_AkebiGC;
                //WatchFile = "injector";
                Console.WriteLine("RUN: " + cst_gamefile);
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

        [Obsolete]
        private void CheckGameRun_Tick(object sender, EventArgs e)
        {
            var isrun = Process.GetProcesses().Where(pr => pr.ProcessName == "YuanShen" || pr.ProcessName == "GenshinImpact" || pr.ProcessName == "injector");
            if (!isrun.Any())
            {
                // Jika Game tidak berjalan....
                IsGameRun = false;
                btStart.Text = "Launch";

                AllStop();

                // Revert to original version every game close
                if (!DoneCheck)
                {
                    Console.WriteLine("Game detected stopped");
                    StopGame(); // this shouldn't be necessary but just let it be
                    var tes = PatchGame(false, checkModeOnline.Checked, GameMetode, GameChannel);
                    if (!string.IsNullOrEmpty(tes))
                    {
                        Console.WriteLine(tes);
                    }
                    DoneCheck = true;
                    discord.UpdateStatus("Not playing", "Stop", "sleep");
                }
            }
            else
            {
                // jika game jalan
                IsGameRun = true;
                btStart.Text = "Stop";
                DoneCheck = false;
                discord.UpdateStatus($"Server: {HostName} Version: {VersionGame}", "In Game", "on", 1);
            }
        }

        [Obsolete]
        public void AllStop()
        {
            StopProxy();
            StopGame();
        }

        [Obsolete]
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
                    //KillProcessAndChildrens(progress.Id);
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
                        EndTask(WatchFile);
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

        public void EndTask(string taskname)
        {
            var chromeDriverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == taskname);
            foreach (var process in chromeDriverProcesses)
            {
                process.Kill();
            }
        }

        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            // We must kill child processes first!
            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }

            // Then kill parents.
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

        private void btStartServer_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Still PR :)");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btReloadServer_Click(object sender, EventArgs e)
        {
            GetServerList();
        }

        private void ServerList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = ServerList.GetItemAt(e.X, e.Y);
            if (item == null)
            {
                return;
            }
            var name_server = item.SubItems[0].Text;
            var host_server = item.SubItems[1].Text;
            GetHost.Text = host_server;
            HostName = name_server;
        }

        private void linkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://discord.yuuki.me/") { UseShellExecute = true });
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/akbaryahya/YuukiPS-Launcher") { UseShellExecute = true });
        }

        private void linkWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://ps.yuuki.me/") { UseShellExecute = true });
        }

        private void CekUpdateTT_Tick(object sender, EventArgs e)
        {
            if (Is_ServerList_Autocheck.Checked)
            {
                UpdateServerListTimer();
            }
        }

        [Obsolete]
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Jika Proxy atau game masih berjalan
            /*
            if (proxy != null)
            {
                if (MessageBox.Show("Currently proxy is still running do you want to cancel exit?", "a good question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else
                {

                }
            }
            */
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Set_LA_Save_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        [Obsolete]
        private void CheckProxyRun_Tick(object sender, EventArgs e)
        {
            CheckProxy(false);
        }

        [Obsolete]
        void CheckProxy(bool force_off = false)
        {
            try
            {
                // Metode 1
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                if ((int)registry.GetValue("ProxyEnable") == 1)
                {
                    if (force_off)
                    {
                        registry.SetValue("ProxyEnable", 0);
                    }

                    // Metode 2
                    if (proxy != null)
                    {
                        stIsRunProxy.Text = "Status: ON (Internal)";
                    }
                    else
                    {
                        stIsRunProxy.Text = "Status: ON (External)";
                    }

                }
                else
                {
                    StopProxy();
                    stIsRunProxy.Text = "Status: OFF";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Does not support proxy check support: " + ex.ToString());
            }
        }
    }
}