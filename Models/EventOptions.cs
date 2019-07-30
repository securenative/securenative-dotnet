using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Models
{
    public class EventOptions
    {
        private const int maxCustomParams = 6;
        public EventOptions(string eventType)
        {
            EventType = eventType;
        }
        //private List<KeyValuePair<string, string>> params;

        public string IP { get; set; }
        public string CID { get; set; }
        public string VID { get; set; }
        public string FP { get; set; }
        public string UserAgent { get; set; }
        public string EventType { get; set; }
        public string RemoteIP { get; set; }
        public User User { get; set; }
        public Device Device { get; set; }
        private List<KeyValuePair<string, string>> _params;
        public List<KeyValuePair<string,string>> Params
        {
            get { return _params; }
            set
            {
                if (value.Count > maxCustomParams)
                {
                    _params = value.GetRange(0, 6);
                }
                else
                {
                    _params = value;
                }

            }
        }
    }
   
}
