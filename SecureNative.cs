using System;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class SecureNative : IApiManager
    {
        private static SecureNative Securenative = null;
        private readonly ApiManager ApiManager;
        private readonly SecureNativeOptions Options;

        public SecureNative(SecureNativeOptions options)
        {
            if (Utils.Utils.IsNullOrEmpty(options.GetApiKey()))
            {
                throw new SecureNativeSDKException("You must pass your SecureNative api key");
            }
            this.Options = options;

            EventManager eventManager = new EventManager(options);
            if (options.GetAutoSend())
            {
                eventManager.StartEventsPersist();
            }
            this.ApiManager = new ApiManager(eventManager, options);

            // TODO: implement logger
            //Logger.initLogger(options.getLogLevel());
        }

        public static SecureNative Init(SecureNativeOptions options)
        {
            if (Securenative == null)
            {
                Securenative = new SecureNative(options);
                return Securenative;
            }
            throw new SecureNativeSDKException("This SDK was already initialized");
        }

        public static SecureNative Init(string apiKey)
        {
            if (Utils.Utils.IsNullOrEmpty(apiKey))
            {
                throw new SecureNativeConfigException("You must pass your SecureNative api key");
            }
            SecureNativeConfigurationBuilder builder = SecureNativeConfigurationBuilder.defaultConfigBuilder();
            SecureNativeOptions secureNativeOptions = builder.WithApiKey(apiKey).Build();
            return Init(secureNativeOptions);
        }


        public static SecureNative Init()
        {
            SecureNativeOptions secureNativeOptions = ConfigurationManager.LoadConfig();
            return Init(secureNativeOptions);
        }

        //public static SecureNative Init(string path)
        //{
        //    SecureNativeOptions secureNativeOptions = ConfigurationManager.LoadConfig(path);
        //    return Init(secureNativeOptions);
        //}

        public static SecureNative GetInstance()
        {
            if (Securenative == null)
            {
                throw new SecureNativeSDKIllegalStateException();
            }
            return Securenative;
        }

        public SecureNativeOptions GetOptions()
        {
            return this.Options;
        }

        public static SecureNativeConfigurationBuilder ConfigBuilder()
        {
            return SecureNativeConfigurationBuilder.DefaultConfigBuilder();
        }

        public static SecureNativeContextBuilder ContextBuilder()
        {
            return SecureNativeContextBuilder.DefaultContextBuilder();
        }

        public void Track(EventOptions eventOptions)
        {
            throw new NotImplementedException();
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            throw new NotImplementedException();
        }
    }
}
