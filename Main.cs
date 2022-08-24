using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;

namespace YuukiPS_Launcher
{
    public partial class Main : Form
    {
        private ProxyController? proxy;
        private Process? progress;

        public Main()
        {
            InitializeComponent();
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
            int set_proxy_port = int.Parse(GetPort.Text);

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
            }

            Console.WriteLine("Folder Game: " + Folder_Game_Now);

            string cn = Path.Combine(Folder_Game_Now, "YuanShen.exe");
            string os = Path.Combine(Folder_Game_Now, "GenshinImpact.exe");

            // Check Game Path
            string FullGamePath;
            string MetadataPath;
            string Metadata_md5_api_ori;
            string Metadata_md5_api_patch;
            if (File.Exists(cn))
            {
                FullGamePath = cn;
                MetadataPath = Path.Combine(Folder_Game_Now, "YuanShen_Data", "Managed", "Metadata");
                Metadata_md5_api_ori = "idk";
                Metadata_md5_api_patch = "";
            }
            else if (File.Exists(os))
            {
                FullGamePath = os;
                MetadataPath = Path.Combine(Folder_Game_Now, "GenshinImpact_Data", "Managed", "Metadata");
                Metadata_md5_api_ori = "809de2b9cd7a0f8cdd8687e3a8291cbb";
                Metadata_md5_api_patch = "6edd177848f05b0f9200a033aac8479c";
            }
            else
            {
                MessageBox.Show("No game files found!!!");
                return;
            }

            Console.WriteLine("Folder MetadataPath: " + MetadataPath);
            Console.WriteLine("File Game: " + FullGamePath);

            // Check Metadata file
            string Medata_now = Path.Combine(MetadataPath, "global-metadata.dat");
            if (!File.Exists(Medata_now))
            {
                MessageBox.Show("Metadata file not found");
                return;
            }

            string Metadata_now_md5 = CalculateMD5(Medata_now);

            Console.WriteLine("MetadataNow: " + Metadata_now_md5);

            string metadata_patch_path = Path.Combine(MetadataPath, "global-metadata-patch.dat");
            string metadata_ori_path = Path.Combine(MetadataPath, "global-metadata-ori.dat");

            string metadata_patch_path_md5 = CalculateMD5(metadata_patch_path);
            string metadata_ori_path_md5 = CalculateMD5(metadata_ori_path);

            Console.WriteLine("MetadataPatch: " + metadata_patch_path_md5);
            Console.WriteLine("MetadataORI: " + metadata_ori_path_md5);

            // check if found patch just skip it
            if (progress == null)
            {
                if (Metadata_md5_api_patch != Metadata_now_md5)
                {
                    Console.WriteLine("Metadata doesn't match, patch it...");

                    if (!File.Exists(metadata_ori_path))
                    {
                        // JIK API ORI SAMA DENGAN SEKARANG, BACKUP
                        if (Metadata_md5_api_ori == Metadata_now_md5)
                        {
                            Console.WriteLine("Backup Metadata ori...");
                            File.Copy(Medata_now, metadata_ori_path, true);
                        }
                        else
                        {
                            // JIKA API ORI BEDA DENGAN META SEKARANG:
                            Console.WriteLine("Original version backup failed because md5 doesn't match\r\n");
                            return;
                        }
                    }

                    if (!File.Exists(metadata_patch_path))
                    {
                        // JIKA FILE PATCH TIDAK ADA
                        Console.WriteLine("No Found Patch file....");
                        // TODO: add download
                        return;
                    }
                    else
                    {
                        // JIKA PATCH ADA, CEK APAKAH API DAN MERA PATCH MD5 SAMA
                        if (Metadata_md5_api_patch == metadata_patch_path_md5)
                        {
                            Console.WriteLine("Patch done...");
                            File.Copy(metadata_patch_path, Medata_now, true);
                        }
                        else
                        {
                            Console.WriteLine("Patch version failed because md5 doesn't match\r\n");
                            return;
                        }
                    }
                }
                else
                {
                    // skip
                }
            }
            else
            {
                // skip
            }

            // For Proxy
            if (proxy == null)
            {
                proxy = new ProxyController(set_proxy_port, set_server_host);
                proxy.Start();
                btStart.Text = "Stop";
            }
            else
            {
                proxy.Stop();
                proxy = null;
                btStart.Text = "Launch";
            }

            // For Patch
            String Metadata_Game = Folder_Game_Now;

            // For Game
            if (progress == null)
            {
                progress = new Process();
                progress.StartInfo = new ProcessStartInfo
                {
                    FileName = FullGamePath
                };
                try
                {
                    progress.Start();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                Console.WriteLine("Game Stop!");
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

                if (File.Exists(metadata_ori_path))
                {
                    try
                    {
                        // JIKA API ORI TIDAK COCOK DENGAN METADATA SEKARANG
                        if (Metadata_md5_api_ori != Metadata_now_md5)
                        {
                            Console.WriteLine("Revert to original version...");
                            File.Copy(metadata_ori_path, Medata_now, true);
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
    }
}