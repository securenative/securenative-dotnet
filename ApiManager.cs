using System;
using Newtonsoft.Json;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Http;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class ApiManager : IApiManager
    {
        private readonly EventManager EventManager;
        private readonly SecureNativeOptions Options;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ApiManager(EventManager eventManager, SecureNativeOptions options)
        {
            this.Options = options;
            this.EventManager = eventManager;
        }

        public void Track(EventOptions eventOptions)
        {
            Logger.Info("Track event call");
            SDKEvent e = new SDKEvent(eventOptions, this.Options);
            this.EventManager.SendAsync(e, ApiRoute.TRACK.ToString(), true);
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            Logger.Info("Verify event call");
            SDKEvent e = new SDKEvent(eventOptions, this.Options);
            try
            {
                HttpResponse res = this.EventManager.SendSync(e, ApiRoute.VERIFY.ToString());
                return JsonConvert.DeserializeObject<VerifyResult>(res.GetBody());
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to call verify %s", ex));
                return this.Options.GetFailoverStrategy() == FailOverStrategy.FAIL_OPEN ?
                        new VerifyResult(RiskLevel.LOW, 0, new String[0])
                        : new VerifyResult(RiskLevel.HIGH, 1, new String[0]);
            }
        }
    }
}
