using System.Security.Cryptography;
using System.Text;

namespace SecureNative.SDK.Utils
{
    public static class SignatureUtils
    {
        public const string SignatureHeader = "x-securenative";

        public static bool IsValidSignature(string headerSignature, string payload, string apiKey)
        {
            var signed = BuildHmacSignature(payload, apiKey);
            if (string.IsNullOrEmpty(signed) || string.IsNullOrEmpty(headerSignature))
            {
                return false;
            }
            return headerSignature.Equals(signed);
        }

        private static string BuildHmacSignature(string payload, string key)
        {
            var hash = new StringBuilder(); ;
            var secretKeyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(payload);
            using var hmac = new HMACSHA512(secretKeyBytes);
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
