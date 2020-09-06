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
    }
}
