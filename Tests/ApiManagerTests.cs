using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class ApiManagerTests
    {
        private readonly SecureNativeOptions options = ConfigurationManager.ConfigBuilder()
                    .WithApiKey("YOUR_API_KEY")
                    .WithAutoSend(true)
                    .WithInterval(10).Build();

        private readonly SecureNativeContext context = SecureNative.ContextBuilder()
            .WithIp("127.0.0.1")
            .WithClientToken("SECURED_CLIENT_TOKEN")
            .WithHeaders(new Dictionary<string, string>() { { "user-agent", "Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405" } })
            .Build();

        [TestMethod]
        public void ShouldCallTrackEventTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .WithContext(context)
                .WithProperties(new Dictionary<Object, Object>() { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 } })
                .WithTimestamp(new DateTime())
                .Build();

            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();
            ApiManager apiManager = new ApiManager(eventManager, options);

            try
            {
                apiManager.Track(eventOptions);
            }
            catch (SecureNativeInvalidOptionsException)
            {
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Sending more than 10 custome properties")]
        public void ShouldThrowWhenSendingMoreThan10CustomPropertiesToTrackEventTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .WithContext(context)
                .WithProperties(new Dictionary<Object, Object>() { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 }, { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 }, { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 }, { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 }, { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 } })
                .WithTimestamp(new DateTime())
                .Build();

            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();
            ApiManager apiManager = new ApiManager(eventManager, options);

            try
            {
                apiManager.Track(eventOptions);
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldNotCallTrackEventWhenAutomaticPersistenceDisabledTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .WithContext(context)
                .Build();

            var eventManager = new EventManager(options);
            ApiManager apiManager = new ApiManager(eventManager, options);

            try
            {
                apiManager.Track(eventOptions);
            }
            catch (SecureNativeInvalidOptionsException)
            {
            }
        }

        [TestMethod]
        public void ShouldNotRetryUnauthorizedTrackEventCallTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .Build();
            var eventManager = new EventManager(options);

            eventManager.StartEventsPersist();
            ApiManager apiManager = new ApiManager(eventManager, options);

            try
            {
                apiManager.Track(eventOptions);
            }
            catch (SecureNativeInvalidOptionsException)
            {
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldCallVerifyEventTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .Build();

            VerifyResult verifyResult = new VerifyResult(RiskLevel.LOW, 0, new String[0]);

            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();
            ApiManager apiManager = new ApiManager(eventManager, options);

            VerifyResult result = null;
            try
            {
                result = apiManager.Verify(eventOptions);
            }
            catch (SecureNativeInvalidOptionsException)
            {
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldFailVerifyEventCallWhenUnauthorizedTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .Build();

            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();
            ApiManager apiManager = new ApiManager(eventManager, options);

            VerifyResult verifyResult = null;
            try
            {
                verifyResult = apiManager.Verify(eventOptions);
            }
            catch (SecureNativeInvalidOptionsException)
            {
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }
    }
}
