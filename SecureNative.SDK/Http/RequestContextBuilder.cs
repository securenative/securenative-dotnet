using System;
using System.Collections.Generic;
using SecureNative.SDK.Models;

namespace SecureNative.SDK.Http
{
    public class RequestContextBuilder
    {
        private string Cid;
        private string Vid;
        private string Fp;
        private string Ip;
        private string RemoteIp;
        private Dictionary<string, string> Headers;
        private string Url;
        private string Method;

        public RequestContextBuilder WithCid(string cid)
        {
            this.Cid = cid;
            return this;
        }

        public RequestContextBuilder WithVid(string vid)
        {
            this.Vid = vid;
            return this;
        }

        public RequestContextBuilder WithFp(string fp)
        {
            this.Fp = fp;
            return this;
        }

        public RequestContextBuilder WithIp(string ip)
        {
            this.Ip = ip;
            return this;
        }

        public RequestContextBuilder WithRemoteIp(string remoteIp)
        {
            this.RemoteIp = remoteIp;
            return this;
        }

        public RequestContextBuilder WitHeaders(Dictionary<string, string> headers)
        {
            this.Headers = headers;
            return this;
        }

        public RequestContextBuilder WithUrl(string url)
        {
            this.Url = url;
            return this;
        }

        public RequestContextBuilder WithMethod(string method)
        {
            this.Method = method;
            return this;
        }

        public RequestContext Build()
        {
            return new RequestContext(Cid, Vid, Fp, Ip, RemoteIp, Headers, Url, Method);
        }
    }
}



