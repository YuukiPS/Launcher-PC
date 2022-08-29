using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;
using YuukiPS_Launcher.json;

namespace YuukiPS_Launcher
{
    public partial class Main : Form
    {
        private ProxyController? proxy;
        private Process? progress;

        private string TES_API = "https://drive.yuuki.me/api/public/dl/ZOrLF1E5/GenshinImpact/Data/PC/3.0.0/Release/Global/Patch/";

        public static string CurrentlyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
        //private static string DataConfig = Path.Combine(CurrentlyPath, "data");

        public Main()
        {
            // Create missing Files
            //Directory.CreateDirectory(DataConfig);

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CheckUpdate();
            GetServerList();
        }

        public void GetServerList()
        {
            var GetDataServerList = API.ServerList();
            var TR = GetDataServerList.list;

            ServerList.BeginUpdate();
            ServerList.Items.Clear();

            for (int i = 0; i < TR.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = TR[i].name;
                lvi.SubItems.Add(TR[i].host);
                lvi.SubItems.Add("N/A");
                lvi.SubItems.Add(TR[i].version + "");
                ServerList.Items.Add(lvi);
            }
            ServerList.EndUpdate();
        }

        public void CheckUpdate()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine(version);
            string ver = "";
            if (version != null)
            {
                ver = version.ToString();
                Set_Version.Text = "Version: " + ver;
                var GetDataUpdate = API.GetUpdate();
                if (GetDataUpdate != null)
                {
                    // If versions are not same, try asking user to update?
                    var judul = GetDataUpdate.name;
                    var name_version = GetDataUpdate.tag_name;
                    var infobody = GetDataUpdate.body;

                    var version1 = new Version("2022.8.29.107");
                    var version2 = new Version(ver);

                    var result = version1.CompareTo(version2);

                    if (result > 0)
                    {
                        // versi 1 lebih besar
                        Set_Version.Text = "Version: " + ver + " (New " + name_version + ")";
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
                        //MessageBox.Show("versi2 lebih besar", "tes", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        Set_Version.Text = "Version: " + ver + " (latest developer version)";
                    }
                    else
                    {
                        Set_Version.Text = "Version: " + ver + " (latest public version)";
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

            if (key == null)
            {
                return "";
            }

            var tes1 = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + version); // 原神
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
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception)
            {
                return "uk";
            }

        }

