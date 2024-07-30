using System.Net;
using System.Net.Security;
using System.Security.Policy;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Yuuki
{
    public class Proxy
    {
        public ProxyServer? proxyServer = null;
        private ExplicitProxyEndPoint? explicitEndPoint = null;

        private readonly int port;
        private readonly Uri our_server;

        private readonly bool sendLog;
        private readonly bool showLog;

        public Proxy(int port, string host, bool sendLog, bool showLog)
        {
            this.port = port;
            our_server = new Uri(host);
            this.sendLog = sendLog;
            this.showLog = showLog;
        }

        public bool Start()
        {
            proxyServer = new ProxyServer();

            // locally trust root certificate used by this proxy
            try
            {
                // proxyServer.CertificateManager.TrustRootCertificate(true);
                proxyServer.CertificateManager.EnsureRootCertificate();
            }
            catch (Exception ex)
            {
                Logger.Error("Proxy", "Error Start Proxy: " + ex.Message);
            }

            // Get Request Data
            proxyServer.BeforeRequest += OnRequest;
            proxyServer.BeforeResponse += OnResponse;
            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback += OnCertificateSelection;

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
                    Logger.Error("Proxy", "Error Start Proxy: " + ex.InnerException.Message);
                }
                else
                {
                    Logger.Error("Proxy", "Error Start Proxy: " + ex.Message);
                }
                return false;
            }

            foreach (var endPoint in proxyServer.ProxyEndPoints)
            {
                Logger.Info("Proxy", $"Listening on {endPoint.GetType().Name} endpoint at Ip {endPoint.IpAddress} and port: {endPoint.Port} ");
            }

            // Only explicit proxies can be set as system proxy!
            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

            return true;

        }
        private Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.CompletedTask;
        }

        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // set IsValid to true/false based on Certificate Errors
            if (e.SslPolicyErrors == SslPolicyErrors.None)
            {
                e.IsValid = true;
            }
            return Task.CompletedTask;
        }
        private async Task OnResponse(object sender, SessionEventArgs e)
        {
            string hostname = e.HttpClient.Request.RequestUri.Host;
            string url = e.HttpClient.Request.Url;

            if (HostPrivate(hostname) && this.showLog)
            {
                var bodyString = await e.GetResponseBodyAsString();
                Logger.Info("Proxy", $"Response: {url}\nBody: {bodyString}");
            }

        }

        public void Stop()
        {
            try
            {
                // Unsubscribe & Quit
                if (explicitEndPoint != null)
                {
                    explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;
                }
                if (proxyServer != null)
                {
                    proxyServer.BeforeRequest -= OnRequest;
                    proxyServer.BeforeResponse -= OnResponse;

                    proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
                    proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Proxy", $"Error while unsubscribing events during proxy stop: {ex.Message}");
            }
            finally
            {
                if (proxyServer != null && proxyServer.ProxyRunning)
                {
                    Logger.Info("Proxy", "Stopping proxy server...");
                    proxyServer.Stop();
                    proxyServer.Dispose();
                    Logger.Info("Proxy", "Proxy server stopped and disposed successfully");
                }
                else
                {
                    Logger.Warning("Proxy", "Attempt to stop proxy server, but it was not running");
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
            // All domains must be decrypt ssl
            e.DecryptSsl = true;
            await Task.CompletedTask;
        }

        private async Task OnRequest(object sender, SessionEventArgs e)
        {
            string hostname = e.HttpClient.Request.RequestUri.Host;
            string url = e.HttpClient.Request.Url;

            if (HostPrivate(hostname))
            {
                // log useless
                if (
                url.Contains("/sdk/dataUpload") || // GI Any
                url.Contains("/h5/dataUpload") || // Hoyolab
                url.Contains("/h5/upload") || // Hoyolab 2
                url.Contains("/common/h5log/log") || // Hoyolab 3
                url.Contains("8888/log") || // GI
                url.Contains("sentry_key") // CL
            )
            {
                e.Ok("{ code: 0 }");
                return;
            }

            // good log
            if (
                url.Contains("/apm/dataUpload") || // SR
                url.Contains("/crash/dataUpload") // GI
            )
            {
                if (!sendLog)
                {
                    e.Ok("{ code: 0 }");
                    return;
                }
            }

            // Ignore this URL
            if (
                url.Contains("/client/") || // Download Game
                url.Contains("/ptolemaios_api/") ||
                url.Contains("/hyp/")
            )
            {
                return;
            }

            var method = e.HttpClient.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                // Get/Set request body as string
                string bodyString = await e.GetRequestBodyAsString();
                e.SetRequestBodyString(bodyString);

                // store request, so that you can find it from response handler 
                e.UserData = e.HttpClient.Request;

                if(this.showLog) Logger.Info("Proxy", $"Request: {url}\nBody: {bodyString}");
            } else {
                if (this.showLog) Logger.Info("Proxy", $"Request: {url} | Method: {method}");
            }

            // Set url private server            
            UriBuilder uriBuilder = new UriBuilder(url)
             {
              Scheme = our_server.Scheme,
              Host = our_server.Host,
              Port = our_server.Port
             };
             var newUrl = uriBuilder.Uri;
             e.HttpClient.Request.Url = newUrl.ToString();

            } else {
                // Ignore
            }
        }

        private static bool HostPrivate(string hostname)
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



    }
}
