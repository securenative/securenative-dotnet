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
            SecureNative.Flush();
            SecureNative.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSDKWithEmptyApiKeyShouldThrowTest()
        {
            SecureNative.Flush();
            SecureNative.Init("");
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndDefaultsTest()
        {
            SecureNative.Flush();
            string apiKey = "API_KEY";
            SecureNative secureNative = SecureNative.Init(apiKey);
            SecureNativeOptions options = secureNative.GetOptions();

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
            SecureNative.Flush();
            SecureNative.Init();
            SecureNative.Init();
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndGetInstanceShouldMatchTest()
        {
            SecureNative.Flush();
            string apiKey = "API_KEY";
            SecureNative secureNative = SecureNative.Init(apiKey);

            Assert.AreEqual(secureNative, SecureNative.GetInstance());
        }

        [TestMethod]
        public void InitSDKWithBuilderTest()
        {
            SecureNative.Flush();
            SecureNative secureNative = SecureNative.Init(SecureNative.ConfigBuilder()
                    .WithApiKey("API_KEY")
                    .WithMaxEvents(10)
                    .WithLogLevel("error")
                    .Build());

            SecureNativeOptions options = secureNative.GetOptions();

            Assert.AreEqual("API_KEY", options.GetApiKey());
            Assert.AreEqual(10, options.GetMaxEvents());
            Assert.AreEqual("error", options.GetLogLevel());
        }
    }
}
