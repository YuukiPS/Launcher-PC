using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;

namespace YuukiPS_Launcher.Yuuki
{
    public static class Tool
    {
        public static string CalcMemoryMensurableUnit(double bytes)
        {
            double kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte 
            double mb = kb / 1024; // · 1024 Kilobytes = 1 Megabyte 
            double gb = mb / 1024; // · 1024 Megabytes = 1 Gigabyte 
            double tb = gb / 1024; // · 1024 Gigabytes = 1 Terabyte 

            string result =
                tb > 1 ? $"{tb:0.##}TB" :
                gb > 1 ? $"{gb:0.##}GB" :
                mb > 1 ? $"{mb:0.##}MB" :
                kb > 1 ? $"{kb:0.##}KB" :
                $"{bytes:0.##}B";

            result = result.Replace("/", ".");
            return result;
        }

        public static string CalculateMD5(string filename)
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

        public static void EndTask(string taskname)
        {
            var chromeDriverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == taskname);
            foreach (var process in chromeDriverProcesses)
            {
                process.Kill();
            }
        }

        public static void KillProcessAndChildrens(int pid)
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

        public static void ExecuteCMD(String strCommand)
        {
            try
            {
                Console.WriteLine(strCommand);
                ProcessStartInfo commandInfo = new ProcessStartInfo();
                commandInfo.CreateNoWindow = true;
                commandInfo.UseShellExecute = false;
                commandInfo.RedirectStandardInput = false;
                commandInfo.RedirectStandardOutput = false;
                commandInfo.FileName = "cmd.exe";
                commandInfo.Arguments = strCommand;
                if (commandInfo != null)
                {
                    Process process = Process.Start(commandInfo);
                    if (process != null)
                    {
                        //process.WaitForExit();
                        process.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // skip
            }
        }

        public static void Logger(string message, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " :" + message);
            Console.ResetColor();
        }

    }
}
