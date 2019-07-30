using System;
using System.Threading;
using Newtonsoft.Json;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    
    public class SnEventManager : IEventManager 
    {
        private const string USER_AGENT_VALUE = "SecureNative .NET";
        private const string SN_VERSION = "SN-Version";
        private const string AUTHORIZATION = "Authorization";

        public string ApiKey { get; private set; }
        private ConcurrentQueue<Message> _events;
        private IMessageSender<IEvent> _messageSender;
        private SecureNativeOptions _options;

        public SnEventManager(string apiKey, SecureNativeOptions options)
        {
            ApiKey = apiKey;

            _events = new ConcurrentQueue<Message>();
            _options = options;

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new EmptyAPIKeyException("You must pass a valid api key");
            }

    
            _messageSender = new MessageSender<IEvent>(apiKey);
            Thread thread = new Thread(SendEvent);
            thread.IsBackground = true;
            thread.Start();   
        }

        public SnEvent BuildEvent(EventOptions eventOptions)
        {
            return new SnEvent()
            {
                EventType = eventOptions != null && !string.IsNullOrEmpty(eventOptions.EventType) ? eventOptions.EventType : EventTypes.LOG_IN.ToDescriptionString(),
                Cid = eventOptions != null ? eventOptions.CID : null,
                Vid = Guid.NewGuid().ToString(),
                Fp = eventOptions != null ?  eventOptions.FP : null,
                Ip = eventOptions != null ? eventOptions.IP : null,
                RemoteIp = eventOptions != null ?  eventOptions.RemoteIP : null,
                UserAgent = eventOptions != null ?  eventOptions.UserAgent  : null,
                User = eventOptions != null ?  eventOptions.User : null,
                Ts= DateTime.UtcNow.Millisecond,
                Device = eventOptions != null ? eventOptions.Device : null,
                Params = eventOptions != null ? eventOptions.Params : null
            };
        }

        private void SendEvent()
        {
            while (true)
            {
                if (_events.Count> 0)
                {
                    var message = _events.Dequeue();
                   var response = _messageSender.Post(message.URL, message.Event);
                    //check for failure and do backoff retry
                }
                Thread.Sleep(_options.Interval);
            }
        }

        public RiskResult SendSync(IEvent snEvent, string requestUrl)
        {
            var result =_messageSender.Post(requestUrl, snEvent);
    
            var riskReult = JsonConvert.DeserializeObject<RiskResult>(result);

            return riskReult;
        
        }

        public void SendAsync(IEvent snEvent, string requestUrl)
        {
            if (_events.Count >= this._options.MaxEvents)
            {
                _events.Dequeue();
            }

            _events.Enqueue(new Message()
            {
                Event= snEvent,
                URL= requestUrl
            });
        }
    }
}