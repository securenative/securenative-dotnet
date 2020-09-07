using System;
using System.Collections.Generic;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Utils;
using SecureNative.SDK.Http;

namespace SecureNative.SDK.Models
{
    public class SDKEvent : IEvent
    {
        private string Rid { get; set; }
        private string EventType { get; set; }
        private string UserId { get; set; }
        private UserTraits UserTraits { get; set; }
        private RequestContext Request { get; set; }
        private string Timestamp { get; set; }
        private Dictionary<Object, Object> Properties { get; set; }

        // TODO: add securenative logger!
        //public static final Logger logger = Logger.getLogger(SecureNative.class);

        public SDKEvent(EventOptions eventOptions, SecureNativeOptions options)
        {
            if (eventOptions.GetUserId() == null || eventOptions.GetUserId().Length <= 0 || eventOptions.GetUserId().Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; User Id is missing");
            }

            if (eventOptions.GetEventType() == null || eventOptions.GetEventType().Length <= 0 || eventOptions.GetEventType().Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; Event Type is missing");
            }

            SecureNativeContext context;
            if (eventOptions.GetContext() != null)
            {
                context = eventOptions.GetContext();
            }
            else
            {
                context = SecureNativeContextBuilder.DefaultContextBuilder().Build();
            }

            ClientToken clientToken = EncryptionUtils.Decrypt(context.GetClientToken(), options.GetApiKey());

            this.Rid = Guid.NewGuid().ToString();
            this.EventType = eventOptions.GetEventType();
            this.UserId = eventOptions.GetUserId();
            this.UserTraits = eventOptions.GetUserTraits();
            this.Request = new RequestContextBuilder()
                    .WithCid(clientToken.GetCid())
                    .WithVid(clientToken.GetVid())
                    .WithFp(clientToken.GetFp())
                    .WithIp(context.GetIp())
                    .WithRemoteIp(context.GetRemoteIp())
                    .WithMethod(context.GetMethod())
                    .WithUrl(context.GetUrl())
                    .WitHeaders(context.GetHeaders())
                    .Build();
            this.Timestamp = DateUtils.ToTimestamp(eventOptions.GetTimestamp());
            this.Properties = eventOptions.GetProperties();
        }

        public string GetEventType()
        {
            return this.EventType;
        }
    }
}
