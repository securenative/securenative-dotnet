using System;
using System.Collections.Generic;
using System.Net;
using SecureNative.SDK.Utils;

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

        public SecureNativeContextBuilder WithClientToken(string clientToken)
        {
            this.Context.SetClientToken(clientToken);
            return this;
        }

        public static SecureNativeContextBuilder FromHttpRequest(HttpWebRequest request)
        {
            Dictionary<string, string> headers = RequestUtils.GetHeadersFromRequest(request);

            String clientToken = RequestUtils.GetCookieValueFromRequest(request, RequestUtils.SECURENATIVE_COOKIE);
            if (Utils.Utils.IsNullOrEmpty(clientToken))
            {
                clientToken = RequestUtils.GetSecureHeaderFromRequest(headers);
            }

            return new SecureNativeContextBuilder()
                    .WithUrl(request.RequestUri.ToString())
                    .WithMethod(request.Method.ToString())
                    .WithHeaders(headers)
                    .WithClientToken(clientToken)
                    .WithIp(RequestUtils.GetClientIpFromRequest(request))
                    .WithRemoteIp(RequestUtils.GetRemoteIpFromRequest(request))
                    .WithBody(null);
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
