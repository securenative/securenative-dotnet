using System;
using SecureNative.SDK.Config;
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
            // TODO: implement me
            throw new NotImplementedException();
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
