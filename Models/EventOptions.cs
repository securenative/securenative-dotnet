using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Models
{
    public class EventOptions
    {
        public string IP { get; set; }
        public string CID { get; set; }
        public string VID { get; set; }
        public string FP { get; set; }
        public string UserAgent { get; set; }
        public string EventType { get; set; }
        public string RemoteIP { get; set; }
        public User User { get; set; }
        public Device Device { get; set; }
        public Dictionary<string,string> Params { get; set; }
    }
}
