using SecureNative.SDK.Interfaces;
using System.Collections.Generic;

namespace SecureNative.SDK.Models
{
    public class SnEvent : IEvent
    {
        public string EventType { get; set; }
        public string Cid { get; set; }
        public string Vid { get; set; }
        public string Fp { get; set; }
        public string Ip { get; set; }
        public string RemoteIp { get; set; }
        public string UserAgent { get; set; }
        public User User{ get; set; }
        public long Ts { get; set; }
        public string Device { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }
}

