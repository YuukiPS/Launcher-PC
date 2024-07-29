using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Net;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Yuuki
{
    public class Server
    {
        //private static string API_GITHUB_DockerGS = "https://api.github.com/repos/YuukiPS/DockerGS/";
        //private static string API_GITHUB_Grasscutter = "https://api.github.com/repos/Grasscutters/Grasscutter/";
        //private static string API_DL_Grasscutter_Resources = "https://gitlab.com/yukiz/GrasscutterResources/";

        public static string Serverfolder { get; } = Path.Combine(Config.CurrentlyPath, "server");

        private static readonly string JavaVersion = "17";
        private static readonly string GithubApiJava = "https://api.github.com/repos/adoptium/temurin" + JavaVersion + "-binaries/";
        private static readonly string JavaVersionLock = "17.0.4.1_1";
        private static readonly string JavaPathFolder = Path.Combine(Serverfolder, "java");
        private static readonly string JavaPath = Path.Combine(JavaPathFolder, "java.zip");

        public static string DLJava()


        {
            if (!Directory.Exists(JavaPathFolder))
            {
                Logger.Info("Server", "Java folder not found. Creating a new folder...");
                Directory.CreateDirectory(JavaPathFolder);
            }
            Logger.Info("Server", "Checking Java version...");
            var Javabin = Path.Combine(JavaPathFolder, "bin");
            if (CheckJava(Javabin))
            {
                return "Java is already at the latest version.";
            }

            var found_zip = false;

            // skip download for debug
            /*
            if (File.Exists(GetJavaZip))
            {
                found_zip = true;
            }
            */

            if (!found_zip)
            {
                Logger.Info("Server", "Retrieving latest Java information...");
                var GetJavaInfo = GetJava(JavaVersionLock);
                if (string.IsNullOrEmpty(GetJavaInfo))
                {
                    Logger.Error("Server", "Failed to retrieve Java information. Please check your internet connection and try again.");
                    return "Failed to retrieve Java information. Please check your internet connection and try again.";
                }

                Logger.Info("Server", "Starting Java download...");
                var DL = new Download(GetJavaInfo, JavaPath);
                if (DL.ShowDialog() != DialogResult.OK)
                {
                    return "Java download failed. Please check your internet connection and try again. If the problem persists, consider manually downloading Java from the official website.";
                }
                else
                {
                    found_zip = true;
                }
            }

            if (found_zip)
            {
                // Unzip zip
                try
                {
                    Console.WriteLine("Extracting Java files from the downloaded archive...");
                    FastZip fastZip = new();
                    string? fileFilter = null;
                    fastZip.ExtractZip(JavaPath, JavaPathFolder, fileFilter);
                }
                catch (Exception e)
                {
                    return $"Failed to unzip Java files: {e.Message}. Please check the downloaded file and try again.";
                }

                // Move folder version to root java folder
                var java_folder_version = Path.Combine(JavaPathFolder, "jdk-" + JavaVersionLock.Replace("_", "+"));
                if (Directory.Exists(java_folder_version))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(java_folder_version, JavaPathFolder, true);
                    //Tool.ExecuteCMD($"Move {java_folder_version}\\*.* {JAVA_FOLDER}");
                    Logger.Info("Server", "Java installation completed successfully. Java folder moved to the correct location.");
                }
                else
                {
                    return "Failed to move Java version folder. Possible reasons: folder doesn't exist, download failed, or unzip operation failed. Please check the logs for more details.";
                }

                // Once again make sure version is correct.
                if (!CheckJava(Javabin))
                {
                    return "Failed to verify Java installation after download and extraction. Please check your system configuration and try again.";
                }
            }

            return "";
        }
        public static bool CheckJava(string p = "")
        {
            try
            {
                ProcessStartInfo? procStartInfo = new ProcessStartInfo(p + "\\java", "-version ");
                procStartInfo!.RedirectStandardOutput = true;
                procStartInfo!.RedirectStandardError = true;
                procStartInfo!.UseShellExecute = false;
                procStartInfo!.CreateNoWindow = true;

                if (procStartInfo != null)
                {
                    Process proc = new()
                    {
                        StartInfo = procStartInfo!
                    };
                    proc.Start();
                    //Console.WriteLine("JAVA Folder: " + p);
                    string? version = proc.StandardError?.ReadLine();
                    if (version != null && version.Contains(JavaVersion))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return false;
        }

        public static string? GetJava(string ver_set = "17.0.4.1_1", string os = "x64_windows")
        {
            var client = new RestClient(GithubApiJava);
            var request = new RestRequest("releases");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var GetData = JsonConvert.DeserializeObject<List<Update>>(response.Content);
                        if (GetData != null)
                        {
                            // Get List Releases
                            foreach (var GetVersion in GetData)
                            {
                                // Get List Asset
                                var aseet = GetVersion.Assets;
                                if (aseet != null)
                                {
                                    foreach (var file in aseet)
                                    {
                                        if (file.Name == "OpenJDK17U-jdk_" + os + "_hotspot_" + ver_set + ".zip")
                                        {
                                            return file.BrowserDownloadUrl;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Server", $"Error in GetJava method: {ex.Message}");
                    }
                }
            }
            else
            {
                Logger.Error("Server", $"Error fetching Java updates: HTTP Status Code {response.StatusCode}");
            }
            return "";
        }
    }
}