        [Obsolete]
        private void btStart_Click(object sender, EventArgs e)
        {
            string set_server_host = GetHost.Text;
            if (string.IsNullOrEmpty(set_server_host))
            {
                MessageBox.Show("Please select a server first, you can click on one in server list");
                return;
            }

            GS api;

            int set_proxy_port = int.Parse(GetPort.Text);
            bool set_server_https = CheckProxyUseHTTPS.Checked;

            string Folder_Game_Now = "";

            var Get_Launcher = GetLauncherPath();
            Console.WriteLine("Folder Launcher: " + Get_Launcher);
            if (string.IsNullOrEmpty(Get_Launcher))
            {
                MessageBox.Show("Please find game install folder!", "Launcher Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Folder_Game_Now = GetGamePath(Get_Launcher);
                if (string.IsNullOrEmpty(Folder_Game_Now))
                {
                    MessageBox.Show("Please find game install folder!", "Game Folder Install Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // debug
            //Folder_Game_Now = "";

            // jika masih belum dapat bukan pilih folder
            if (string.IsNullOrEmpty(Folder_Game_Now))
            {
                Folder_Game_Now = SelectGamePath();
                if (string.IsNullOrEmpty(Folder_Game_Now))
                {
                    MessageBox.Show("Because it can't find the folder so it can't run", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // TODO: save config for next run
                }
            }

            Console.WriteLine("Folder Game: " + Folder_Game_Now);

            string cn = Path.Combine(Folder_Game_Now, "YuanShen.exe");
            string os = Path.Combine(Folder_Game_Now, "GenshinImpact.exe");

            // Path
            string PathfileGame;
            string PathMetadata;

            // TODO: add api check md5
            string Metadata_API_Original_MD5;
            string Metadata_API_Patches_MD5;

            if (File.Exists(cn))
            {
                //api = API.GS_DL("cn");
                PathfileGame = cn;
                PathMetadata = Path.Combine(Folder_Game_Now, "YuanShen_Data", "Managed", "Metadata");
                Metadata_API_Original_MD5 = "idk";
                Metadata_API_Patches_MD5 = "";
            }
            else if (File.Exists(os))
            {
                //api = API.GS_DL();
                PathfileGame = os;
                PathMetadata = Path.Combine(Folder_Game_Now, "GenshinImpact_Data", "Managed", "Metadata");
                Metadata_API_Original_MD5 = "809de2b9cd7a0f8cdd8687e3a8291cbb";
                Metadata_API_Patches_MD5 = "1307d55022167856879b284084f43426";
            }
            else
            {
                MessageBox.Show("No game files found!!!");
                return;
            }

            Console.WriteLine("Folder PathMetadata: " + PathMetadata);
            Console.WriteLine("File Game: " + PathfileGame);

            // Check file metadata
            string PathfileMetadata_Now = Path.Combine(PathMetadata, "global-metadata.dat");
            string PathfileMetadata_Patched = Path.Combine(PathMetadata, "global-metadata-patched.dat");
            string PathfileMetadata_Original = Path.Combine(PathMetadata, "global-metadata-original.dat");

            SetInputMetadata.Text = PathfileMetadata_Now;

            if (!File.Exists(PathfileMetadata_Now))
            {
                // Download global-metadata-original.dat to global-metadata.dat
                var DL1 = new Download(TES_API + "global-metadata-original.dat", PathfileMetadata_Now);
                if (DL1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Metadata file not found");
                    return;
                }
            }

            // Get MD5
            string Metadata_LOC_Now_MD5 = CalculateMD5(PathfileMetadata_Now);
            string Metadata_LOC_Patched_MD5 = CalculateMD5(PathfileMetadata_Patched);
            string Metadata_LOC_Original_MD5 = CalculateMD5(PathfileMetadata_Original);
            string Game_LOC_Original_MD5 = CalculateMD5(PathfileGame);

            Console.WriteLine("Metadata_LOC_Now_MD5: " + Metadata_LOC_Now_MD5);
            Console.WriteLine("Metadata_LOC_Patched_MD5: " + Metadata_LOC_Patched_MD5);
            Console.WriteLine("Metadata_LOC_Original_MD5: " + Metadata_LOC_Original_MD5);
            Console.WriteLine("Game_LOC_Original_MD5: " + Game_LOC_Original_MD5);

            // here should start patching process

            // If game doesn't run, check patch
            if (progress == null)
            {

                // If original backup file is not found, start backup process
                if (!File.Exists(PathfileMetadata_Original))
                {
                    // Check if Metadata_API_Original_MD5 (original file from api) matches Metadata_LOC_Now_MD5 (file in current use)
                    if (Metadata_API_Original_MD5 == Metadata_LOC_Now_MD5)
                    {
                        Console.WriteLine("Backup Metadata Original");
                        try
                        {
                            File.Copy(PathfileMetadata_Now, PathfileMetadata_Original, true);
                        }
                        catch (Exception ignore)
                        {
                            MessageBox.Show(ignore.Message, "Failed: Backup Metadata Original");
                            return;
                        }

                    }
                    else
                    {
                        // Download original metadata
                        var DL3 = new Download(TES_API + "global-metadata-original.dat", PathfileMetadata_Original);
                        if (DL3.ShowDialog() != DialogResult.OK)
                        {
                            MessageBox.Show("Original Backup failed because md5 doesn't match");
                            return;
                        }
                    }
                }


                if (set_server_host == "official")
                {
                    // If Metadata_API_Original_MD5 doesn't match Metadata_LOC_Now_MD5
                    if (Metadata_API_Original_MD5 != Metadata_LOC_Now_MD5)
                    {
                        Console.WriteLine("Metadata_API_Original_MD5 doesn't match with Metadata_LOC_Now_MD5, backup it....");
                        try
                        {
                            File.Copy(PathfileMetadata_Original, PathfileMetadata_Now, true);
                        }
                        catch (Exception ignore)
                        {
                            MessageBox.Show(ignore.Message, "Failed: Backup Metadata Original");
                            return;
                        }
                    }
                }
                else if (Metadata_API_Patches_MD5 != Metadata_LOC_Now_MD5)
                {
                    // If Metadata_API_Patches_MD5 doesn't match Metadata_LOC_Now_MD5
                    if (!File.Exists(PathfileMetadata_Patched))
                    {
                        // If you don't have PathfileMetadata_Patched, download it
                        var DL2 = new Download(TES_API + "global-metadata-patched.dat", PathfileMetadata_Patched);
                        if (DL2.ShowDialog() != DialogResult.OK)
                        {
                            // If PathfileMetadata_Patched (Patched file) doesn't exist
                            MessageBox.Show("No Found Patch file....");
                            return;
                        }
                    }
                    else
                    {
                        // If Metadata_API_Patches_MD5 (patch file from api) matches Metadata_LOC_Patched_MD5 (current patch file)
                        if (Metadata_API_Patches_MD5 == Metadata_LOC_Patched_MD5)
                        {
                            // Patch to PathfileMetadata_Now                            
                            try
                            {
                                File.Copy(PathfileMetadata_Patched, PathfileMetadata_Now, true);
                                Console.WriteLine("Patch done...");
                            }
                            catch (Exception ignore)
                            {
                                MessageBox.Show(ignore.Message, "Failed Patch");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed because file doesn't match from md5 api");
                            return;
                        }
                    }
                }
                else
                {
                    // If Metadata_API_Patches_MD5 match Metadata_LOC_Now_MD5
                }
            }
            else
            {
                // if game is running
            }

            // For Proxy
            if (proxy == null)
            {
                // skip proxy if official
                if (set_server_host != "official")
                {
                    proxy = new ProxyController(set_proxy_port, set_server_host, set_server_https);
                    if (!proxy.Start())
                    {
                        MessageBox.Show("Maybe port is already use or Windows Firewall does not allow using port " + set_proxy_port + " or Windows Update sometimes takes that range", "Failed Start...");
                        proxy = null;
                        return;
                    }
                    stIsRunProxy.Text = "Status: ON";
                }
            }
            else
            {
                proxy.Stop();
                proxy = null;
                stIsRunProxy.Text = "Status: OFF";
            }

            // For Game
            if (progress == null)
            {
                progress = new Process();
                progress.StartInfo = new ProcessStartInfo
                {
                    FileName = PathfileGame
                };
                try
                {
                    progress.Start();
                    btStart.Text = "Stop";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    progress.Dispose();
                    progress = null;
                    btStart.Text = "Launch";

                }
            }
            else
            {
                try
                {
                    KillProcessAndChildrens(progress.Id);
                }
                catch (Exception exx)
                {
                    //skip
                }
                progress.WaitForExit();
                progress = null;

                Console.WriteLine("Game Stop!");

                btStart.Text = "Launch";

                if (File.Exists(PathfileMetadata_Now))
                {
                    try
                    {
                        // If md5 file from api doesn't match current file
                        if (Metadata_API_Original_MD5 != Metadata_LOC_Now_MD5)
                        {
                            Console.WriteLine("Revert to original version...");
                            try
                            {
                                File.Copy(PathfileMetadata_Original, PathfileMetadata_Now, true);
                            }
                            catch (Exception ignore)
                            {
                                MessageBox.Show(ignore.Message, "Failed Revert to original version");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Because this is still using original version");
                            return;
                        }
                    }
                    catch (Exception xc)
                    {
                        Console.WriteLine("Failed to return to the original version because the game has not been closed");
                    }

                }

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
    }
}