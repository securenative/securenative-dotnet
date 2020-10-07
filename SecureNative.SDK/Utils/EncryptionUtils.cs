using SecureNative.SDK.Models;

namespace SecureNative.SDK.Utils
{
    public static class EncryptionUtils
    {
        private static readonly int BlockSize = 16;

        public static ClientToken Decrypt(string secret, string key)
        {
            // TODO: implement me
            return new ClientToken("", "", "");
        }

        public static string Encrypt(string text, string key)
        {
            // TODO: implement me
            return "";
        }
    }
}
