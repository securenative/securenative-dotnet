using System;
using System.Collections.Generic;
using SecureNative.SDK.Context;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class EventOptionsBuilder
    {
        private readonly int MAX_PROPERTIES_SIZE = 10;
        private readonly EventOptions EventOptions;

        public static EventOptionsBuilder Builder(string eventType)
        {
            return new EventOptionsBuilder(eventType);
        }

        public static EventOptionsBuilder Builder(EventTypes eventType)
        {
            return new EventOptionsBuilder(eventType.ToString());
        }

        private EventOptionsBuilder(string eventType)
        {
            this.EventOptions = new EventOptions(eventType);
        }

        public EventOptionsBuilder WithUserId(string userId)
        {
            this.EventOptions.SetUserId(userId);
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name)
        {
            this.EventOptions.SetUserTraits(new UserTraits(name));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email)
        {
            this.EventOptions.SetUserTraits(new UserTraits(name, email));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email, string phone, DateTime createdAt)
        {
            this.EventOptions.SetUserTraits(new UserTraits(name, email, phone, createdAt));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email, string phone)
        {
            this.EventOptions.SetUserTraits(new UserTraits(name, email, phone));
            return this;
        }

        public EventOptionsBuilder WithContext(SecureNativeContext context)
        {
            this.EventOptions.SetContext(context);
            return this;
        }

        public EventOptionsBuilder WithProperties(Dictionary<Object, Object> properties)
        {
            this.EventOptions.SetProperties(properties);
            return this;
        }

        public EventOptionsBuilder WithTimestamp(DateTime timestamp)
        {
            this.EventOptions.SetTimestamp(timestamp);
            return this;
        }

        public EventOptions Build()
        {
            if (this.EventOptions.GetProperties() != null && this.EventOptions.GetProperties().Count > MAX_PROPERTIES_SIZE)
            {
                throw new SecureNativeInvalidOptionsException(String.Format("You can have only up to %d custom properties", MAX_PROPERTIES_SIZE));
            }
            return this.EventOptions;
        }
    }
}
