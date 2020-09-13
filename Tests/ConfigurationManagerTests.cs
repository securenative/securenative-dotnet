using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class ConfigurationManagerTests
    {
        [TestMethod]
        public void ParseConfigFileCorrectlyTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
            Assert.Equals(options.GetApiKey(), "SOME_API_KEY");
            Assert.Equals(options.GetApiUrl(), "SOME_API_URL");
            Assert.Equals(options.IsAutoSend(), true);
            Assert.Equals(options.IsDisabled(), false);
            Assert.Equals(options.GetFailoverStrategy(), FailOverStrategy.FAIL_CLOSED);
            Assert.Equals(options.GetInterval(), 1000);
            Assert.Equals(options.GetLogLevel(), "fatal");
            Assert.Equals(options.GetMaxEvents(), 100);
            Assert.Equals(options.GetTimeout(), 1500);
        }

        [TestMethod]
        public void IgnoreUnknownConfigInPropertiesFileTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
            Assert.Equals(options.GetTimeout(), 7500);
        }

        [TestMethod]
        public void HandleInvalidConfigFileTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNotNull(options);
        }

        [TestMethod]
        public void IgnoreInvalidConfigFileEntriesTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();
        }

        [TestMethod]
        public void LoadDefaultConfigTest()
        {

            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.IsNull(options.GetApiKey());
            Assert.Equals(options.GetApiUrl(), "https://api.securenative.com/collector/api/v1");
            Assert.Equals(options.GetInterval(), 1000);
            Assert.Equals(options.GetTimeout(), 1500);
            Assert.Equals(options.GetTimeout(), 1500);
            Assert.Equals(options.GetMaxEvents(), 1000);
            Assert.Equals(options.IsAutoSend(), true);
            Assert.Equals(options.IsDisabled(), false);
            Assert.Equals(options.GetLogLevel(), "fatal");
            Assert.Equals(options.GetFailoverStrategy(), FailOverStrategy.FAIL_OPEN);
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
            Environment.SetEnvironmentVariable("SECURENATIVE_LOG_LEVEL", "fatal");
            Environment.SetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY", "fail-closed");

            SecureNativeOptions options = ConfigurationManager.LoadConfig();

            Assert.Equals(options.GetApiKey(), "SOME_API_KEY");
            Assert.Equals(options.GetApiUrl(), "SOME_API_URL");
            Assert.Equals(options.GetInterval(), 6000);
            Assert.Equals(options.GetTimeout(), 2000);
            Assert.Equals(options.GetMaxEvents(), 700);
            Assert.Equals(options.IsAutoSend(), false);
            Assert.Equals(options.IsDisabled(), true);
            Assert.Equals(options.GetLogLevel(), "fatal");
            Assert.Equals(options.GetFailoverStrategy(), FailOverStrategy.FAIL_CLOSED);

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

        [TestMethod]
        public void OverwriteEnvVariablesWithConfigFileTest()
        {
            Environment.SetEnvironmentVariable("SECURENATIVE_API_KEY", "API_KEY_FROM_ENV");
            Environment.SetEnvironmentVariable("SECURENATIVE_API_URL", "API_URL_ENV");
            Environment.SetEnvironmentVariable("SECURENATIVE_INTERVAL", "2000");
            Environment.SetEnvironmentVariable("SECURENATIVE_MAX_EVENTS", "200");
            Environment.SetEnvironmentVariable("SECURENATIVE_TIMEOUT", "3000");
            Environment.SetEnvironmentVariable("SECURENATIVE_AUTO_SEND", "true");
            Environment.SetEnvironmentVariable("SECURENATIVE_DISABLE", "true");
            Environment.SetEnvironmentVariable("SECURENATIVE_LOG_LEVEL", "error");
            Environment.SetEnvironmentVariable("SECURENATIVE_FAILOVER_STRATEGY", "fail-open");

            SecureNativeOptions options = ConfigurationManager.LoadConfig();

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

        [TestMethod]
        public void DefaultValuesForInvalidEnumConfigPropsTest()
        {
            SecureNativeOptions options = ConfigurationManager.LoadConfig();
        }
    }
}
