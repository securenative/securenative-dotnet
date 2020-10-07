using System.Collections.Generic;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContext
    {
        private string ClientToken { get; set; }
        private string Ip { get; set; }
        private string RemoteIp { get; set; }
        private Dictionary<string, string> Headers { get; set; }
        private string Url { get; set; }
        private string Method { get; set; }
        private string Body { get; set; }

        public SecureNativeContext()
        {
        }

        public SecureNativeContext(string clientToken, string ip, string remoteIp, Dictionary<string, string> headers, string url, string method, string body)
        {
            ClientToken = clientToken;
            Ip = ip;
            RemoteIp = remoteIp;
            Headers = headers;
            Url = url;
            Method = method;
            Body = body;
        }

        public string GetClientToken()
        {
            return ClientToken;
        }

        public void SetClientToken(string value)
        {
            ClientToken = value;
        }

        public string GetIp()
        {
            return Ip;
        }

        public void SetIp(string value)
        {
            Ip = value;
        }

        public string GetRemoteIp()
        {
            return RemoteIp;
        }

        public void SetRemoteIp(string value)
        {
            RemoteIp = value;
        }

        public Dictionary<string, string> GetHeaders()
        {
            return Headers;
        }

        public void SetHeaders(Dictionary<string, string> value)
        {
            Headers = value;
        }

        public string GetUrl()
        {
            return Url;
        }

        public void SetUrl(string value)
        {
            Url = value;
        }

        public string GetMethod()
        {
            return Method;
        }

        public void SetMethod(string value)
        {
            Method = value;
        }

        public string GetBody()
        {
            return Body;
        }

        public void SetBody(string value)
        {
            Body = value;
        }
    }
}
