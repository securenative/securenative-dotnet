using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            SDK.SecureNative.Flush();
            SDK.SecureNative.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSdkWithEmptyApiKeyShouldThrowTest()
        {
            SDK.SecureNative.Flush();
            SDK.SecureNative.Init("");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndDefaultsTest()
        {
            SDK.SecureNative.Flush();
            const string apiKey = "API_KEY";
            var secureNative = SDK.SecureNative.Init(apiKey);
            var options = secureNative.GetOptions();

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
            SDK.SecureNative.Flush();
            SDK.SecureNative.Init("SecureNative.Tests/securenative.json");
            SDK.SecureNative.Init("SecureNative.Tests/securenative.json");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndGetInstanceShouldMatchTest()
        {
            SDK.SecureNative.Flush();
            const string apiKey = "API_KEY";
            SDK.SecureNative secureNative = SDK.SecureNative.Init(apiKey);

            Assert.AreEqual(secureNative, SDK.SecureNative.GetInstance());
        }

        [TestMethod]
        public void InitSdkWithBuilderTest()
        {
            SDK.SecureNative.Flush();
            var secureNative = SDK.SecureNative.Init(SDK.SecureNative.ConfigBuilder()
                    .WithApiKey("API_KEY")
                    .WithMaxEvents(10)
                    .WithLogLevel("error")
                    .Build());

            var options = secureNative.GetOptions();

            Assert.AreEqual("API_KEY", options.GetApiKey());
            Assert.AreEqual(10, options.GetMaxEvents());
            Assert.AreEqual("error", options.GetLogLevel());
        }
    }
}
