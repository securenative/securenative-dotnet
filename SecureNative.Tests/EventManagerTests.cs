using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using SecureNative.SDK.Config;
using SecureNative.SDK.Models;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Tests
{
    public class SampleEvent : IEvent
    {
        private readonly string eventType = "custom-event";
        private readonly string timestamp = DateUtils.GenerateTimestamp();

        public string GetEventType()
        {
            return eventType;
        }

        public string GetTimestamp()
        {
            return timestamp;
        }
    }


    [TestClass]
    public class EventManagerTests
    {
        private readonly SampleEvent e = new SampleEvent();
        private readonly SecureNativeOptions options = ConfigurationManager.ConfigBuilder()
                    .WithApiKey("YOUR_API_KEY")
                    .WithAutoSend(true)
                    .WithInterval(10).Build();


        [TestMethod]
        public void ShouldSuccessfullySendSyncEventWithStatusCode200Test()
        {   
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.securenative.com/collector/api/v1/*").Respond(HttpStatusCode.OK, "application/json", "");
            
            var eventManager = new EventManager(options, mockHttp);
            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(e, "track");

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

            var eventManager = new EventManager(options, mockHttp);

            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(e, "track");

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

            var eventManager = new EventManager(options, mockHttp);

            try
            {
                eventManager.StartEventsPersist();
                var result = eventManager.SendSync(e, "track");

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
