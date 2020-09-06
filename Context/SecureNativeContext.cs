using System;
using System.Collections.Generic;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContext
    {
        private string ClientToken { get; set; }
        private string Ip { get; set; }
        private string RemoteIp { get; set; }
        private Dictionary<String, String> headers { get; set; }
        private string Url { get; set; }
        private string Method { get; set; }
        private string Body { get; set; }

        public SecureNativeContext()
        {
        }

        public SecureNativeContext(string clientToken, string ip, string remoteIp, Dictionary<string, string> headers, string url, string method, string body)
        {
            this.ClientToken = clientToken;
            this.Ip = ip;
            this.RemoteIp = remoteIp;
            this.headers = headers;
            this.Url = url;
            this.Method = method;
            this.Body = body;
        }
    }
}
