using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;

namespace SecureNative.Tests
{
    [TestClass]
    public class SecureNativeTests
    {
        [TestMethod]
        [ExpectedException(typeof(SecureNativeSdkIllegalStateException), "Get SDK instance without Initialization")]
        public void GetSdkInstanceWithoutInitThrowsTest()
        {
            Client.Flush();
            Client.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSdkWithEmptyApiKeyShouldThrowTest()
        {
            Client.Flush();
            Client.Init("");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndDefaultsTest()
        {
            Client.Flush();
            const string apiKey = "API_KEY";
            var secureNative = Client.Init(apiKey);
            var options = Client.GetOptions();

            Assert.AreEqual(apiKey, options.GetApiKey());
            Assert.AreEqual("https://api.securenative.com/collector/api/v1", options.GetApiUrl());
            Assert.AreEqual(1000, options.GetInterval());
            Assert.AreEqual(1500, options.GetTimeout());
            Assert.AreEqual(1000, options.GetMaxEvents());
            Assert.AreEqual(true, options.IsAutoSend());
            Assert.AreEqual(false, options.IsDisabled());
            Assert.AreEqual("fatal", options.GetLogLevel());
            Assert.AreEqual(FailOverStrategy.FAIL_OPEN, options.GetFailOverStrategy());
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeSdkException), "Initialize SDK twice")]
        public void InitSdkTwiceWillThrowTest()
        {
            Client.Flush();
            Client.Init("SecureNative.Tests/securenative.json");
            Client.Init("SecureNative.Tests/securenative.json");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndGetInstanceShouldMatchTest()
        {
            Client.Flush();
            const string apiKey = "API_KEY";
            Client secureNative = Client.Init(apiKey);

            Assert.AreEqual(secureNative, Client.GetInstance());
        }

        [TestMethod]
        public void InitSdkWithBuilderTest()
        {
            Client.Flush();
            var secureNative = Client.Init(Client.ConfigBuilder()
                    .WithApiKey("API_KEY")
                    .WithMaxEvents(10)
                    .WithLogLevel("error")
                    .Build());

            var options = Client.GetOptions();

            Assert.AreEqual("API_KEY", options.GetApiKey());
            Assert.AreEqual(10, options.GetMaxEvents());
            Assert.AreEqual("error", options.GetLogLevel());
        }
    }
}
