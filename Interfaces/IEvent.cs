using SecureNative.SDK.Models;
using System.Collections.Generic;

namespace SecureNative.SDK.Interfaces
{
    public interface IEvent
    {
         string EventType { get; set; }
         string Cid { get; set; }
         string Vid { get; set; }
         string Fp { get; set; }
         string Ip { get; set; }
         string RemoteIp { get; set; }
         string UserAgent { get; set; }
         User User { get; set; }
         long Ts { get; set; }
         string Device { get; set; }
         Dictionary<string, string> Params { get; set; }
    }
}