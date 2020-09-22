using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SecureNative.SDK.Config;

namespace SecureNative.SDK.Http
{
    public class SecureNativeHTTPClient : IHttpClient
    {
        private readonly string AUTHORIZATION_HEADER = "Authorization";
        private readonly string VERSION_HEADER = "SN-Version";
        private readonly string USER_AGENT_HEADER = "User-Agent";
        private readonly string USER_AGENT_HEADER_VALUE = "SecureNative-dotnet";
        private readonly HttpClient Client = new HttpClient();
        private SecureNativeOptions Options;
        private static string JSON_MEDIA_TYPE = "application/json";
        private readonly static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SecureNativeHTTPClient(SecureNativeOptions options, HttpMessageHandler handler = null)
        {
            this.Options = options;
            if (handler != null)
            {
                this.Client = new HttpClient(handler);
            } else
            {
                this.Client = new HttpClient();
            }
    }

        public HttpResponse Post(string path, string body)
        {
            string url = this.Options.GetApiUrl() + "/"  + path;
            var data = new StringContent(body, Encoding.UTF8, JSON_MEDIA_TYPE);

            try
            {
                var response = Client.PostAsync(url, data).Result;

                if (response != null)
                {
                    try
                    {
                        var b = JsonConvert.SerializeObject(response);
                        return new HttpResponse(true, (int)response.StatusCode, b);
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("Failed to parse response body: " + e.ToString());
                        return new HttpResponse(false, (int)response.StatusCode, null);
                    }
                }
            } catch (Exception e)
            {
                Logger.Warn("Could not post request: " + e.ToString());
            }

            return new HttpResponse(false, 500, null);
        }
    }
}
