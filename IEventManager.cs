using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;
using System;
 

namespace SecureNative.SDK
{
    public interface IEventManager
    {
        SnEvent BuildEvent(EventOptions eventOptions);
        RiskResult SendSync(IEvent snEvent, string requestUrl);
        void SendAsync(IEvent snEvent, string requestURL);
    }
}