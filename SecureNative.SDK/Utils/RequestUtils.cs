using System;
using System.Collections.Generic;
using System.Net;

namespace SecureNative.SDK.Utils
{
    public static class RequestUtils
    {
        public readonly static string SECURENATIVE_COOKIE = "_sn";
        public readonly static string SECURENATIVE_HEADER = "x-securenative";
        private readonly static List<String> IpHeaders = new List<string> { "x-forwarded-for", "x-client-ip", "x-real-ip", "x-forwarded", "x-cluster-client-ip", "forwarded-for", "forwarded", "via" };

        public static Dictionary<string, string> GetHeadersFromRequest(HttpWebRequest request)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            foreach (var key in request.Headers.AllKeys)
            {
                headers.Add(key, request.Headers[key]);
            }

            return headers;
        }

        public static string GetSecureHeaderFromRequest(Dictionary<string, string> headers)
        {
            return headers.GetValueOrDefault(SECURENATIVE_HEADER, "");
        }

        public static string GetCookieValueFromRequest(HttpWebRequest request, string cookieName)
        {
            return request.Headers.GetValues(cookieName).ToString();
        }

        public static string GetClientIpFromRequest(HttpWebRequest request)
        {
            try
            {
                foreach (var header in IpHeaders)
                {
                    if (request.Headers.Get(header) != null)
                    {
                        return request.Headers.Get(header);
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
            return "";
        }
        public static string GetRemoteIpFromRequest(HttpWebRequest request)
        {
            return request.Host;
        }
    }
}
