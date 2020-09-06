using System;
using System.Net.Http;
using SecureNative.SDK.Config;

namespace SecureNative.SDK.Http
{
    public class SecureNativeHTTPClient : IHttpClient
    {
        private readonly string AUTHORIZATION_HEADER = "Authorization";
        private readonly string VERSION_HEADER = "SN-Version";
        private readonly string USER_AGENT_HEADER = "User-Agent";
        private readonly string USER_AGENT_HEADER_VALUE = "SecureNative-java";
        static readonly HttpClient Client = new HttpClient();
        private SecureNativeOptions Options;
        public static string JSON_MEDIA_TYPE = "application/json; charset=utf-8";

        public SecureNativeHTTPClient(SecureNativeOptions options)
        {
            this.Options = options;
        }

        public HttpResponse Post(string url, string body)
        {
            throw new NotImplementedException();
        }
    }
}
