using System;
using System.Collections.Generic;
using System.Net;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContextBuilder
    {
        private readonly SecureNativeContext _context;

        public SecureNativeContextBuilder()
        {
            _context = new SecureNativeContext();
        }

        public SecureNativeContextBuilder WithIp(string ip)
        {
            _context.SetIp(ip);
            return this;
        }

        public SecureNativeContextBuilder WithRemoteIp(string remoteIp)
        {
            _context.SetRemoteIp(remoteIp);
            return this;
        }

        public SecureNativeContextBuilder WithHeaders(Dictionary<string, string> headers)
        {
            _context.SetHeaders(headers);
            return this;
        }

        public SecureNativeContextBuilder WithUrl(string url)
        {
            _context.SetUrl(url);
            return this;
        }

        public SecureNativeContextBuilder WithMethod(string method)
        {
            _context.SetMethod(method);
            return this;
        }

        public SecureNativeContextBuilder WithBody(string body)
        {
            _context.SetBody(body);
            return this;
        }

        public SecureNativeContextBuilder WithClientToken(string clientToken)
        {
            _context.SetClientToken(clientToken);
            return this;
        }

        public static SecureNativeContextBuilder FromHttpRequest(HttpWebRequest request)
        {
            var headers = RequestUtils.GetHeadersFromRequest(request);

            var clientToken = RequestUtils.GetCookieValueFromRequest(request, RequestUtils.SecurenativeCookie);
            if (string.IsNullOrEmpty(clientToken))
            {
                clientToken = RequestUtils.GetSecureHeaderFromRequest(headers);
            }

            return new SecureNativeContextBuilder()
                    .WithUrl(request.RequestUri.ToString())
                    .WithMethod(request.Method)
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
            return _context;
        }
    }
}
