namespace SecureNative.SDK.Models
{
    public class SecureNativeOptions
    {
        public string ApiKey { get; set; }

        string _apiUrl= "https://api.securenative.com/collector/api/v1";

        public string ApiUrl
        {
            get {
                return _apiUrl;
            }
            set {
                _apiUrl = value;
            }
        }

        int _interval = 1000;

        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }


        int _maxEvents = 1000;

        public int MaxEvents
        {
            get
            {
                return _maxEvents;
            }
            set
            {
                _maxEvents = value;
            }
        }

        int _timeout = 1500;

        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        bool _autoSend = true;

        public bool AutoSend
        {
            get
            {
                return _autoSend;
            }
            set
            {
                _autoSend = value;
            }
        }

    }
}