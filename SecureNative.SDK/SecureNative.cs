using System;
using System.IO;
using System.Net;
using NLog;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;
using SecureNative.SDK.Utils;
using securenative_dotnet.Utils;

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
            if (options.IsAutoSend())
            {
                eventManager.StartEventsPersist();
            }
            this.ApiManager = new ApiManager(eventManager, options);

            LogLevel logLevel = SecureNativeLogger.GetLogLevel(options.GetLogLevel());
            SecureNativeLogger.InitLogger(logLevel);
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
            SecureNativeConfigurationBuilder builder = SecureNativeConfigurationBuilder.DefaultConfigBuilder();
            SecureNativeOptions secureNativeOptions = builder.WithApiKey(apiKey).Build();
            return Init(secureNativeOptions);
        }


        public static SecureNative Init()
        {
            SecureNativeOptions secureNativeOptions = ConfigurationManager.LoadConfig();
            return Init(secureNativeOptions);
        }

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
            this.ApiManager.Track(eventOptions);
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            return this.ApiManager.Verify(eventOptions);
        }

        public Boolean VerifyRequestPayload(HttpWebRequest request)
        {
            if (request.Headers[SignatureUtils.SIGNATURE_HEADER] != null)
            {
                string requestSignature = request.Headers.GetValues(SignatureUtils.SIGNATURE_HEADER).ToString();

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                string body = sr.ReadToEnd();
                sr.Close();
                res.Close();

                return SignatureUtils.IsValidSignature(requestSignature, body, this.Options.GetApiKey());
            }
            return false;
        }

        public static void Flush()
        {
            Securenative = null;
        }
    }
}
