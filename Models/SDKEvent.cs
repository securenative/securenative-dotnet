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
        public string Rid;
        public string EventType;
        public string UserId;
        public UserTraits UserTraits;
        public RequestContext Request;
        public string Timestamp;
        public Dictionary<Object, Object> Properties;

        // TODO: add securenative logger!
        //public static final Logger logger = Logger.getLogger(SecureNative.class);

        public SDKEvent(EventOptions eventOptions, SecureNativeOptions options)
        {
            if (eventOptions.UserId == null || eventOptions.UserId.Length <= 0 || eventOptions.UserId.Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; User Id is missing");
            }

            if (eventOptions.EventType == null || eventOptions.EventType.Length <= 0 || eventOptions.EventType.Equals(""))
            {
                throw new SecureNativeInvalidOptionsException("Invalid event structure; Event Type is missing");
            }

            SecureNativeContext context;
            if (eventOptions.Context != null)
            {
                context = eventOptions.Context;
            }
            else
            {
                context = SecureNativeContextBuilder.DefaultContextBuilder().Build();
            }

            ClientToken clientToken = EncryptionUtils.Decrypt(context.ClientToken, options.ApiKey);

            this.Rid = Guid.NewGuid().ToString();
            this.EventType = eventOptions.EventType;
            this.UserId = eventOptions.UserId;
            this.UserTraits = eventOptions.UserTraits;
            this.Request = new RequestContextBuilder()
                    .WithCid(clientToken.Cid)
                    .WithVid(clientToken.Vid)
                    .WithFp(clientToken.Fp)
                    .WithIp(context.Ip)
                    .WithRemoteIp(context.RemoteIp)
                    .WithMethod(context.Method)
                    .WithUrl(context.Url)
                    .WitHeaders(context.Headers)
                    .Build();
            this.Timestamp = DateUtils.ToTimestamp(eventOptions.Timestamp);
            this.Properties = eventOptions.Properties;
        }

        public string GetEventType()
        {
            return this.EventType;
        }
    }
}
