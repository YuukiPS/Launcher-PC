using System.Diagnostics;

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

        public static void Logger(string message, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " :" + message);
            Console.ResetColor();
        }

    }
}
