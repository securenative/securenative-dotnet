using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SecureNative.SDK.Models;

namespace SecureNative.SDK.Utils
{
    public static class EncryptionUtils
    {
        private const int AesBlockSize = 16;
        public static ClientToken Decrypt(string data, string cipherKey)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(cipherKey.Substring(0, 32));
                var cipherText = HexadecimalStringToByteArray(data);
                if (cipherText.Length < AesBlockSize)
                {
                    return new ClientToken("", "", "");
                }

                using var aes = Aes.Create();
                var iv = cipherText.Take(AesBlockSize).ToArray();
                cipherText = cipherText.Skip(AesBlockSize).Take(cipherText.Length).ToArray();
                if (aes == null) return new ClientToken("", "", "");
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var msEncrypt = new MemoryStream();
                using var msDecrypt = new MemoryStream(cipherText);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                var r = srDecrypt.ReadToEnd();
                
                return JsonConvert.DeserializeObject<ClientToken>(r);
            }
            catch (Exception)
            {
                return new ClientToken("", "", "");
            }
        }
        private static byte[] HexadecimalStringToByteArray(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using var sr = new StringReader(input);
            for (var i = 0; i < outputLength; i++)
            {
                output[i] = Convert.ToByte(new string(new char[2] {(char) sr.Read(), (char) sr.Read()}), 16);
            }

            return output;
        }
    }
}