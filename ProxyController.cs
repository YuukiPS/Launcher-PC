using System.Net;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace YuukiPS_Launcher
{
    public class ProxyController
    {
        public ProxyServer proxyServer;
        private ExplicitProxyEndPoint explicitEndPoint;

        private int port;
        private string ps;

        public ProxyController(int port, string host)
        {
            this.port = port;
            this.ps = host;
        }

        [Obsolete]
        public void Start()
        {
            proxyServer = new ProxyServer();

            // Install Certificate
            proxyServer.CertificateManager.EnsureRootCertificate();

            // Get Request Data
            proxyServer.BeforeRequest += OnRequest;


            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;

            explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, port, true);

            // Fired when a CONNECT request is received
            explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

            // An explicit endpoint is where the client knows about the existence of a proxy So client sends request in a proxy friendly manner
            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();

            foreach (var endPoint in proxyServer.ProxyEndPoints)
            {
                Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ", endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);
            }

            // Only explicit proxies can be set as system proxy!
            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

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

                Console.WriteLine("Request Original: " + url);

                var urlps = url.Replace(q.Host, ps);

                Console.WriteLine("Request Private: " + urlps);

                // Set
                e.HttpClient.Request.Url = urlps;

            }
            return Task.CompletedTask;
        }

        // Allows overriding default certificate validation logic
        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // Check if valid?
            e.IsValid = true;
            return Task.CompletedTask;
        }

    }
}
