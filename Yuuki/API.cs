using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Json.Mod;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Yuuki
{
    public class API
    {
        public static string API_DL_CF = "https://file2.yuuki.me/";
        public static string API_DL_OW = "https://drive.yuuki.me/";
        public static string API_Yuuki = "https://ps.yuuki.me/";
        public static string WEB_LINK = "https://ps.yuuki.me";

        public static string API_GITHUB_YuukiPS = "https://api.github.com/repos/YuukiPS/Launcher-PC/";
        public static string API_GITHUB_RSA = "https://api.github.com/repos/34736384/RSAPatch/";

        public static Client? GS_DL(string dl = "os")
        {
            var client = new RestClient(API_Yuuki);
            var request = new RestRequest("api/genshin/download/latest/" + dl);

            var response = client.Execute(request);
            var getme = response.StatusCode == HttpStatusCode.OK ? response.Content : response.StatusCode.ToString();
            return JsonConvert.DeserializeObject<Client>(getme!);
        }

        public static VersionGenshin? GetMD5VersionGS(string md5)
        {
            var client = new RestClient(API_Yuuki);
            var request = new RestRequest("api/genshin/version?md5=" + md5.ToUpper());

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (response.Content != null)
                    {
                        var tes = JsonConvert.DeserializeObject<VersionGenshin>(response.Content);
                        if (tes != null)
                        {
                            return tes;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Logger.Error("API", "Error GET API: " + response.StatusCode);
            }
            return null;
        }

        public static Patch? GetMD5Game(string md5, GameType type_game)
        {
            var url = "json/" + type_game.SEOUrl() + "/version/patch/" + md5.ToUpper() + ".json";

            var client = new RestClient(API_Yuuki);
            var request = new RestRequest(url);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var isContent = response.Content;
                try
                {
                    var patch = JsonConvert.DeserializeObject<Patch>(isContent);
                    return patch;
                }
                catch (Exception ex)
                {
                    Logger.Error("API", $"Error getting patch data: {ex.Message} > {url}");
                    Logger.Error("API", isContent);
                }
            }
            else
            {
                Logger.Error("API", $"Error download patch data: {response.StatusCode} > {url}");
            }
            return null;
        }

        public static bool isYuuki(int port)
        {
            IWebProxy proxy = new WebProxy($"http://localhost:{port}");
            var options = new RestClientOptions("https://globaldp-prod-os01.starrails.com")
            {
                Proxy = proxy
            };
            var client = new RestClient(options);
            var request = new RestRequest("api");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var isContent = response.Content;
                if(isContent == "API YuukiPS v2")
                {
                    return true;
                }
                else
                {
                    Logger.Error("API", $"NO YUUKI :( > {isContent}");
                }
            }
            else
            {
                Logger.Error("API", $"Error check yuuki: {response.StatusCode}");
            }
            return false;
        }

        public static KeyGS? GSKEY()
        {
            var client = new RestClient(API_Yuuki);
            var request = new RestRequest("api/genshin/key/latest");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (response.Content != null)
                    {
                        var tes = JsonConvert.DeserializeObject<KeyGS>(response.Content);
                        if (tes != null)
                        {
                            return tes;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine("Error GET KEY: " + response.StatusCode);
            }
            return null;
        }

        public static ServerList? ServerList()
        {
            var client = new RestClient(API_Yuuki);
            var request = new RestRequest("api/launcher/server");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var tes = JsonConvert.DeserializeObject<ServerList>(response.Content);
                        if (tes != null)
                        {
                            return tes;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            }
            return null;
        }

        public static VersionServer? GetServerStatus(string url)
        {
            var s = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(s);
            var request = new RestRequest();

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var tes = JsonConvert.DeserializeObject<VersionServer>(response.Content);
                        if (tes != null)
                        {
                            return tes;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                Debug.Print("Error Host " + url + ": " + response.StatusCode);
            }
            return null;
        }

        public static Update? GetUpdate()
        {
            var client = new RestClient(API_GITHUB_YuukiPS);
            var request = new RestRequest("releases");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var tes = JsonConvert.DeserializeObject<List<Update>>(response.Content);
                        if (tes != null)
                        {
                            return tes[0];
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error GetUpdate1: ", ex);
                    }

                }
            }
            else
            {
                Console.WriteLine("Error GetUpdate2: " + response.StatusCode);
            }
            return null;
        }

        // TODO: Add multi support cheat
        public static Json.Mod.Config GetCheat(GameType game_type = GameType.GenshinImpact, int ch = 1, string ver_set = "3.8.0", string path_game = "")
        {
            var client = new RestClient(API_Yuuki);
            var request = new RestRequest("json/" + game_type.SEOUrl() + "/cheat.json");
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
                            var filteredGame = getData.Where(c => c.game == (int)game_type).ToList();
                            foreach (var game_cheat in filteredGame)
                            {
                                if (game_cheat.archives != null)
                                {
                                    // Filter archives based on version
                                    var filteredArchives = game_cheat.archives.Where(a => a.support.Contains(ver_set) && a.channel.Contains(ch)).ToList();
                                    // Process the filtered archives
                                    foreach (var archive in filteredArchives)
                                    {
                                        //Console.WriteLine($"{archive.url} found");
                                        //return archive;

                                        var Update_Cheat = false;
                                        var run_Cheat = false;

                                        var set_Cheat = Path.Combine(Json.Config.Modfolder, "Cheat", $"{(GameType)game_cheat.game}", $"{game_cheat.nama}", $"{archive.support}");
                                        Directory.CreateDirectory(set_Cheat);
                                        string get_Cheat = Path.Combine(set_Cheat, archive.config.launcher);
                                        string get_Cheat_zip = Path.Combine(set_Cheat, "update.zip");
                                        string get_Cheat_md5 = Path.Combine(set_Cheat, "md5.txt");

                                        // Check MD5
                                        if (!File.Exists(get_Cheat_md5))
                                        {
                                            // not found md5
                                            Update_Cheat = true;
                                            Console.WriteLine("md5 not found");
                                        }
                                        else
                                        {
                                            // found md5
                                            string readText = File.ReadAllText(get_Cheat_md5);
                                            if (!readText.Contains(archive.md5))
                                            {
                                                Console.WriteLine("md5 is not the same maybe because it's new, time to download");
                                                Update_Cheat = true;
                                            }
                                            else
                                            {
                                                run_Cheat = true;
                                                Console.WriteLine("Cheat already most recent");
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
                                            Console.WriteLine("Update Cheat...");

                                            if (string.IsNullOrEmpty(archive.url))
                                            {
                                                MessageBox.Show("No download links found");
                                            }
                                            MessageBox.Show(archive.comment, "Info Update: " + game_cheat.nama); // get better
                                            var DL2 = new Download(archive.url, get_Cheat_zip);
                                            if (DL2.ShowDialog() != DialogResult.OK)
                                            {
                                                MessageBox.Show("Download Cheat failed");
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
                                                    File.WriteAllText(get_Cheat_md5, archive.md5);

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
                                            Console.WriteLine("Skip Cheat...");
                                        }

                                        // RUN
                                        if (run_Cheat)
                                        {
                                            // Update folder
                                            if (!string.IsNullOrEmpty(path_game))
                                            {
                                                var file_config = @$"{set_Cheat}\{archive.config.save}";
                                                try
                                                {
                                                    if (archive.config.format == 1)
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
                                            archive.config.launcher = get_Cheat;

                                            return archive.config;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cheat datebase found but 404");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Get Cheat 2: " + ex);
                    }
                }
                else
                {
                    Console.WriteLine("NoData");
                }
            }
            else
            {
                Console.WriteLine("Error Get Cheat 1: " + response.StatusCode);
            }
            return null;
        }
    }
}
