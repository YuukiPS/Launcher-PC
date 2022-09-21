using Microsoft.DotNet.PlatformAbstractions;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace YuukiPS_Launcher
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

        public const string UNIX_PID_REGX = @"\w+\s+(\d+).*";
        public const string WIND_PID_REGX = @".*\s+(\d+)";
        public static void findAndKillProcessRuningOn(string port)
        {
            List<string> pidList = new List<string>();
            List<string> list = new List<string>();
            switch (getOSName())
            {
                case Platform.Linux:
                    list = findUnixProcess();
                    list = filterProcessListBy(processList: list, filter: ":" + port);

                    foreach (string pidString in list)
                    {
                        string pid = getPidFrom(pidString: pidString, pattern: UNIX_PID_REGX);

                        if (!String.IsNullOrEmpty(pid))
                        {
                            pidList.Add(pid);
                        }
                    }
                    break;

                case Platform.Windows:
                    list = findWindowsProcess();
                    list = filterProcessListBy(processList: list, filter: ":" + port);

                    foreach (string pidString in list)
                    {
                        string pid = getPidFrom(pidString: pidString, pattern: WIND_PID_REGX);

                        if (!String.IsNullOrEmpty(pid))
                        {
                            pidList.Add(pid);
                        }
                    }
                    break;
                default:
                    Console.WriteLine("No match found");
                    break;
            }

            foreach (string pid in pidList)
            {
                killProcesBy(pidString: pid);
            }
        }

        public static Platform getOSName()
        {
            string os = System.Environment.OSVersion.VersionString;
            Console.WriteLine("OS = {0}", os);

            if (os != null && os.ToLower().Contains("unix"))
            {
                Console.WriteLine("UNXI machine");
                return Platform.Linux;
            }
            else
            {
                Console.WriteLine("Windows machine");
                return Platform.Windows;
            }
        }

        public static void killProcesBy(string pidString)
        {
            int pid = -1;
            if (pidString != null && int.TryParse(s: pidString, result: out pid))
            {
                Process p = Process.GetProcessById(pid);
                p.Kill();
                Console.WriteLine("Killed pid =" + pidString);
            }
            else
            {
                Console.WriteLine("Process not found for pid =" + pidString);
            }

        }

        public static List<String> findUnixProcess()
        {
            ProcessStartInfo processStart = new ProcessStartInfo();
            processStart.FileName = "bash";
            processStart.Arguments = "-c lsof -i";

            processStart.RedirectStandardOutput = true;
            processStart.UseShellExecute = false;
            processStart.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processStart;
            process.Start();

            String outstr = process.StandardOutput.ReadToEnd();

            return splitByLineBreak(outstr);
        }

        public static List<String> findWindowsProcess()
        {
            ProcessStartInfo processStart = new ProcessStartInfo();
            processStart.FileName = "netstat.exe";
            processStart.Arguments = "-aon";

            processStart.RedirectStandardOutput = true;
            processStart.UseShellExecute = false;
            processStart.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processStart;
            process.Start();

            String outstr = process.StandardOutput.ReadToEnd();

            return splitByLineBreak(outstr);
        }

        public static List<string> splitByLineBreak(string processLines)
        {
            List<string> processList = new List<string>();

            if (processLines != null)
            {
                string[] list = processLines.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                processList.AddRange(collection: list);
            }

            return processList;
        }

        public static List<String> filterProcessListBy(List<String> processList,
                                                   String filter)
        {

            if (processList == null)
            {
                return new List<string>();
            }

            if (filter == null)
            {
                return processList;
            }

            return processList.FindAll(i => i != null && i.ToLower().Contains(filter.ToLower()));
        }

        public static String getPidFrom(String pidString, String pattern)
        {
            MatchCollection matches = Regex.Matches(pidString, pattern);

            if (matches != null && matches.Count > 0)
            {
                return matches[0].Groups[1].Value;
            }

            return "";
        }

        public static void Logger(string message, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " :" + message);
            Console.ResetColor();
        }

    }
}
