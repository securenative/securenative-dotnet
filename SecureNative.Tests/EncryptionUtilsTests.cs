using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.Tests
{
    [TestClass]
    public class EncryptionUtilsTests
    {
        private const string SecretKey = "B00C42DAD33EAC6F6572DA756EA4915349C0A4F6";
        private const string Payload = "{\"cid\":\"198a41ff-a10f-4cda-a2f3-a9ca80c0703b\",\"vi\":\"148a42ff-b40f-4cda-a2f3-a8ca80c0703b\",\"fp\":\"6d8cabd95987f8318b1fe01593d5c2a5.24700f9f1986800ab4fcc880530dd0ed\"}";
        private const string Cid = "198a41ff-a10f-4cda-a2f3-a9ca80c0703b";
        private const string Fp = "6d8cabd95987f8318b1fe01593d5c2a5.24700f9f1986800ab4fcc880530dd0ed";

        [TestMethod]
        public void EncryptTest()
        {
            var result = EncryptionUtils.Encrypt(Payload, SecretKey);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > Payload.Length);
        }

        [TestMethod]
        public void DecryptTest()
        {
            const string encryptedPayload = "5208ae703cc2fa0851347f55d3b76d3fd6035ee081d71a401e8bc92ebdc25d42440f62310bda60628537744ac03f200d78da9e61f1019ce02087b7ce6c976e7b2d8ad6aa978c532cea8f3e744cc6a5cafedc4ae6cd1b08a4ef75d6e37aa3c0c76954d16d57750be2980c2c91ac7ef0bbd0722abd59bf6be22493ea9b9759c3ff4d17f17ab670b0b6fc320e6de982313f1c4e74c0897f9f5a32d58e3e53050ae8fdbebba9009d0d1250fe34dcde1ebb42acbc22834a02f53889076140f0eb8db1";
            var result = EncryptionUtils.Decrypt(encryptedPayload, SecretKey);

            Assert.AreEqual(Cid, result.GetCid());
            Assert.AreEqual(Fp, result.GetFp());
        }
    }
}
