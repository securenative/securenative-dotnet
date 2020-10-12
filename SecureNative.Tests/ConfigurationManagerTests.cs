using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;

namespace SecureNative.Tests
{
    [TestClass]
    public class ConfigurationManagerTests
    {
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

        [TestMethod]
        public void IgnoreUnknownConfigInPropertiesFileTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
            Assert.AreEqual(1500, options.GetTimeout());
        }

        [TestMethod]
        public void LoadDefaultConfigTest()
        {

            SecureNativeOptions options = ConfigurationManager.LoadConfig("some/path");

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
            Environment.SetEnvironmentVariable("SECURENATIVE_AUTO_SEND", "false");
            Environment.SetEnvironmentVariable("SECURENATIVE_DISABLE", "true");
            Environment.SetEnvironmentVariable("SECURENATIVE_LOG_LEVEL", "debug");
            Environment.SetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY", "fail-closed");

            SecureNativeOptions options = ConfigurationManager.LoadConfig("some/path");

            Assert.AreEqual("SOME_ENV_API_KEY", options.GetApiKey());
            Assert.AreEqual("SOME_API_URL", options.GetApiUrl());
            Assert.AreEqual(6000, options.GetInterval());
            Assert.AreEqual(1700, options.GetTimeout());
            Assert.AreEqual(700, options.GetMaxEvents());
            Assert.AreEqual(false, options.IsAutoSend());
            Assert.AreEqual(true, options.IsDisabled());
            Assert.AreEqual("debug", options.GetLogLevel());
            Assert.AreEqual(FailOverStrategy.FAIL_CLOSED, options.GetFailOverStrategy());

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
