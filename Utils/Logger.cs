using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YuukiPS_Launcher.Utils
{
    internal class Logger
    {
        public static string logPath = "";
        public static string strMsg = "";

        public void initLogging(string startMsg, string lPath)
        {
            logPath = lPath;
            strMsg = startMsg;

            File.WriteAllText(logPath, strMsg);
        }

        public static void Info(string type, string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(DateTime.Now.ToString("hh:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(type);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("INFO");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(msg + "\n");

            File.AppendAllText(logPath, $"[${DateTime.Now.ToString("hh:mm:ss")}] [${type}:INFO] "+msg+"\n");
        }

        public static void Error(string type, string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(DateTime.Now.ToString("hh:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(type);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERROR");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(msg + "\n");

            File.WriteAllText(logPath, $"[${DateTime.Now.ToString("hh:mm:ss")}] [${type}:ERROR] " + msg + "\n");
        }
        public static void Warning(string type, string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(DateTime.Now.ToString("hh:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(type);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("WARNING");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(msg + "\n");
            File.WriteAllText(logPath, $"[${DateTime.Now.ToString("hh:mm:ss")}] [${type}:WARNING] " + msg + "\n");
        }
    }
}
