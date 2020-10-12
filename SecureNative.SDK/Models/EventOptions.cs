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
        private Dictionary<object, object> Properties { get; set; }
        private DateTime? Timestamp { get; set; }

        public EventOptions(string eventType)
        {
            EventType = eventType;
        }
        
        public EventOptions(string eventType, string userId)
        {
            EventType = eventType;
            UserId = userId;
        }

        public string GetEventType()
        {
            return EventType;
        }

        public void SetEventType(string value)
        {
            EventType = value;
        }

        public string GetUserId()
        {
            return UserId;
        }

        public void SetUserId(string value)
        {
            UserId = value;
        }

        public UserTraits GetUserTraits()
        {
            return UserTraits;
        }

        public void SetUserTraits(UserTraits value)
        {
            UserTraits = value;
        }

        public SecureNativeContext GetContext()
        {
            return Context;
        }

        public void SetContext(SecureNativeContext value)
        {
            Context = value;
        }

        public Dictionary<Object, Object> GetProperties()
        {
            return Properties;
        }

        public void SetProperties(Dictionary<Object, Object> value)
        {
            Properties = value;
        }

        public DateTime? GetTimestamp()
        {
            return Timestamp;
        }

        public void SetTimestamp(DateTime value)
        {
            Timestamp = value;
        }
    }
}