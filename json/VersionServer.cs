namespace YuukiPS_Launcher.Json
{
    public class VersionServer
    {
        public int retcode { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public int MemoryMax { get; set; }
        public int MemoryCurrently { get; set; }
        public int MemoryInit { get; set; }
        public int MemoryCommitted { get; set; }
        public int Thread { get; set; }
        public int ThreadTotalStarted { get; set; }
        public int ThreadDaemon { get; set; }
        public int TotalAccount { get; set; }
        public int playerCount { get; set; }
        public int maxPlayer { get; set; }
        public string DockerGS { get; set; }
        public string Version { get; set; }
    }
}
