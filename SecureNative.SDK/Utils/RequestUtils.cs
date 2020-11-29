using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
        private static readonly List<string> PiiHeaders = new List<string>
        {
            "authorization", "access_token", "apikey", "password", "passwd", "secret", "api_key"
        };

        public static Dictionary<string, string> GetHeadersFromRequest(HttpWebRequest request, SecureNativeOptions options)
        {
            var headers = new Dictionary<string, string>();
            if (options?.GetPiiHeaders().Length > 0)
            {
                try
                {
                    foreach (var key in request.Headers.AllKeys)
                    {
                        if (!options.GetPiiHeaders().Contains(key))
                        {
                            headers.Add(key, request.Headers[key]);   
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            } else if (options != null && options.GetPiiRegexPattern() != "")
            {
                try
                {
                    var pattern = new Regex(options.GetPiiRegexPattern());

                    foreach (var key in request.Headers.AllKeys)
                    {
                        if (!pattern.Match(key).Success)
                        {
                            headers.Add(key, request.Headers[key]);   
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                try
                {
                    foreach (var key in request.Headers.AllKeys)
                    {
                        if (!PiiHeaders.Contains(key))
                        {
                            headers.Add(key, request.Headers[key]);   
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }   
            }

            return headers;
        }

        public static string GetCookieValueFromRequest(HttpWebRequest request, string cookieName)
        {
            var cookie = "";
            try
            {
                try
                {
                    cookie = request.Headers[cookieName];
                }
                catch (Exception)
                {
                    // ignored
                }

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
                if (cookie.Contains(cookieName) && !cookie.Contains("_sncid="))
                {
                    return cookie.Replace(cookieName + "=", "").Trim();
                }
            }

            return string.Empty;
        }

        public static string GetClientIpFromRequest(HttpWebRequest request, SecureNativeOptions options)
        {
            if (options?.GetProxyHeaders().Length > 0)
            {
                foreach (var header in options.GetProxyHeaders())
                {
                    if (string.IsNullOrEmpty(request.Headers.Get(header)))
                    {
                        continue;
                    }
                    var ips = request.Headers.Get(header).Split(",");
                    var extracted = GetValidIp(ips);
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                }
            }
            
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers.Get(header) != null))
                {
                    var ips = request.Headers.Get(header).Split(",");
                    var extracted = GetValidIp(ips);
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public static string GetRemoteIpFromRequest(HttpWebRequest request)
        {
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers.Get(header) != null))
                {
                    var ips = request.Headers.Get(header).Split(",");
                    var extracted = GetValidIp(ips);
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                    
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public static Dictionary<string, string> GetHeadersFromRequest(HttpRequest request, SecureNativeOptions options)
        {
            var headers = new Dictionary<string, string>();
            if (options?.GetPiiHeaders().Length > 0)
            {
                try
                {
                    foreach (var key in request.Headers.Keys)
                    {
                        if (!options.GetPiiHeaders().Contains(key))
                        {
                            headers.Add(key, request.Headers[key]);
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            } else if (options != null && options.GetPiiRegexPattern() != "")
            {
                try
                {
                    var pattern = new Regex(options.GetPiiRegexPattern());

                    foreach (var key in request.Headers.Keys)
                    {
                        if (!pattern.Match(key).Success)
                        {
                            headers.Add(key, request.Headers[key]); 
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                try
                {
                    foreach (var key in request.Headers.Keys)
                    {
                        if (!PiiHeaders.Contains(key))
                        {
                            headers.Add(key, request.Headers[key]);
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }   
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
                try
                {
                    cookie = request.Headers[cookieName][0];
                }
                catch (Exception)
                {
                    // ignored
                }

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

        public static string GetClientIpFromRequest(HttpRequest request, SecureNativeOptions options)
        {
            if (options?.GetProxyHeaders().Length > 0)
            {
                foreach (var header in options.GetProxyHeaders())
                {
                    if (request.Headers[header].ToArray() == null) continue;
                    var extracted = GetValidIp(request.Headers[header].ToArray());
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                }
            }
            
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers[header].Count > 0))
                {
                    var extracted = GetValidIp(request.Headers[header].ToArray());
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                }

                if (request.HttpContext.Connection.LocalIpAddress != null)
                {
                    return request.HttpContext.Connection.LocalIpAddress.ToString();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public static string GetRemoteIpFromRequest(HttpRequest request)
        {
            try
            {
                foreach (var header in IpHeaders.Where(header => request.Headers[header].Count > 0))
                {
                    var extracted = GetValidIp(request.Headers[header].ToArray());
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        return extracted;
                    }
                }
                
                if (request.HttpContext.Connection.RemoteIpAddress != null)
                {
                    return request.HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }
        
        private static string GetValidIp(IEnumerable<string> ipAddresses) {
            foreach (var extracted in ipAddresses)
            {
                var ips = extracted.Split(",");
                foreach (var ip in ips)
                {
                    if (IpUtils.IsValidPublicIp(ip.Trim()))
                    {
                        return ip.Trim();
                    }
                }
                            
                foreach (var ip in ips)
                {
                    if (!IpUtils.IsLoopBack(ip.Trim()))
                    {
                        return ip.Trim();
                    }
                }
            }

            return string.Empty;
        }
    }
}