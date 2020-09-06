using System;
using System.Collections.Generic;
using SecureNative.SDK.Context;

namespace SecureNative.SDK.Models
{
    public class EventOptions
    {
        private string EventType { get; set; }
        private string UserId { get; set; }
        private UserTraits UserTraits { get; set; }
        private SecureNativeContext Context { get; set; }
        private Dictionary<Object, Object> Properties { get; set; }
        private DateTime Timestamp { get; set; }

        public EventOptions(string eventType)
        {
            this.EventType = eventType;
        }
    }
}