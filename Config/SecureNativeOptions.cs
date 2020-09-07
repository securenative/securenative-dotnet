using System;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Config
{
    public class SecureNativeOptions
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

        public SecureNativeOptions(string apiKey, string apiUrl, int interval, int maxEvents, int timeout, Boolean autoSend, Boolean disable, string logLevel, FailOverStrategy failoverStrategy)
        {
            this.ApiKey = apiKey;
            this.ApiUrl = apiUrl;
            this.Interval = interval;
            this.MaxEvents = maxEvents;
            this.Timeout = timeout;
            this.AutoSend = autoSend;
            this.Disable = disable;
            this.LogLevel = logLevel;
            this.FailoverStrategy = failoverStrategy;
        }

        public string GetApiKey()
        {
            return this.ApiKey;
        }

        public void SetApiKey(string value)
        {
            this.ApiKey = value;
        }

        public string GetApiUrl()
        {
            return this.ApiUrl;
        }

        public void SetApiUrl(string value)
        {
            this.ApiUrl = value;
        }

        public int GetInterval()
        {
            return this.Interval;
        }

        public void SetInterval(int value)
        {
            this.Interval = value;
        }

        public int GetMaxEvents()
        {
            return this.MaxEvents;
        }

        public void SetMaxEvents(int value)
        {
            this.MaxEvents = value;
        }

        public int GetTimeout()
        {
            return this.Timeout;
        }

        public void SetTimeout(int value)
        {
            this.Timeout = value;
        }

        public Boolean GetAutoSend()
        {
            return this.AutoSend;
        }

        public void SetAutoSend(Boolean value)
        {
            this.AutoSend = value;
        }

        public Boolean GetDisable()
        {
            return this.Disable;
        }

        public void SetDisable(Boolean value)
        {
            this.Disable = value;
        }

        public string GetLogLevel()
        {
            return this.LogLevel;
        }

        public void SetLogLevel(string value)
        {
            this.LogLevel = value;
        }

        public FailOverStrategy GetFailoverStrategy()
        {
            return this.FailoverStrategy;
        }

        public void SetFailoverStrategy(FailOverStrategy value)
        {
            this.FailoverStrategy = value;
        }
    }
}
