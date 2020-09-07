using System;
using System.Collections.Generic;

namespace SecureNative.SDK.Models
{
    public class RequestContext
    {
        private string Cid { get; set; }
        private string Vid { get; set; }
        private string Fp { get; set; }
        private string Ip { get; set; }
        private string RemoteIp { get; set; }
        private Dictionary<string, string> Headers { get; set; }
        private string Url { get; set; }
        private string Method { get; set; }

        public RequestContext()
        {
        }

        public RequestContext(string cid, string vid, string fp, string ip, string remoteIp, Dictionary<string, string> headers, string url, string method)
        {
            this.Cid = cid;
            this.Vid = vid;
            this.Fp = fp;
            this.Ip = ip;
            this.RemoteIp = remoteIp;
            this.Headers = headers;
            this.Url = url;
            this.Method = method;
        }

        public string GetCid()
        {
            return this.Cid;
        }

        public void SetCid(string value)
        {
            this.Cid = value;
        }

        public string GetVid()
        {
            return this.Vid;
        }

        public void SetVid(string value)
        {
            this.Vid = value;
        }

        public string GetFp()
        {
            return this.Fp;
        }

        public void SetFp(string value)
        {
            this.Fp = value;
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
    }
}
