using SecureNative.SDK.Models;

namespace SecureNative.SDK.Interfaces
{
    public interface IEventManager
    {
        RiskResult SendSync(EventOptions ievent, string requestUrl);
        void SendAsync(EventOptions ievent, string url);
    }
}