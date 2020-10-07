using System;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Config
{
    public class SecureNativeConfigurationBuilder
    {
        private string ApiKey { get; set; }
        private string ApiUrl { get; set; }
        private int Interval { get; set; }
        private int MaxEvents { get; set; }
        private int Timeout { get; set; }
        private bool AutoSend { get; set; }
        private bool Disable { get; set; }
        private string LogLevel { get; set; }
        private FailOverStrategy FailoverStrategy { get; set; }

        public static SecureNativeConfigurationBuilder DefaultConfigBuilder()
        {
            return new SecureNativeConfigurationBuilder()
                    .WithApiKey("")
                    .WithApiUrl("https://api.securenative.com/collector/api/v1")
                    .WithInterval(1000)
                    .WithTimeout(1500)
                    .WithMaxEvents(1000)
                    .WithAutoSend(true)
                    .WithDisable(false)
                    .WithLogLevel("fatal")
                    .WithFailoverStrategy(FailOverStrategy.FAIL_OPEN);
        }

        public SecureNativeConfigurationBuilder WithApiKey(string apiKey)
        {
            ApiKey = apiKey;
            return this;
        }

        public SecureNativeConfigurationBuilder WithApiUrl(string apiUrl)
        {
            ApiUrl = apiUrl;
            return this;
        }

        public SecureNativeConfigurationBuilder WithInterval(int interval)
        {
            Interval = interval;
            return this;
        }

        public SecureNativeConfigurationBuilder WithMaxEvents(int maxEvents)
        {
            MaxEvents = maxEvents;
            return this;
        }

        public SecureNativeConfigurationBuilder WithTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }

        public SecureNativeConfigurationBuilder WithAutoSend(bool autoSend)
        {
            AutoSend = autoSend;
            return this;
        }

        public SecureNativeConfigurationBuilder WithDisable(bool disable)
        {
            Disable = disable;
            return this;
        }

        public SecureNativeConfigurationBuilder WithLogLevel(String logLevel)
        {
            LogLevel = logLevel;
            return this;
        }

        public SecureNativeConfigurationBuilder WithFailoverStrategy(FailOverStrategy failoverStrategy)
        {
            FailoverStrategy = failoverStrategy;
            return this;
        }
        
        public SecureNativeOptions Build()
        {
            return new SecureNativeOptions(ApiKey, ApiUrl, Interval, MaxEvents, Timeout, AutoSend, Disable, LogLevel, FailoverStrategy);
        }
    }
}
