using Newtonsoft.Json;
using RestSharp;
using System.Net;
using YuukiPS_Launcher.json;

namespace YuukiPS_Launcher
{
    public class API
    {
        public static string API_DL_CF = "https://file.yuuki.me/";
        public static string API_DL_OW = "https://drive.yuuki.me/";
        public static string API_DL_WB = "https://ps.yuuki.me/api/";
        public static string API_GITHUB = "https://api.github.com/repos/akbaryahya/YuukiPS-Launcher/";

        public static GS GS_DL(string dl = "os")
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("genshin/download/latest/" + dl);

            var response = client.Execute(request);
            var getme = response.StatusCode == HttpStatusCode.OK ? response.Content : response.StatusCode.ToString();
            return JsonConvert.DeserializeObject<GS>(getme);
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

        public static VersionGS? GetServerStatus(string url)
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
                        var tes = JsonConvert.DeserializeObject<VersionGS>(response.Content);
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
                Console.WriteLine("Error Host " + url + ": " + response.StatusCode);
            }
            return null;
        }

        public static Update? GetUpdate()
        {
            var client = new RestClient(API_GITHUB);
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
    }
}
