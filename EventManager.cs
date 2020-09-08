using System;
using System.Net.Http;
using SecureNative.SDK.Config;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class EventManager : IEventManager
    {
        private SecureNativeOptions Options;

        public EventManager(SecureNativeOptions options)
        {
            this.Options = options;
        }

        public void SendAsync(IEvent e, string url, bool retry)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage SendSync(IEvent e, string url)
        {
            throw new NotImplementedException();
        }

        public void StartEventsPersist()
        {
            throw new NotImplementedException();
        }

        public void StopEventsPersist()
        {
            throw new NotImplementedException();
        }
    }
}
