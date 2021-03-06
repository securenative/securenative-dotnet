﻿using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK
{
    public class Client : IApiManager
    {
        private static Client _securenative;
        private readonly ApiManager _apiManager;
        private static SecureNativeOptions _options;

        public Client(SecureNativeOptions options)
        {
            if (string.IsNullOrEmpty(options.GetApiKey()))
            {
                throw new SecureNativeSdkException("You must pass your SecureNative api key");
            }

            var eventManager = new EventManager(options);
            if (options.IsAutoSend())
            {
                eventManager.StartEventsPersist();
            }

            _apiManager = new ApiManager(eventManager, options);

            var logLevel = SecureNativeLogger.GetLogLevel(options.GetLogLevel());
            SecureNativeLogger.InitLogger(logLevel);
        }

        public static Client Init(SecureNativeOptions options)
        {
            if (_securenative != null) throw new SecureNativeSdkException("This SDK was already initialized");
            _securenative = new Client(options);
            _options = options;
            return _securenative;
        }

        public static Client Init(string value)
        {
            if (value.Contains("/") || value.Contains("\""))
            {
                var secureNativeOptions = ConfigurationManager.LoadConfig(value);
                return Init(secureNativeOptions);
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new SecureNativeConfigException("You must pass your SecureNative api key");
                }

                var builder = SecureNativeConfigurationBuilder.DefaultConfigBuilder();
                var secureNativeOptions = builder.WithApiKey(value).Build();
                return Init(secureNativeOptions);
            }
        }
        
        public static Client GetInstance()
        {
            if (_securenative == null)
            {
                throw new SecureNativeSdkIllegalStateException();
            }

            return _securenative;
        }

        public static SecureNativeOptions GetOptions()
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

        public static SecureNativeContextBuilder FromHttpRequest(HttpWebRequest request)
        {
            return SecureNativeContextBuilder.FromHttpRequest(request, _options);
        }
        
        public static SecureNativeContextBuilder FromHttpRequest(HttpRequest request)
        {
            return SecureNativeContextBuilder.FromHttpRequest(request, _options);
        }

        public bool VerifyRequestPayload(HttpWebRequest request)
        {
            if (request.Headers[SignatureUtils.SignatureHeader] == null) return false;
            var requestSignature = request.Headers.GetValues(SignatureUtils.SignatureHeader)?.ToString();

            var res = (HttpWebResponse) request.GetResponse();
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