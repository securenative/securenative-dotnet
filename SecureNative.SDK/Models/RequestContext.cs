using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecureNative.SDK.Models
{
    public class RequestContext
    {
        [JsonProperty("cid", NullValueHandling=NullValueHandling.Ignore)]
        private string Cid { get; set; }
        [JsonProperty("vid", NullValueHandling=NullValueHandling.Ignore)]
        private string Vid { get; set; }
        [JsonProperty("fp", NullValueHandling=NullValueHandling.Ignore)]
        private string Fp { get; set; }
        [JsonProperty("ip", NullValueHandling=NullValueHandling.Ignore)]
        private string Ip { get; set; }
        [JsonProperty("remoteIp", NullValueHandling=NullValueHandling.Ignore)]
        private string RemoteIp { get; set; }
        [JsonProperty("headers", NullValueHandling=NullValueHandling.Ignore)]
        private Dictionary<string, string> Headers { get; set; }
        [JsonProperty("url", NullValueHandling=NullValueHandling.Ignore)]
        private string Url { get; set; }
        [JsonProperty("method", NullValueHandling=NullValueHandling.Ignore)]
        private string Method { get; set; }

        public RequestContext(string cid, string vid, string fp, string ip, string remoteIp, Dictionary<string, string> headers, string url, string method)
        {
            Cid = cid;
            Vid = vid;
            Fp = fp;
            Ip = ip;
            RemoteIp = remoteIp;
            Headers = headers;
            Url = url;
            Method = method;
        }

        public string GetCid()
        {
            return Cid;
        }

        public void SetCid(string value)
        {
            Cid = value;
        }

        public string GetVid()
        {
            return Vid;
        }

        public void SetVid(string value)
        {
            Vid = value;
        }

        public string GetFp()
        {
            return Fp;
        }

        public void SetFp(string value)
        {
            Fp = value;
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
    }
}
