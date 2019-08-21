using System;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;
using System.Configuration;
using System.Threading;

namespace SecureNative.SDK
{

    public class SecureNative : ISecureNative
    {
        private const int MAX_CUSTOM_PARAMS = 6;
        private IEventManager _eventManager;
        public SecureNativeOptions Options { get; private set; }
        public string ApiKey { get; private set; }

        public SecureNative(string apiKey, SecureNativeOptions snOptions)
        {
            apiKey = !string.IsNullOrEmpty(apiKey) ? apiKey : ConfigurationManager.AppSettings["snApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new EmptyAPIKeyException("You must pass SecureNative API Key");
            }

            ApiKey = apiKey;
            if (snOptions == null)
            {
                snOptions = new SecureNativeOptions()
                {
                    ApiUrl = "https://api.securenative.com/collector/api/v1",
                    Interval = 1000,
                    MaxEvents = 1000,
                    Timeout = 1500,
                    AutoSend = true,
                    IsSdsEnabled = true
                };
            }
            Options = snOptions;
            _eventManager = new SnEventManager(apiKey, snOptions);
        }

        public void Track(EventOptions eventOptions)
        {
            if (eventOptions != null && eventOptions.Params!=null && eventOptions.Params.Count > MAX_CUSTOM_PARAMS)
            {
                throw new Exception(string.Format("You can only specify maximum of {0} params", MAX_CUSTOM_PARAMS));
            }

            var requestUrl = string.Format("{0}/track", Options.ApiUrl);
            var snEvent = _eventManager.BuildEvent(eventOptions);
            _eventManager.SendAsync(snEvent, requestUrl);
        }

        public RiskResult Verify(EventOptions eventOptions)
        {
            var requestUrl = string.Format("{0}/verify", Options.ApiUrl);
            var snEvent = _eventManager.BuildEvent(eventOptions);
            return _eventManager.SendSync(snEvent, requestUrl);
        }

        public RiskResult Flow(long flowId, EventOptions eventOptions)
        {
            var requestUrl = string.Format("{0}/flow/{1}", Options.ApiUrl, flowId);
            var snEvent = _eventManager.BuildEvent(eventOptions);
            return _eventManager.SendSync(snEvent, requestUrl);
        }

        public RiskResult Risk(EventOptions eventOptions)
        {
            var requestUrl = string.Format("{0}/risk", Options.ApiUrl);
            var snEvent = _eventManager.BuildEvent(eventOptions);
            return _eventManager.SendSync(snEvent, requestUrl);
            //VerifyWebhook.Decrypt(rr.ToString(), this.ApiKey);
        }

       
    }
}