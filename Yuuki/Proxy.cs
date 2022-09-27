using System.Net;
using System.Net.Security;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace YuukiPS_Launcher.Yuuki
{
    public class Proxy
    {
        public ProxyServer proxyServer;
        private ExplicitProxyEndPoint explicitEndPoint;

        private int port;
        private string ps;
        private bool usehttps;

        public Proxy(int port, string host, bool usehttps)
        {
            this.port = port;
            ps = host;
            this.usehttps = usehttps;
        }

        [Obsolete]
        public bool Start()
        {
            proxyServer = new ProxyServer();

            // Install Certificate
            proxyServer.CertificateManager.EnsureRootCertificate();

            // Get Request Data
            proxyServer.BeforeRequest += OnRequest;
            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;

            try
            {
                //Tool.findAndKillProcessRuningOn("" + port + "");
            }
            catch (Exception ex)
            {
                // skip
            }

            explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, port, true);

            // Fired when a CONNECT request is received
            explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

            // An explicit endpoint is where the client knows about the existence of a proxy So client sends request in a proxy friendly manner
            try
            {
                proxyServer.AddEndPoint(explicitEndPoint);
                proxyServer.Start();
            }
            catch (Exception ex)
            {
                // https://stackoverflow.com/a/41340197/3095372
                // https://stackoverflow.com/a/69051680/3095372
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Error Start Proxy: {0}", ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine("Error Start Proxy: {0}", ex.Message);
                }
                return false;
            }

            foreach (var endPoint in proxyServer.ProxyEndPoints)
            {
                Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ", endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);
            }

            // Only explicit proxies can be set as system proxy!
            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

            return true;

        }

        [Obsolete]
        public void Stop()
        {
            try
            {
                explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;
                proxyServer.BeforeRequest -= OnRequest;
                proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Stop Proxy: ", ex);
            }
            finally
            {
                if (proxyServer.ProxyRunning)
                {
                    Console.WriteLine("Proxy Stop");
                    proxyServer.Stop();
                    //UninstallCertificate();
                    proxyServer.Dispose();
                }
                else
                {
                    Console.WriteLine("Proxy tries to stop but the proxy is not running.");
                }
            }
        }

        public void UninstallCertificate()
        {
            proxyServer.CertificateManager.RemoveTrustedRootCertificate();
            proxyServer.CertificateManager.RemoveTrustedRootCertificateAsAdmin();
        }

        [Obsolete]
        private Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            // Do not decrypt SSL if not required domain/host
            string hostname = e.WebSession.Request.RequestUri.Host;
            if (
                hostname.EndsWith(".yuanshen.com") |
                hostname.EndsWith(".hoyoverse.com") |
                hostname.EndsWith(".mihoyo.com") |
                hostname.EndsWith(ps))
            {
                e.DecryptSsl = true;
            }
            else
            {
                e.DecryptSsl = false;
            }
            return Task.CompletedTask;
        }

        [Obsolete]
        private Task OnRequest(object sender, SessionEventArgs e)
        {
            // Change Host
            string hostname = e.WebSession.Request.RequestUri.Host;
            if (
                hostname.EndsWith(".yuanshen.com") |
                hostname.EndsWith(".hoyoverse.com") |
                hostname.EndsWith(".mihoyo.com"))
            {
                var q = e.WebSession.Request.RequestUri;

                var url = e.HttpClient.Request.Url;

                //Console.WriteLine("Request Original: " + url);

                if (!usehttps)
                {
                    url = url.Replace("https", "http");
                }

                url = url.Replace(q.Host, ps);

                Tool.Logger("Request " + url, ConsoleColor.Green);

                // Set
                e.HttpClient.Request.Url = url;

            }
            return Task.CompletedTask;
        }

        // Allows overriding default certificate validation logic
        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // set IsValid to true/false based on Certificate Errors
            if (e.SslPolicyErrors == SslPolicyErrors.None)
            {
                e.IsValid = true;
            }
            return Task.CompletedTask;
        }

    }
}
