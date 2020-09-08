using System;
using System.Net.Http;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public interface IEventManager
    {
        HttpResponseMessage SendSync(IEvent e, string url);
        void SendAsync(IEvent e, string url, Boolean retry);
        void StartEventsPersist();
        void StopEventsPersist();
    }
}
