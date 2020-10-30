using System.Collections.Generic;
using SecureNative.SDK.Models;

namespace SecureNative.SDK.Http
{
    public class RequestContextBuilder
    {
        private string _cid;
        private string _vid;
        private string _fp;
        private string _ip;
        private string _remoteIp;
        private Dictionary<string, string> _headers;
        private string _url;
        private string _method;

        public RequestContextBuilder WithCid(string cid)
        {
            _cid = cid;
            return this;
        }

        public RequestContextBuilder WithVid(string vid)
        {
            _vid = vid;
            return this;
        }

        public RequestContextBuilder WithFp(string fp)
        {
            _fp = fp;
            return this;
        }

        public RequestContextBuilder WithIp(string ip)
        {
            _ip = ip;
            return this;
        }

        public RequestContextBuilder WithRemoteIp(string remoteIp)
        {
            _remoteIp = remoteIp;
            return this;
        }

        public RequestContextBuilder WitHeaders(Dictionary<string, string> headers)
        {
            _headers = headers;
            return this;
        }

        public RequestContextBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public RequestContextBuilder WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public RequestContext Build()
        {
            return new RequestContext(_cid, _vid, _fp, _ip, _remoteIp, _headers, _url, _method);
        }
    }
}



