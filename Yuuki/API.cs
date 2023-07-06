using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Net;
using YuukiPS_Launcher.Json;
using YuukiPS_Launcher.Json.GameClient;
using YuukiPS_Launcher.Json.Mod;

namespace YuukiPS_Launcher.Yuuki
{
    public class API
    {
        public static string API_DL_CF = "https://file.yuuki.me/";
        public static string API_DL_OW = "https://drive.yuuki.me/";
        public static string API_DL_WB = "https://ps.yuuki.me/api/";

        public static string API_GITHUB_YuukiPS = "https://api.github.com/repos/YuukiPS/Launcher-PC/";
        public static string API_GITHUB_RSA = "https://api.github.com/repos/34736384/RSAPatch/";

        public static Client GS_DL(string dl = "os")
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("genshin/download/latest/" + dl);

            var response = client.Execute(request);
            var getme = response.StatusCode == HttpStatusCode.OK ? response.Content : response.StatusCode.ToString();
            return JsonConvert.DeserializeObject<Client>(getme);
        }

        public static VersionGenshin GetMD5VersionGS(string md5)
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("genshin/version?md5=" + md5.ToUpper());

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
                    Console.WriteLine("Error: ", ex);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
            return null;
        }

        public static Patch GetMD5Game(string md5, GameType type_game)
        {
            var url = type_game + "/patch/" + md5.ToUpper();
            Console.WriteLine("GetMD5Game: " + md5 + " url: " + url);
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest(url);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (response.Content != null)
                    {
                        var tes = JsonConvert.DeserializeObject<Patch>(response.Content);
                        if (tes != null)
                        {
                            return tes;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
            return null;
        }

        public static KeyGS? GSKEY()
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("genshin/key/latest");

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
                    Console.WriteLine("Error: ", ex);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
            return null;
        }

        public static ServerList? ServerList()
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("launcher/server");
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
                        Console.WriteLine("Error: ", ex);
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
                        Console.WriteLine("Error: ", ex);
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

        public static string? GetAkebi(int ch = 1, string ver_set = "3.2.0")
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("genshin/mod/akebi/" + ver_set);
            var response = client.Execute(request);

            var whos = "cn";
            if (ch == 2)
            {
                whos = "os";
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    try
                    {
                        var GetData = JsonConvert.DeserializeObject<Akebi>(response.Content);
                        if (GetData != null)
                        {
                            if (ch == 2)
                            {
                                return GetData.package.cn.md5 + "|" + GetData.package.cn.url;
                            }
                            else
                            {
                                return GetData.package.os.md5 + "|" + GetData.package.os.url;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error GetAkebi: ", ex);
                    }

                }
            }
            else
            {
                Console.WriteLine("Error GetUpdate2: " + response.StatusCode);
            }
            return "";
        }
    }
}
