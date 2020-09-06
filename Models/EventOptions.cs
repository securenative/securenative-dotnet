using System;
using System.Collections.Generic;
using SecureNative.SDK.Context;

namespace SecureNative.SDK.Models
{
    public class EventOptions
    {
        public string EventType { get; set; }
        public string UserId { get; set; }
        public UserTraits UserTraits { get; set; }
        public SecureNativeContext Context { get; set; }
        public Dictionary<Object, Object> Properties { get; set; }
        public DateTime Timestamp { get; set; }

        public EventOptions(string eventType)
        {
            this.EventType = eventType;
        }
    }
}