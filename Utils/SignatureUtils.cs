using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureNative.SDK.Utils
{
    public static class SignatureUtils
    {
        public readonly static string SIGNATURE_HEADER = "x-securenative";

        public static Boolean IsValidSignature(string headerSignature, string payload, string apiKey)
        {
            string signed = BuildHmacSignature(payload, apiKey);
            if (Utils.IsNullOrEmpty(signed) || Utils.IsNullOrEmpty(headerSignature))
            {
                return false;
            }
            return headerSignature.Equals(signed);
        }

        private static string BuildHmacSignature(string payload, string key)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(payload);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }
    }
}
