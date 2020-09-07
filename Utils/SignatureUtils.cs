using System;
namespace SecureNative.SDK.Utils
{
    public static class SignatureUtils
    {
        public readonly static string SIGNATURE_HEADER = "x-securenative";
        private static readonly string HMAC_SHA512 = "HmacSHA512";

        public static Boolean IsValidSignature(string headerSignature, string payload, string apiKey)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
