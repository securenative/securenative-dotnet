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

        // TODO: imeplement logger
        //public static readonly Logger logger = Logger.getLogger(SecureNative.class);

        public ApiManager(EventManager eventManager, SecureNativeOptions options)
        {
            this.Options = options;
            this.EventManager = eventManager;
        }

        public void Track(EventOptions eventOptions)
        {
            // TODO: imeplement me
            //this.logger.info("Track event call");
            SDKEvent e = new SDKEvent(eventOptions, this.Options);
            this.EventManager.SendAsync(e, ApiRoute.TRACK.ToString(), true);
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            // TODO: imeplement me
            //this.logger.info("Verify event call");
            SDKEvent e = new SDKEvent(eventOptions, this.Options);
            try
            {
                HttpResponse res = this.EventManager.SendSync(e, ApiRoute.VERIFY.ToString());
                return JsonConvert.DeserializeObject<VerifyResult>(res.GetBody());
            }
            catch (Exception ex)
            {
                // TODO: imeplement me
                //this.logger.error("Failed to call verify", ex);
                return this.Options.GetFailoverStrategy() == FailOverStrategy.FAIL_OPEN ?
                        new VerifyResult(RiskLevel.LOW, 0, new String[0])
                        : new VerifyResult(RiskLevel.HIGH, 1, new String[0]);
            }
        }
    }
}
