using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
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

        public static ServerList ServerList()
        {
            var client = new RestClient(API_DL_WB);
            var request = new RestRequest("launcher/server");

            var response = client.Execute(request);
            var getme = response.StatusCode == HttpStatusCode.OK ? response.Content : response.StatusCode.ToString();
            return JsonConvert.DeserializeObject<ServerList>(getme);
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
                var getme = response.Content;
                if (getme != null)
                {
                    var tes = JsonConvert.DeserializeObject<VersionGS>(getme);
                    if (tes != null)
                    {
                        return tes;
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
            var client = new RestClient(API_GITHUB);
            var request = new RestRequest("releases");

            var response = client.Execute(request);
            var getme = response.StatusCode == HttpStatusCode.OK ? response.Content : response.StatusCode.ToString();
            if (getme != null)
            {
                var tes = JsonConvert.DeserializeObject<List<Update>>(getme);
                if (tes != null)
                {
                    return tes[0];
                }
            }
            return null;
        }
    }
}
