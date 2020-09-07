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
        private Boolean AutoSend { get; set; }
        private Boolean Disable { get; set; }
        private string LogLevel { get; set; }
        private FailOverStrategy FailoverStrategy { get; set; }

        public SecureNativeConfigurationBuilder()
        {
        }

        public static SecureNativeConfigurationBuilder defaultConfigBuilder()
        {
            return new SecureNativeConfigurationBuilder()
                    .WithApiKey(null)
                    .WithApiUrl("https://api.securenative.com/collector/api/v1")
                    .WithInterval(1000)
                    .WithTimeout(1500)
                    .WithMaxEvents(1000)
                    .WithAutoSend(true)
                    .WithDisable(false)
                    .WithLogLevel("fatal")
                    .WithFailoverStrategy(FailOverStrategy.FAIL_OPEN);
        }

        public SecureNativeConfigurationBuilder WithApiKey(String apiKey)
        {
            this.ApiKey = apiKey;
            return this;
        }

        public SecureNativeConfigurationBuilder WithApiUrl(String apiUrl)
        {
            this.ApiUrl = apiUrl;
            return this;
        }

        public SecureNativeConfigurationBuilder WithInterval(int interval)
        {
            this.Interval = interval;
            return this;
        }

        public SecureNativeConfigurationBuilder WithMaxEvents(int maxEvents)
        {
            this.MaxEvents = maxEvents;
            return this;
        }

        public SecureNativeConfigurationBuilder WithTimeout(int timeout)
        {
            this.Timeout = timeout;
            return this;
        }

        public SecureNativeConfigurationBuilder WithAutoSend(Boolean autoSend)
        {
            this.AutoSend = autoSend;
            return this;
        }

        public SecureNativeConfigurationBuilder WithDisable(Boolean disable)
        {
            this.Disable = disable;
            return this;
        }

        public SecureNativeConfigurationBuilder WithLogLevel(String logLevel)
        {
            this.LogLevel = logLevel;
            return this;
        }

        public SecureNativeConfigurationBuilder WithFailoverStrategy(FailOverStrategy failoverStrategy)
        {
            this.FailoverStrategy = failoverStrategy;
            return this;
        }


        public SecureNativeOptions Build()
        {
            return new SecureNativeOptions(ApiKey, ApiUrl, Interval, MaxEvents, Timeout, AutoSend, Disable, LogLevel, FailoverStrategy);
        }
    }
}
