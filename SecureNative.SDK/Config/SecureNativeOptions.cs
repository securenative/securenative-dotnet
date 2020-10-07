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
        private bool AutoSend { get; set; }
        private bool Disable { get; set; }
        private string LogLevel { get; set; }
        private FailOverStrategy FailoverStrategy { get; set; }

        public SecureNativeOptions(string apiKey, string apiUrl, int interval, int maxEvents, int timeout, bool autoSend, bool disable, string logLevel, FailOverStrategy failoverStrategy)
        {
            ApiKey = apiKey;
            ApiUrl = apiUrl;
            Interval = interval;
            MaxEvents = maxEvents;
            Timeout = timeout;
            AutoSend = autoSend;
            Disable = disable;
            LogLevel = logLevel;
            FailoverStrategy = failoverStrategy;
        }

        public string GetApiKey()
        {
            return ApiKey;
        }

        public void SetApiKey(string value)
        {
            ApiKey = value;
        }

        public string GetApiUrl()
        {
            return ApiUrl;
        }

        public void SetApiUrl(string value)
        {
            ApiUrl = value;
        }

        public int GetInterval()
        {
            return Interval;
        }

        public void SetInterval(int value)
        {
            Interval = value;
        }

        public int GetMaxEvents()
        {
            return MaxEvents;
        }

        public void SetMaxEvents(int value)
        {
            MaxEvents = value;
        }

        public int GetTimeout()
        {
            return Timeout;
        }

        public void SetTimeout(int value)
        {
            Timeout = value;
        }

        public bool IsAutoSend()
        {
            return AutoSend;
        }

        public void SetAutoSend(bool value)
        {
            AutoSend = value;
        }

        public bool IsDisabled()
        {
            return Disable;
        }

        public void SetDisabled(bool value)
        {
            Disable = value;
        }

        public string GetLogLevel()
        {
            return LogLevel;
        }

        public void SetLogLevel(string value)
        {
            LogLevel = value;
        }

        public FailOverStrategy GetFailoverStrategy()
        {
            return FailoverStrategy;
        }

        public void SetFailoverStrategy(FailOverStrategy value)
        {
            FailoverStrategy = value;
        }
    }
}
