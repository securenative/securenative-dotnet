using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class SecureNativeTests
    {
        [TestMethod]
        [ExpectedException(typeof(SecureNativeSdkIllegalStateException), "Get SDK instance without Initialization")]
        public void GetSDKInstanceWithoutInitThrowsTest()
        {
            Client.Flush();
            Client.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSDKWithEmptyApiKeyShouldThrowTest()
        {
            Client.Flush();
            Client.Init("");
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndDefaultsTest()
        {
            Client.Flush();
            string apiKey = "API_KEY";
            Client client = Client.Init(apiKey);
            SecureNativeOptions options = client.GetOptions();

            Assert.AreEqual(apiKey, options.GetApiKey());
            Assert.AreEqual("https://api.securenative.com/collector/api/v1", options.GetApiUrl());
            Assert.AreEqual(1000, options.GetInterval());
            Assert.AreEqual(1500, options.GetTimeout());
            Assert.AreEqual(1000, options.GetMaxEvents());
            Assert.AreEqual(true, options.IsAutoSend());
            Assert.AreEqual(false, options.IsDisabled());
            Assert.AreEqual("fatal", options.GetLogLevel());
            Assert.AreEqual(FailOverStrategy.FAIL_OPEN, options.GetFailoverStrategy());
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeSdkException), "Initialize SDK twice")]
        public void InitSDKTwiceWillThrowTest()
        {
            Client.Flush();
            Client.Init();
            Client.Init();
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndGetInstanceShouldMatchTest()
        {
            Client.Flush();
            string apiKey = "API_KEY";
            Client client = Client.Init(apiKey);

            Assert.AreEqual(client, Client.GetInstance());
        }

        [TestMethod]
        public void InitSDKWithBuilderTest()
        {
            Client.Flush();
            Client client = Client.Init(Client.ConfigBuilder()
                    .WithApiKey("API_KEY")
                    .WithMaxEvents(10)
                    .WithLogLevel("error")
                    .Build());

            SecureNativeOptions options = client.GetOptions();

            Assert.AreEqual("API_KEY", options.GetApiKey());
            Assert.AreEqual(10, options.GetMaxEvents());
            Assert.AreEqual("error", options.GetLogLevel());
        }
    }
}
