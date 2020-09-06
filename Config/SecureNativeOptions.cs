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
    }
}
