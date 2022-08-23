namespace YuukiPS_Launcher
{
    internal static class Program
    {

        public static string DataConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create missing Files
            Directory.CreateDirectory(DataConfig);

            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}