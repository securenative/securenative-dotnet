using SecureNative.SDK.Models;

namespace SecureNative.SDK.Interfaces
{
    public interface IEventManager
    {
        RiskResult SendSync(IEvent ievent, string requestUrl);
        void SendAsync(IEvent ievent, string url);
    }
}