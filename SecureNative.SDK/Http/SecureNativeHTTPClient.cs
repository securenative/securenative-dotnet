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
        private readonly string USER_AGENT_HEADER_VALUE = "SecureNative-java";
        private static readonly HttpClient Client = new HttpClient();
        private SecureNativeOptions Options;
        private static string JSON_MEDIA_TYPE = "application/json";

        public SecureNativeHTTPClient(SecureNativeOptions options)
        {
            this.Options = options;
        }

        public HttpResponse Post(string path, string body)
        {
            string url = String.Format("%s/%s", this.Options.GetApiUrl(), path);
            var data = new StringContent(body, Encoding.UTF8, JSON_MEDIA_TYPE);
            var response = Client.PostAsync(url, data);

            if (response != null)
            {
                var b = JsonConvert.SerializeObject(response);
                return new HttpResponse(true, (int)response.Result.StatusCode, b);
            }

            return new HttpResponse(false, 500, null);
        }
    }
}
