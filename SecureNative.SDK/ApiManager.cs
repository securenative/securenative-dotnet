using System;
using Newtonsoft.Json;
using NLog;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class ApiManager : IApiManager
    {
        private readonly EventManager _eventManager;
        private readonly SecureNativeOptions _options;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ApiManager(EventManager eventManager, SecureNativeOptions options)
        {
            _options = options;
            _eventManager = eventManager;
        }

        public void Track(EventOptions eventOptions)
        {
            Logger.Info("Track event call");
            var e = new SdkEvent(eventOptions, _options);
            _eventManager.SendAsync(e, ApiRoute.TRACK.ToString(), true);
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            Logger.Info("Verify event call");
            var e = new SdkEvent(eventOptions, _options);
            try
            {
                var res = _eventManager.SendSync(e, ApiRoute.VERIFY.ToString());
                return JsonConvert.DeserializeObject<VerifyResult>(res.GetBody());
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to call verify; {ex.Message}");
                return _options.GetFailoverStrategy() == FailOverStrategy.FAIL_OPEN ?
                        new VerifyResult(RiskLevel.LOW, 0, new string[0])
                        : new VerifyResult(RiskLevel.HIGH, 1, new string[0]);
            }
        }
    }
}
