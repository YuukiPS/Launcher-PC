using System.Net;
using System.Net.Security;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace YuukiPS_Launcher.Yuuki
{
    public class Proxy
    {
        public ProxyServer? proxyServer = null;
        private ExplicitProxyEndPoint? explicitEndPoint = null;

        private int port;
        private Uri our_server;

        public Proxy(int port, string host)
        {
            this.port = port;
            this.our_server = new Uri(host);
        }

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
            catch (Exception)
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

        public void Stop()
        {
            try
            {
                if (explicitEndPoint != null)
                {
                    explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;
                }
                if (proxyServer != null)
                {
                    proxyServer.BeforeRequest -= OnRequest;
                    proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Stop Proxy: ", ex);
            }
            finally
            {
                if (proxyServer != null && proxyServer.ProxyRunning)
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
            if (proxyServer == null)
            {
                return;
            }
            proxyServer.CertificateManager.RemoveTrustedRootCertificate();
            proxyServer.CertificateManager.RemoveTrustedRootCertificateAsAdmin();
        }

        private async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            // Do not decrypt SSL if not required domain/host
            string hostname = e.HttpClient.Request.RequestUri.Host;
            if (HostPrivate(hostname) || hostname.EndsWith(our_server.Host))
            {
                e.DecryptSsl = true;
            }
            else
            {
                e.DecryptSsl = false;
            }
            await Task.CompletedTask;
        }

        private Task OnRequest(object sender, SessionEventArgs e)
        {
            // Change Host
            string hostname = e.HttpClient.Request.RequestUri.Host;
            if (HostPrivate(hostname))
            {
                var q = e.HttpClient.Request.RequestUri;

                var url = e.HttpClient.Request.Url;

                //Console.WriteLine("Request Original: " + url);

                // if host private server have https
                bool isHostIsHttps = our_server.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
                if (!isHostIsHttps)
                {
                    url = url.Replace("https", "http");
                }

                url = url.Replace(q.GetLeftPart(UriPartial.Authority), our_server.GetLeftPart(UriPartial.Authority));

                Tool.Logger("Request " + url, ConsoleColor.Green);

                // Set
                e.HttpClient.Request.Url = url;

            }
            return Task.CompletedTask;
        }

        private bool HostPrivate(string hostname)
        {
            if (
                hostname.EndsWith(".zenlesszonezero.com") |
                hostname.EndsWith(".honkaiimpact3.com") |
                hostname.EndsWith(".bhsr.com") |
                hostname.EndsWith(".starrails.com") |
                hostname.EndsWith(".yuanshen.com") |
                hostname.EndsWith(".hoyoverse.com") |
                hostname.EndsWith(".mihoyo.com"))
            {
                return true;
            }
            return false;
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
