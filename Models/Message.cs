using SecureNative.SDK.Interfaces;

namespace SecureNative.SDK.Models
{
    public class Message
    {
        public IEvent Event { get; set; }
        public string URL { get; set; }
    }
}