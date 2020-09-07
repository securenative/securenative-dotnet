using System;
using System.Collections.Generic;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContext
    {
        private string ClientToken { get; set; }
        private string Ip { get; set; }
        private string RemoteIp { get; set; }
        private Dictionary<String, String> Headers { get; set; }
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
            this.Headers = headers;
            this.Url = url;
            this.Method = method;
            this.Body = body;
        }

        public string GetClientToken()
        {
            return this.ClientToken;
        }

        public void SetClientToken(string value)
        {
            this.ClientToken = value;
        }

        public string GetIp()
        {
            return this.Ip;
        }

        public void SetIp(string value)
        {
            this.Ip = value;
        }

        public string GetRemoteIp()
        {
            return this.RemoteIp;
        }

        public void SetRemoteIp(string value)
        {
            this.RemoteIp = value;
        }

        public Dictionary<string, string> GetHeaders()
        {
            return this.Headers;
        }

        public void SetHeaders(Dictionary<string, string> value)
        {
            this.Headers = value;
        }

        public string GetUrl()
        {
            return this.Url;
        }

        public void SetUrl(string value)
        {
            this.Url = value;
        }

        public string GetMethod()
        {
            return this.Method;
        }

        public void SetMethod(string value)
        {
            this.Method = value;
        }

        public string GetBody()
        {
            return this.Body;
        }

        public void SetBody(string value)
        {
            this.Body = value;
        }
    }
}
