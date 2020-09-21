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
        [ExpectedException(typeof(SecureNativeSDKIllegalStateException), "Get SDK instance without Initialization")]
        public void GetSDKInstanceWithoutInitThrowsTest()
        {
            SecureNative.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeSDKException), "Initialize SDK without api key")]
        public void InitSDKWithoutApiKeyShouldThrowTest()
        {
            SecureNative _ = SecureNative.Init();

        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSDKWithEmptyApiKeyShouldThrowTest()
        {
            SecureNative.Init("");
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndDefaultsTest()
        {
            string apiKey = "API_KEY";
            SecureNative secureNative = SecureNative.Init(apiKey);
            SecureNativeOptions options = secureNative.GetOptions();

            Assert.Equals(options.GetApiKey(), apiKey);
            Assert.Equals(options.GetApiUrl(), "https://api.securenative.com/collector/api/v1");
            Assert.Equals(options.GetInterval(), 1000);
            Assert.Equals(options.GetTimeout(), 1500);
            Assert.Equals(options.GetMaxEvents(), 1000);
            Assert.Equals(options.IsAutoSend(), true);
            Assert.Equals(options.IsDisabled(), false);
            Assert.Equals(options.GetLogLevel(), "fatal");
            Assert.Equals(options.GetFailoverStrategy(), FailOverStrategy.FAIL_OPEN);
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeSDKException), "Initialize SDK twice")]
        public void InitSDKTwiceWillThrowTest()
        {
            SecureNative.Init();
            SecureNative.Init();
        }

        [TestMethod]
        public void InitSDKWithApiKeyAndGetInstanceShouldMatchTest()
        {
            string apiKey = "API_KEY";
            SecureNative secureNative = SecureNative.Init(apiKey);

            Assert.Equals(secureNative, SecureNative.GetInstance());
        }

        [TestMethod]
        public void InitSDKWithBuilderTest()
        {
            SecureNative secureNative = SecureNative.Init(SecureNative.ConfigBuilder()
                    .WithApiKey("API_KEY")
                    .WithMaxEvents(10)
                    .WithLogLevel("error")
                    .Build());

            SecureNativeOptions options = secureNative.GetOptions();

            Assert.Equals(options.GetApiKey(), "API_KEY");
            Assert.Equals(options.GetMaxEvents(), 10);
            Assert.Equals(options.GetLogLevel(), "error");
        }
    }
}
