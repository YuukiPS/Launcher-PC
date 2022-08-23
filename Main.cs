using Microsoft.Win32;
using System.Diagnostics;
using System.Management;

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
            if (File.Exists(cn))
            {
                FullGamePath = cn;
            }
            else if (File.Exists(os))
            {
                FullGamePath = os;
            }
            else
            {
                MessageBox.Show("No game files found!!!");
                return;
            }

            Console.WriteLine("Game Path: " + FullGamePath);

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
                KillProcessAndChildrens(progress.Id);
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