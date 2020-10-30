using System;
using System.Collections.Generic;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Http;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Models
{
    public class SdkEvent : IEvent
    {
        private string Rid { get; }
        private string EventType { get; }
        private string UserId { get; }
        private UserTraits UserTraits { get; }
        private RequestContext Request { get; }
        private string Timestamp { get; }
        private Dictionary<object, object> Properties { get; }

        public SdkEvent(EventOptions eventOptions, SecureNativeOptions options)
        {
            if (eventOptions.GetUserId() == null || eventOptions.GetUserId().Length <= 0 || eventOptions.GetUserId().Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; User Id is missing");
            }

            if (eventOptions.GetEventType() == null || eventOptions.GetEventType().Length <= 0 || eventOptions.GetEventType().Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; Event Type is missing");
            }

            var context = eventOptions.GetContext() ?? SecureNativeContextBuilder.DefaultContextBuilder().Build();
            var clientToken = EncryptionUtils.Decrypt(context.GetClientToken(), options.GetApiKey());

            Rid = Guid.NewGuid().ToString();
            EventType = eventOptions.GetEventType();
            UserId = eventOptions.GetUserId();
            UserTraits = eventOptions.GetUserTraits();
            Request = new RequestContextBuilder()
                    .WithCid(clientToken.GetCid())
                    .WithVid(clientToken.GetVid())
                    .WithFp(clientToken.GetFp())
                    .WithIp(context.GetIp())
                    .WithRemoteIp(context.GetRemoteIp())
                    .WithMethod(context.GetMethod())
                    .WithUrl(context.GetUrl())
                    .WitHeaders(context.GetHeaders())
                    .Build();
            
            Timestamp = DateUtils.ToTimestamp(eventOptions.GetTimestamp() ?? new DateTime());
            Properties = eventOptions.GetProperties();
        }

        public string GetEventType()
        {
            return EventType;
        }

        public string GetRid()
        {
            return Rid;
        }

        public string GetUserId()
        {
            return UserId;
        }

        public UserTraits GetUserTraits()
        {
            return UserTraits;
        }

        public RequestContext GetRequest()
        {
            return Request;
        }

        public string GetTimestamp()
        {
            return Timestamp;
        }

        public Dictionary<object, object> GetProperties()
        {
            return Properties;
        }
    }
}
