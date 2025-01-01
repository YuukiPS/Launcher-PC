using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Json.Mod;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Yuuki
{
    public class API
    {
        // public static string API_DL_CF = "https://file2.yuuki.me/";
        public static string WebLink { get; } = "https://ps.yuuki.me";
        public static string GithubApiYuukiPS { get; } = "https://api.github.com/repos/YuukiPS/Launcher-PC/";

        public static Patch? GetMD5Game(string md5, GameType typeGame)
        {
            var url = "/json/" + typeGame.SEOUrl() + "/version/patch/v2/" + md5.ToUpper() + ".json?time=" + DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            var client = new RestClient(WebLink);
            var request = new RestRequest(url);

            request.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            request.AddHeader("Pragma", "no-cache");
            request.AddHeader("Expires", "0");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var isContent = response.Content;
                try
                {
                    if (isContent != null)
                    {
                        var patch = JsonConvert.DeserializeObject<Patch>(isContent);
                        return patch;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("API", $"Error getting patch data: {ex.Message} > {url}");
                    Logger.Error("API", isContent ?? "No error content");
                }
            }
            else
            {
                Logger.Error("API", $"Failed to download patch data. Status code: {response.StatusCode}. URL: {url}.");
            }
            return null;
        }

        public static bool IsYuuki(int port)
        {
            IWebProxy proxy = new WebProxy($"http://localhost:{port}");
            var options = new RestClientOptions("https://globaldp-prod-os01.starrails.com")
            {
                Proxy = proxy
            };
            var client = new RestClient(options);
            var request = new RestRequest("api");

            request.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            request.AddHeader("Pragma", "no-cache");
            request.AddHeader("Expires", "0");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var isContent = response.Content;
                if (isContent == "API YuukiPS v2")
                {
                    return true;
                }
                else
                {
                    Logger.Error("API", $"YuukiPS API not detected. Unexpected response: {isContent}");
                }
            }
            else
            {
                Logger.Error("API", $"Failed to check YuukiPS API. Status code: {response.StatusCode}. URL: {options.BaseUrl}/api");
            }
            return false;
        }

        public static Update? GetUpdate()
        {
            var client = new RestClient(GithubApiYuukiPS);
            var request = new RestRequest("releases");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var updates = System.Text.Json.JsonSerializer.Deserialize<List<Update>>(response.Content, options);
                        if (updates != null && updates.Count > 0)
                        {
                            return updates[0];
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("API", $"Error deserializing update data: {e.Message}");
                        Logger.Error("API", $"Stack trace: {e.StackTrace}");
                    }

                }
            }
            else
            {
                Logger.Error("API", $"Failed to fetch update information. Status code: {response.StatusCode}");
            }
            return null;
        }

        // TODO: Add multi support cheat
        public static Json.Mod.Config GetCheat(GameType game_type = GameType.GenshinImpact, int ch = 1, string ver_set = "3.8.0", string path_game = "")
        {
            var client = new RestClient(WebLink);
            var request = new RestRequest("/json/" + game_type.SEOUrl() + "/cheat.json");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        //Console.WriteLine($"tes {ver_set}: " + JsonConvert.SerializeObject(response.Content));
                        var getData = JsonConvert.DeserializeObject<List<Cheat>>(response.Content);
                        if (getData != null)
                        {
                            var filteredGame = getData.Where(c => c.Game == (int)game_type).ToList();
                            foreach (var game_cheat in filteredGame)
                            {
                                if (game_cheat.Archives != null)
                                {
                                    // Filter archives based on version
                                    var filteredArchives = game_cheat.Archives.Where(a => a.Support.Contains(ver_set) && a.Channel.Contains(ch)).ToList();
                                    // Process the filtered archives
                                    foreach (var archive in filteredArchives)
                                    {
                                        //Console.WriteLine($"{archive.url} found");
                                        //return archive;

                                        var Update_Cheat = false;
                                        var run_Cheat = false;

                                        var set_Cheat = Path.Combine(Json.Config.Modfolder, "Cheat", $"{(GameType)game_cheat.Game}", $"{game_cheat.Nama}", $"{archive.Support}");
                                        Directory.CreateDirectory(set_Cheat);
                                        string get_Cheat = Path.Combine(set_Cheat, archive.Config.Launcher);
                                        string get_Cheat_zip = Path.Combine(set_Cheat, "update.zip");
                                        string get_Cheat_md5 = Path.Combine(set_Cheat, "md5.txt");

                                        // Check MD5
                                        if (!File.Exists(get_Cheat_md5))
                                        {
                                            // not found md5
                                            Update_Cheat = true;
                                            Logger.Warning("API", $"MD5 file not found for cheat: {game_cheat.Nama}. Initiating download.");
                                        }
                                        else
                                        {
                                            // found md5
                                            string readText = File.ReadAllText(get_Cheat_md5);
                                            if (!readText.Contains(archive.Md5))
                                            {
                                                Logger.Info("API", $"MD5 mismatch detected for cheat: {game_cheat.Nama}. New version available. Initiating download.");
                                                Update_Cheat = true;
                                            }
                                            else
                                            {
                                                run_Cheat = true;
                                                Logger.Info("API", $"Cheat '{game_cheat.Nama}' is already up to date. No update required.");
                                            }
                                            /*
                                            if (!readText.Contains(cekCheat.version))
                                            {
                                                Console.WriteLine("version is not the same maybe because it's new, time to download");
                                                Update_Cheat = true;
                                            }
                                            */
                                        }

                                        // Need Update
                                        if (Update_Cheat)
                                        {
                                            Logger.Info("API", $"Updating cheat '{game_cheat.Nama}' for {(GameType)game_cheat.Game}...");

                                            if (string.IsNullOrEmpty(archive.Url))
                                            {
                                                Logger.Error("API", "No download links found for cheat update.");
                                                MessageBox.Show("No download links found for cheat update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            MessageBox.Show(archive.Comment, "Info Update: " + game_cheat.Nama); // get better
                                            var DL2 = new Download(archive.Url, get_Cheat_zip);
                                            if (DL2.ShowDialog() != DialogResult.OK)
                                            {
                                                Logger.Error("API", $"Failed to download cheat: {game_cheat.Nama}");
                                                MessageBox.Show($"Failed to download cheat: {game_cheat.Nama}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            else
                                            {
                                                // if download done....
                                                var file_update_Cheat = set_Cheat + @"\update.bat";
                                                try
                                                {
                                                    // Make bat file for update
                                                    var w = new StreamWriter(file_update_Cheat);
                                                    w.WriteLine("@echo off");

                                                    w.WriteLine("cd \"" + set_Cheat + "\" ");

                                                    // Kill Cheat
                                                    //w.WriteLine("Taskkill /IM Launcher.exe /F");

                                                    // Unzip file
                                                    w.WriteLine("echo unzip file...");
                                                    w.WriteLine("tar -xvf update.zip");

                                                    //delete file old
                                                    w.WriteLine("echo delete file zip");
                                                    w.WriteLine("del /F update.zip");

                                                    // del update
                                                    w.WriteLine("del /F update.bat");
                                                    w.Close();

                                                    Process.Start(new ProcessStartInfo(file_update_Cheat))?.WaitForExit();

                                                    // Update MD5
                                                    File.WriteAllText(get_Cheat_md5, archive.Md5);

                                                    run_Cheat = true;

                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            Logger.Info("API", $"Skipping cheat update for {game_cheat.Nama} as it's already up to date.");
                                        }

                                        // RUN
                                        if (run_Cheat)
                                        {
                                            // Update folder
                                            if (!string.IsNullOrEmpty(path_game))
                                            {
                                                var file_config = @$"{set_Cheat}\{archive.Config.Save}";
                                                try
                                                {
                                                    if (archive.Config.Format == 1)
                                                    {
                                                        var w = new StreamWriter(file_config);
                                                        w.WriteLine("[Inject]");
                                                        w.WriteLine("GenshinPath = " + path_game);
                                                        w.WriteLine("[System]");
                                                        w.WriteLine("InitializationDelayMS = 25000");
                                                        w.Close();
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    //MessageBox.Show(ex.Message);
                                                    //return;
                                                }
                                            }

                                            // set path
                                            archive.Config.Launcher = get_Cheat;

                                            return archive.Config;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cheat database found, but returned a 404 Not Found error.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("API", $"Error retrieving cheat data: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Logger.Error("API", $"Inner exception: {ex.InnerException.Message}");
                        }
                        Console.WriteLine($"Error retrieving cheat data. Check the log file for details.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: No cheat data received from the server.");
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving cheat data: HTTP status code {response.StatusCode}");
            }
            return null!;
        }
    }
}
