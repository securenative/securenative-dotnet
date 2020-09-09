using System;
using System.Net.Http;
using SecureNative.SDK.Http;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public interface IEventManager
    {
        HttpResponse SendSync(IEvent e, string url);
        void SendAsync(IEvent e, string url, Boolean retry);
        void StartEventsPersist();
        void StopEventsPersist();
    }
}
