using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SecureNative.SDK.Utils
{
    public static class RequestUtils
    {
        public readonly static String SECURENATIVE_COOKIE = "_sn";
        public readonly static String SECURENATIVE_HEADER = "x-securenative";
        private readonly static List<String> IpHeaders = new List<string> { "x-forwarded-for", "x-client-ip", "x-real-ip", "x-forwarded", "x-cluster-client-ip", "forwarded-for", "forwarded", "via" };

        public static Dictionary<string, string> GetHeadersFromRequest(HttpRequestMessage request)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static string GetSecureHeaderFromRequest(Dictionary<string, string> headers)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static string GetCookieValueFromRequest(HttpRequestMessage request, string name)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static string GetClientIpFromRequest(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static string GetRemoteIpFromRequest(HttpRequestMessage request)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
