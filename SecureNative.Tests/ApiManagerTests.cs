using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK;
using SecureNative.SDK.Config;
using SecureNative.SDK.Context;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.Tests
{
    [TestClass]
    public class ApiManagerTests
    {
        private readonly SecureNativeOptions _options = ConfigurationManager.ConfigBuilder()
                    .WithApiKey("YOUR_API_KEY")
                    .WithAutoSend(true)
                    .WithInterval(10).Build();

        private readonly SecureNativeContext _context = SDK.SecureNative.ContextBuilder()
            .WithIp("127.0.0.1")
            .WithClientToken("SECURED_CLIENT_TOKEN")
            .WithHeaders(new Dictionary<string, string>() { { "user-agent", "Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405" } })
            .Build();

        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Sending more than 10 custom properties")]
        public void ShouldThrowWhenSendingMoreThan10CustomPropertiesToTrackEventTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .WithContext(_context)
                .WithProperties(new Dictionary<object, object>() { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 }, { "prop4", "CUSTOM_PARAM_VALUE" }, { "prop5", true }, { "prop6", 3 }, { "prop7", "CUSTOM_PARAM_VALUE" }, { "prop8", true }, { "prop9", 3 }, { "prop10", "CUSTOM_PARAM_VALUE" }, { "prop11", true }, { "prop12", 3 }, { "prop13", "CUSTOM_PARAM_VALUE" } })
                .WithTimestamp(new DateTime())
                .Build();

            var eventManager = new EventManager(_options);
            eventManager.StartEventsPersist();
            var apiManager = new ApiManager(eventManager, _options);

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
        public void ShouldCallVerifyEventTest()
        {
            var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
                .WithUserId("USER_ID")
                .WithUserTraits("USER_NAME", "USER_EMAIL")
                .Build();

            var verifyResult = new VerifyResult(RiskLevel.LOW, 0, null);

            var eventManager = new EventManager(_options);
            eventManager.StartEventsPersist();
            var apiManager = new ApiManager(eventManager, _options);

            try
            {
                var result = apiManager.Verify(eventOptions);

                Assert.IsNotNull(result);
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

            var eventManager = new EventManager(_options);
            eventManager.StartEventsPersist();
            var apiManager = new ApiManager(eventManager, _options);

            try
            {
                var verifyResult = apiManager.Verify(eventOptions);

                Assert.IsNotNull(verifyResult);
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
