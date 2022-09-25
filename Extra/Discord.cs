using DiscordRPC;
using DiscordRPC.Logging;
using Button = DiscordRPC.Button;

namespace YuukiPS_Launcher.Extra
{
    public class Discord
    {
        public DiscordRpcClient? client;

        public void Ready(string appid = "1023479009335582830")
        {
            if (client == null)
            {
                client = new DiscordRpcClient(appid);
                client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
                client.RegisterUriScheme();

                //Subscribe to events
                client.OnReady += (sender, e) =>
                {
                    Console.WriteLine("Discord: Received Ready from user {0}", e.User.Username);
                    UpdateStatus("Getting ready", "Wait");
                };
                client.OnError += (sender, e) =>
                {
                    Console.WriteLine("Discord: Error Update {0}", e.Message);
                };

                //Connect to the RPC
                client.Initialize();
            }

        }

        public void UpdateStatus(string details, string state, string iconkey = "", int type = 0)
        {
            if (client != null)
            {
                var Editor = new RichPresence()
                {
                    Details = details,
                    State = state
                };
                var Detail = new Assets()
                {
                    LargeImageKey = "yuuki",
                    LargeImageText = "YuukiPS"
                };

                if (!string.IsNullOrEmpty(iconkey))
                {
                    Detail.LargeImageKey = iconkey;
                    Detail.LargeImageText = state;
                }

                if (type == 0)
                {
                    Detail.SmallImageKey = "offline";
                    Detail.SmallImageText = "Offline";
                }
                else if (type == 1)
                {
                    Detail.SmallImageKey = "online";
                    Detail.SmallImageText = "Online";
                }

                if (state.Contains("In Game"))
                {
                    Editor.Buttons = new Button[]
                     {
                       new Button() { Label = "Join", Url = "https://ps.yuuki.me/" }
                     };
                }

                Editor.Assets = Detail;

                client.SetPresence(Editor);
            }

        }

        public void Stop()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }
    }
}
