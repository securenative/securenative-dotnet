using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using SecureNative.SDK;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Models;

namespace SecureNative.Tests
{
    [TestClass]
    public class EventManagerTests
    {
        private static readonly SecureNativeOptions Options = ConfigurationManager.ConfigBuilder()
                    .WithApiKey("YOUR_API_KEY")
                    .WithAutoSend(true)
                    .WithInterval(10).Build();

        private static readonly EventOptions EOptions = new EventOptions(EventTypes.LOG_IN.ToString(), "12345");
        private readonly SdkEvent _e = new SdkEvent(EOptions, Options);


        [TestMethod]
        public void ShouldSuccessfullySendSyncEventWithStatusCode200Test()
        {   
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.securenative.com/collector/api/v1/*").Respond(HttpStatusCode.OK, "application/json", "");
            
            var eventManager = new EventManager(Options, mockHttp);
            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(_e, "track");

                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.GetStatusCode());
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }


        [TestMethod]
        public void ShouldSendSyncEventAndFailWhenStatusCode401Test()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.securenative.com/collector/api/v1/*").Respond(HttpStatusCode.Unauthorized, "application/json", "");

            var eventManager = new EventManager(Options, mockHttp);

            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(_e, "track");

                Assert.IsNotNull(result);
                Assert.AreEqual(401, result.GetStatusCode());
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldSendSyncEventAndFailWhenStatusCode500Test()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.securenative.com/collector/api/v1/*").Respond(HttpStatusCode.InternalServerError, "application/json", "");

            var eventManager = new EventManager(Options, mockHttp);

            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(_e, "track");

                Assert.IsNotNull(result);
                Assert.AreEqual(500, result.GetStatusCode());
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }
    }
}
