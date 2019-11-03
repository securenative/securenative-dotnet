using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;
using System;
 

namespace SecureNative.SDK
{
    public interface IEventManager
    {
        EventOptions BuildEvent(EventOptions eventOptions);
        RiskResult SendSync(EventOptions snEvent, string requestUrl);
        void SendAsync(EventOptions snEvent, string requestURL);
    }
}