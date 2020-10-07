using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NLog;
using SecureNative.SDK.Config;

namespace SecureNative.SDK.Http
{
    public class SecureNativeHttpClient : IHttpClient
    {
        private const string AuthorizationHeader = "Authorization";
        private const string VersionHeader = "SN-Version";
        private const string UserAgentHeader = "User-Agent";
        private const string UserAgentHeaderValue = "SecureNative-dotnet";
        private const string JsonMediaType = "application/json";
        private readonly HttpClient _client;
        private readonly SecureNativeOptions _options;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SecureNativeHttpClient(SecureNativeOptions options, HttpMessageHandler handler = null)
        {
            _options = options;
            _client = handler != null ? new HttpClient(handler) : new HttpClient();
        }

        public HttpResponse Post(string path, string body)
        {
            var url = _options.GetApiUrl() + "/"  + path;
            
            try
            {
                var data = new StringContent(body, Encoding.UTF8, JsonMediaType);
                var response = _client.PostAsync(url, data).Result;

                if (response != null)
                {
                    try
                    {
                        var b = JsonConvert.SerializeObject(response);
                        return new HttpResponse(true, (int)response.StatusCode, b);
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("Failed to parse response body: " + e);
                        return new HttpResponse(false, (int)response.StatusCode, null);
                    }
                }
            } catch (Exception e)
            {
                Logger.Warn("Could not post request: " + e);
            }

            return new HttpResponse(false, 500, null);
        }
    }
}
