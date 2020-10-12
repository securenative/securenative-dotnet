using System;
using System.Collections.Generic;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class EventOptionsBuilder
    {
        private const int MaxPropertiesSize = 10;
        private readonly EventOptions _eventOptions;

        public static EventOptionsBuilder Builder(string eventType)
        {
            return new EventOptionsBuilder(eventType);
        }

        private EventOptionsBuilder(string eventType)
        {
            _eventOptions = new EventOptions(eventType);
        }

        public EventOptionsBuilder WithUserId(string userId)
        {
            _eventOptions.SetUserId(userId);
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name)
        {
            _eventOptions.SetUserTraits(new UserTraits(name));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email)
        {
            _eventOptions.SetUserTraits(new UserTraits(name, email));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email, string phone, DateTime createdAt)
        {
            _eventOptions.SetUserTraits(new UserTraits(name, email, phone, createdAt));
            return this;
        }

        public EventOptionsBuilder WithUserTraits(string name, string email, string phone)
        {
            _eventOptions.SetUserTraits(new UserTraits(name, email, phone));
            return this;
        }

        public EventOptionsBuilder WithContext(SecureNativeContext context)
        {
            _eventOptions.SetContext(context);
            return this;
        }

        public EventOptionsBuilder WithProperties(Dictionary<Object, Object> properties)
        {
            _eventOptions.SetProperties(properties);
            return this;
        }

        public EventOptionsBuilder WithTimestamp(DateTime timestamp)
        {
            _eventOptions.SetTimestamp(timestamp);
            return this;
        }

        public EventOptions Build()
        {
            if (_eventOptions.GetProperties() != null && _eventOptions.GetProperties().Count > MaxPropertiesSize)
            {
                throw new SecureNativeInvalidOptionsException(
                    $"You can have only up to {MaxPropertiesSize} custom properties");
            }
            return _eventOptions;
        }
    }
}
