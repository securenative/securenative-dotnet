using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;

namespace SecureNative.Tests
{
    [TestClass]
    public class ConfigurationManagerTests
    {

#if IS_LINUX
        [TestMethod]
        public void ParseConfigFileCorrectlyTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
            Assert.AreEqual("SOME_API_KEY", options.GetApiKey());
            Assert.AreEqual("SOME_API_URL", options.GetApiUrl());
            Assert.AreEqual(true, options.IsAutoSend());
            Assert.AreEqual(false, options.IsDisabled());
            Assert.AreEqual(FailOverStrategy.FAIL_CLOSED, options.GetFailOverStrategy());
            Assert.AreEqual(1000, options.GetInterval());
            Assert.AreEqual("fatal", options.GetLogLevel());
            Assert.AreEqual(100, options.GetMaxEvents());
            Assert.AreEqual(1500, options.GetTimeout());
        }
#endif

        [TestMethod]
        public void IgnoreUnknownConfigInPropertiesFileTest()
        {
            var options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
            Assert.AreEqual(1500, options.GetTimeout());
        }

        [TestMethod]
        public void LoadDefaultConfigTest()
        {
            var options = ConfigurationManager.LoadConfig("some/path");

            Assert.AreEqual("", options.GetApiKey());
            Assert.AreEqual("https://api.securenative.com/collector/api/v1", options.GetApiUrl());
            Assert.AreEqual(1000, options.GetInterval());
            Assert.AreEqual(1500, options.GetTimeout());
            Assert.AreEqual(1500, options.GetTimeout());
            Assert.AreEqual(1000, options.GetMaxEvents());
            Assert.AreEqual(true, options.IsAutoSend());
            Assert.AreEqual(false, options.IsDisabled());
            Assert.AreEqual("fatal", options.GetLogLevel());
            Assert.AreEqual(FailOverStrategy.FAIL_OPEN, options.GetFailOverStrategy());
        }

        [TestMethod]
        public void GetConfigFromEnvVariablesTest()
        {
            Environment.SetEnvironmentVariable("SECURENATIVE_API_KEY", "SOME_ENV_API_KEY");
            Environment.SetEnvironmentVariable("SECURENATIVE_API_URL", "SOME_API_URL");
            Environment.SetEnvironmentVariable("SECURENATIVE_INTERVAL", "6000");
            Environment.SetEnvironmentVariable("SECURENATIVE_MAX_EVENTS", "700");
            Environment.SetEnvironmentVariable("SECURENATIVE_TIMEOUT", "1700");
            Environment.SetEnvironmentVariable("SECURENATIVE_AUTO_SEND", "False");
            Environment.SetEnvironmentVariable("SECURENATIVE_DISABLE", "True");
            Environment.SetEnvironmentVariable("SECURENATIVE_LOG_LEVEL", "debug");
            Environment.SetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY", "fail-closed");

            var options = ConfigurationManager.LoadConfig("some/path");

            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_API_KEY"), options.GetApiKey());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_API_URL"), options.GetApiUrl());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_INTERVAL"), options.GetInterval().ToString());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_TIMEOUT"), options.GetTimeout().ToString());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_MAX_EVENTS"), options.GetMaxEvents().ToString());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_AUTO_SEND"), options.IsAutoSend().ToString());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_DISABLE"), options.IsDisabled().ToString());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_LOG_LEVEL"), options.GetLogLevel());
            Assert.AreEqual(Environment.GetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY"), options.GetFailOverStrategy());

            Environment.SetEnvironmentVariable("SECURENATIVE_API_KEY", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_API_URL", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_INTERVAL", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_MAX_EVENTS", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_TIMEOUT", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_AUTO_SEND", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_DISABLE", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_LOG_LEVEL", "");
            Environment.SetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY", "");
        }
    }
}
