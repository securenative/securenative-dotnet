using System;
using System.Collections.Generic;

namespace SecureNative.SDK.Models
{
    public class RequestContext
    {
        public string Cid { get; set; }
        public string Vid { get; set; }
        public string Fp { get; set; }
        public string Ip { get; set; }
        public string RemoteIp { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }

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
    }
}
