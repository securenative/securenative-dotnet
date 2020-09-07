using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContextBuilder
    {
        private readonly SecureNativeContext Context;

        public SecureNativeContextBuilder()
        {
            this.Context = new SecureNativeContext();
        }

        public SecureNativeContextBuilder WithIp(string ip)
        {
            this.Context.SetIp(ip);
            return this;
        }

        public SecureNativeContextBuilder WithRemoteIp(string remoteIp)
        {
            this.Context.SetRemoteIp(remoteIp);
            return this;
        }

        public SecureNativeContextBuilder WithHeaders(Dictionary<string, string> headers)
        {
            this.Context.SetHeaders(headers);
            return this;
        }

        public SecureNativeContextBuilder WithUrl(string url)
        {
            this.Context.SetUrl(url);
            return this;
        }

        public SecureNativeContextBuilder WithMethod(string method)
        {
            this.Context.SetMethod(method);
            return this;
        }

        public SecureNativeContextBuilder WithBody(string body)
        {
            this.Context.SetBody(body);
            return this;
        }

        public static SecureNativeContextBuilder FromHttpServletRequest(HttpRequestMessage request)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static SecureNativeContextBuilder DefaultContextBuilder()
        {
            return new SecureNativeContextBuilder();
        }

        public SecureNativeContext Build()
        {
            return this.Context;
        }
    }
}
