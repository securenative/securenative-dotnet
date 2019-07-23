using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SecureNative.SDK
{
    public class VerifyWebhook
    {
        private static string[] ipHeaders = { "X-Forwarded-For", "x-client-ip", "x-real-ip", "x-forwarded", "x-cluster-client-ip", "forwarded-for", "forwarded", "via" };

        public static bool IsRequestFromSecureNative(System.Web.HttpContext context, string apiKey)
        {
            if (context == null)
            {
                return false;
            }
            var header = HttpContext.Current.Response.Headers.Get("x-securenative");
            string body;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            if (IsVerifiedSnRequest(body, header, apiKey))// stream to string
            {
                return true;
            }
            return false;

        }

        private static bool IsVerifiedSnRequest(string body, string headerSignature, string apiKey)
        {
            var signed = CalculateSignature(body, apiKey);
            if (string.IsNullOrEmpty(signed) || string.IsNullOrEmpty(headerSignature))
            {
                return false;
            }
            return string.Compare(headerSignature, signed) == 0;

        }

        private static string CalculateSignature(string body, string apiKey)
        {
            using (HMACSHA512 hmac = new HMACSHA512(Encoding.ASCII.GetBytes(apiKey)))
            {
                return hmac.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(body))).ToString();
            }
        }

        public static string GetIPAddress(System.Web.HttpContext context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static IEvent BuildEventFromContxt(System.Web.HttpContext context, IEvent ievet)
        {
            var encodedCookie = HttpContext.Current.Response.Headers.Get("_sn");

            IEvent e = ievet == null ? new SnEvent() : ievet;

            ievet.RemoteIp = String.IsNullOrEmpty(ievet.RemoteIp) ? RemoteIpFromContext(context): ievet.RemoteIp;
            ievet.User = ievet.User != null ? ievet.User : new User();
            ievet.UserAgent = ievet.UserAgent != null ? ievet.UserAgent : GetHeader(context, "User-Agent");  
           
            return ievet;
        }

        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrEmpty(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public static bool isValidIP(string ipString)
        {
            IPAddress address;
           

            if (IPAddress.TryParse(ipString, out address))
            {
                switch (address.AddressFamily)
                {
                    // we have IPv4
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                    // we have IPv6
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        return true;
                    default:
                       
                    break;
                }
            }

            return false;
        }

        public static bool isPrivateIPAddress(string ipAddress)
        {
            int[] ipParts = ipAddress.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.Parse(s)).ToArray();
            // in private ip range
            if (ipParts[0] == 10 ||
                (ipParts[0] == 192 && ipParts[1] == 168) ||
                (ipParts[0] == 172 && (ipParts[1] >= 16 && ipParts[1] <= 31)))
            {
                return true;
            }

            // IP Address is probably public.
            // This doesn’t catch some VPN ranges like OpenVPN and Hamachi.
            return false;
        }

        private static string GetHeader(System.Web.HttpContext context, string key)
        {
            return context.Request.Headers.Get(key);
        }          

        public static string RemoteIpFromContext(System.Web.HttpContext context)
        {
            var ipHeaders = VerifyWebhook.ipHeaders;
            for (var i = 0; i < ipHeaders.Length; i++)
            {
                var headerValue = GetHeader(context, ipHeaders[i]);

                if (!String.IsNullOrEmpty(headerValue))
                {
                    var validated = headerValue.Split(',').Where(h =>
                        (isValidIP(h.Trim())) &&
                        !isPrivateIPAddress(h.Trim())
                    ).ToArray();

                    if (validated.Length > 0)
                    {
                        return validated[0];
                    }
                }  
            }
            return "127.0.0.1";
        }
    }
}



/**
 * public String remoteIpFromRequest(Function<String, String> headerExtractor) {
       Optional<String> bestCandidate = Optional.empty();
       String header = “”;
       for (int i = 0; i < ipHeaders.length; i++) {
           List<String> candidates = Arrays.asList();
           header = headerExtractor.apply(ipHeaders[i]);
           if (!this.isNullOrEmpty(header)) {
               candidates = Arrays.stream(header.split(“,”)).map(s -> s.trim()).filter(s -> !this.isNullOrEmpty(s) &&
                       (isValidInet4Address(s) || this.isIpV6Address(s)) &&
                       !isPrivateIPAddress(s)).collect(Collectors.toList());
               if (candidates.size() > 0) {
                   return candidates.get(0);
               }
           }
           if (!bestCandidate.isPresent()) {
               bestCandidate = candidates.stream().filter(x -> isLoopBack(x)).findFirst();
           }
       }
       return “127.0.0.1”;
   }







    public Event buildEventFromHttpServletRequest(HttpServletRequest request, Event event) {
       String encodedCookie = getCookie(request, event != null && !this.utils.isNullOrEmpty(event.getCookieName()) ? event.getCookieName() : this.utils.COOKIE_NAME);
       encodedCookie = utils.isNullOrEmpty(encodedCookie) && !utils.isNullOrEmpty(event.getCookieValue()) ? event.getCookieValue() : encodedCookie;
       String eventype =  event == null || this.utils.isNullOrEmpty(event.getEventType()) ? EventTypes.LOG_IN.getType() : event.getEventType();
       String ip = event != null && event.getIp() != null ? event.getIp() : remoteIpFromServletRequest(request);
       String remoteIP = request.getRemoteAddr();
       String userAgent = event != null && event.getUserAgent() != null ? event.getUserAgent() : request.getHeader(this.utils.USERAGENT_HEADER);
       User user = event != null && event.getUser() != null ? event.getUser() : new User(null, null, “anonymous”);
       Device device = event != null && event.getDevice() != null ? event.getDevice() : null;
       return new SnEvent.EventBuilder(eventype).withCookieValue(this.utils.isNullOrEmpty(encodedCookie) ? request.getHeader(utils.SN_HEADER) : encodedCookie).withIp(ip).withRemoteIP(remoteIP).withUserAgent(userAgent).withUser(user).withDevice(device).build();
   }
 


  }


}
    */