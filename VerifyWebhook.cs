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
using System.Security.Cryptography;
namespace SecureNative.SDK
{
    public class VerifyWebhook
    {
        private static int AES_KEY_SIZE = 32;
        private static int AES_BLOCK_SIZE = 16;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="headerSignature"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        private static bool IsVerifiedSnRequest(string body, string headerSignature, string apiKey)
        {
            var signed = CalculateSignature(body, apiKey);
            //var signed = Encrypt(body, apiKey);
            //if (string.IsNullOrEmpty(signed) || string.IsNullOrEmpty(headerSignature))
            //{
            //    return false;
            //}
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

            e.RemoteIp = String.IsNullOrEmpty(e.RemoteIp) ? RemoteIpFromContext(context): e.RemoteIp;
            e.User = e.User != null ? e.User : new User();
            e.UserAgent = e.UserAgent != null ? e.UserAgent : GetHeader(context, "User-Agent");  
           
            return e;
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



        public static string Decrypt(string unEncrepted, string cipherKey)
        {
            var key = Encoding.ASCII.GetBytes(cipherKey).Take(32).ToArray();
            var ba = Encoding.Default.GetBytes(unEncrepted);
            var cipherText = HexadecimalStringToByteArray(unEncrepted);
         
            if (cipherText.Length < AES_BLOCK_SIZE)
            {
                throw new Exception("plain text has the wrong block size");
            }
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                var iv = cipherText.Take(AES_BLOCK_SIZE).ToArray();
                cipherText = cipherText.Skip(AES_BLOCK_SIZE).Take(cipherText.Length).ToArray();
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                try
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (var msDecrypt = new MemoryStream(cipherText))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {

                                    return srDecrypt.ReadToEnd();



                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            return string.Empty;
        }

        public static byte[] HexadecimalStringToByteArray(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }
    }
}
