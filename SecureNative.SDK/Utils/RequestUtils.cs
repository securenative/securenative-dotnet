﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using SecureNative.SDK.Config;

namespace SecureNative.SDK.Utils
{
    public static class RequestUtils
    {
        public const string SecurenativeCookie = "_sn";
        private const string SecurenativeHeader = "x-securenative";

        private static readonly List<string> IpHeaders = new List<string>
        {
            "x-forwarded-for", "x-client-ip", "x-real-ip", "x-forwarded", "x-cluster-client-ip", "forwarded-for",
            "forwarded", "via"
        };

        public static Dictionary<string, string> GetHeadersFromRequest(HttpWebRequest request)
        {
            var headers = new Dictionary<string, string>();
            try
            {
                foreach (var key in request.Headers.AllKeys)
                {
                    headers.Add(key, request.Headers[key]);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return headers;
        }

        public static string GetCookieValueFromRequest(HttpWebRequest request, string cookieName)
        {
            var cookie = "";
            try
            {
                cookie = request.Headers[cookieName];

                if (cookie != null && cookie.Equals(""))
                {
                    var cookies = request.Headers["Cookie"];
                    cookie = ParseCookie(cookies, cookieName);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return cookie;
        }

        private static string ParseCookie(string cookieString, string cookieName)
        {
            var cookies = cookieString.Split(";");
            foreach (var cookie in cookies)
            {
                if (cookie.Contains(cookieName))
                {
                    return cookie.Replace("_sn=", "");
                }
            }

            return "";
        }

        public static string GetClientIpFromRequest(HttpWebRequest request, SecureNativeOptions options)
        {
            if (options?.GetProxyHeaders() != null)
            {
                foreach (var header in options.GetProxyHeaders())
                {
                    if (request.Headers.Get(header) != null)
                    {
                        return request.Headers.Get(header);
                    }
                }
            }
            
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers.Get(header) != null))
                {
                    return request.Headers.Get(header);
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
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers.Get(header) != null))
                {
                    return request.Headers.Get(header);
                }
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }

        public static Dictionary<string, string> GetHeadersFromRequest(HttpRequest request)
        {
            var headers = new Dictionary<string, string>();
            try
            {
                foreach (var key in request.Headers.Keys)
                {
                    headers.Add(key, request.Headers[key]);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return headers;
        }

        public static string GetSecureHeaderFromRequest(Dictionary<string, string> headers)
        {
            var header = "";
            try
            {
                header = headers.GetValueOrDefault(SecurenativeHeader, "");
            }
            catch (Exception)
            {
                // ignored
            }

            return header;
        }

        public static string GetCookieValueFromRequest(HttpRequest request, string cookieName)
        {
            var cookie = "";
            try
            {
                cookie = request.Headers[cookieName][0];
            }
            catch (Exception)
            {
                // ignored
            }

            return cookie;
        }

        public static string GetClientIpFromRequest(HttpRequest request)
        {
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers[header].Count > 0))
                {
                    return request.Headers[header][0];
                }

                if (request.HttpContext.Connection.LocalIpAddress != null)
                {
                    return request.HttpContext.Connection.LocalIpAddress.ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }

        public static string GetRemoteIpFromRequest(HttpRequest request)
        {
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers[header].Count > 0))
                {
                    return request.Headers[header][0];
                }
                
                if (request.HttpContext.Connection.RemoteIpAddress != null)
                {
                    return request.HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }
    }
}