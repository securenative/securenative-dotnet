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
            SDK.Client.Flush();
            SDK.Client.GetInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeConfigException), "Initialize SDK with empty api key")]
        public void InitSdkWithEmptyApiKeyShouldThrowTest()
        {
            SDK.Client.Flush();
            SDK.Client.Init("");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndDefaultsTest()
        {
            SDK.Client.Flush();
            const string apiKey = "API_KEY";
            var secureNative = SDK.Client.Init(apiKey);
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
            SDK.Client.Flush();
            SDK.Client.Init("SecureNative.Tests/securenative.json");
            SDK.Client.Init("SecureNative.Tests/securenative.json");
        }

        [TestMethod]
        public void InitSdkWithApiKeyAndGetInstanceShouldMatchTest()
        {
            SDK.Client.Flush();
            const string apiKey = "API_KEY";
            SDK.Client secureNative = SDK.Client.Init(apiKey);

            Assert.AreEqual(secureNative, SDK.Client.GetInstance());
        }

        [TestMethod]
        public void InitSdkWithBuilderTest()
        {
            SDK.Client.Flush();
            var secureNative = SDK.Client.Init(SDK.Client.ConfigBuilder()
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
