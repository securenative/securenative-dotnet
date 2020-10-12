using System.IO;
using System.Net;
using System.Text;
using NLog;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK
{
    public class SecureNative : IApiManager
    {
        private static SecureNative _securenative;
        private readonly ApiManager _apiManager;
        private readonly SecureNativeOptions _options;

        public SecureNative(SecureNativeOptions options)
        {
            if (string.IsNullOrEmpty(options.GetApiKey()))
            {
                throw new SecureNativeSdkException("You must pass your SecureNative api key");
            }
            _options = options;

            var eventManager = new EventManager(options);
            if (options.IsAutoSend())
            {
                eventManager.StartEventsPersist();
            }
            _apiManager = new ApiManager(eventManager, options);

            LogLevel logLevel = SecureNativeLogger.GetLogLevel(options.GetLogLevel());
            SecureNativeLogger.InitLogger(logLevel);
        }

        public static SecureNative Init(SecureNativeOptions options)
        {
            if (_securenative != null) throw new SecureNativeSdkException("This SDK was already initialized");
            _securenative = new SecureNative(options);
            return _securenative;
        }

        public static SecureNative Init(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
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
            if (_securenative == null)
            {
                throw new SecureNativeSdkIllegalStateException();
            }
            return _securenative;
        }

        public SecureNativeOptions GetOptions()
        {
            return _options;
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
            _apiManager.Track(eventOptions);
        }

        public VerifyResult Verify(EventOptions eventOptions)
        {
            return _apiManager.Verify(eventOptions);
        }

        public bool VerifyRequestPayload(HttpWebRequest request)
        {
            if (request.Headers[SignatureUtils.SignatureHeader] == null) return false;
            var requestSignature = request.Headers.GetValues(SignatureUtils.SignatureHeader)?.ToString();

            var res = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(res.GetResponseStream()!, Encoding.Default);
            var body = sr.ReadToEnd();
            sr.Close();
            res.Close();

            return SignatureUtils.IsValidSignature(requestSignature, body, _options.GetApiKey());
        }

        public static void Flush()
        {
            _securenative = null;
        }
    }
}
