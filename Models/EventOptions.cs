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

        public string GetEventType()
        {
            return this.EventType;
        }

        public void SetEventType(string value)
        {
            this.EventType = value;
        }

        public string GetUserId()
        {
            return this.UserId;
        }

        public void SetUserId(string value)
        {
            this.UserId = value;
        }

        public UserTraits GetUserTraits()
        {
            return this.UserTraits;
        }

        public void SetUserTraits(UserTraits value)
        {
            this.UserTraits = value;
        }

        public SecureNativeContext GetContext()
        {
            return this.Context;
        }

        public void SetContext(SecureNativeContext value)
        {
            this.Context = value;
        }

        public Dictionary<Object, Object> GetProperties()
        {
            return this.Properties;
        }

        public void SetProperties(Dictionary<Object, Object> value)
        {
            this.Properties = value;
        }

        public DateTime GetTimestamp()
        {
            return this.Timestamp;
        }

        public void SetTimestamp(DateTime value)
        {
            this.Timestamp = value;
        }
    }
}