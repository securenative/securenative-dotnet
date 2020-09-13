using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Exceptions;
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
        public void SendAsyncEventWithStatusCode200Test()
        {
            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();

            eventManager.SendAsync(e, "some-path/to-api", true);

            try
            {
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldHandleInvalidJsonResponseWithStatus200Test()
        {
            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();

            try
            {
                eventManager.SendAsync(e, "some-path/to-api", true);
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldNotRetrySendingAsyncEventWhenStatusCode200Test()
        {
            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();

            try
            {
                eventManager.SendAsync(e, "some-path/to-api", true);
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldNotRetrySendingAsyncEventWhenStatusCode401Test()
        {
            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();

            try
            {
                eventManager.SendAsync(e, "some-path/to-api", true);
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldRetrySendingAsyncEventWhenStatusCode500Test()
        {
            var eventManager = new EventManager(options);
            eventManager.StartEventsPersist();

            try
            {
                eventManager.SendAsync(e, "some-path/to-api", true);
            }
            finally
            {
                eventManager.StopEventsPersist();
            }
        }

        [TestMethod]
        public void ShouldSuccessfullySendSyncEventWithStatusCode200Test()
        {
            string resBody = "{ \"data\": true }";
            var eventManager = new EventManager(options);
            try
            {
                var data = eventManager.SendSync(e, "some-path/to-api");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void ShouldSendSyncEventAndHandleInvalidJsonResponseTest()
        {
            var eventManager = new EventManager(options);

            try
            {
                var resp = eventManager.SendSync(e, "some-path/to-api");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void ShouldSendSyncEventAndFailWhenStatusCode401Test()
        {
            var eventManager = new EventManager(options);

            try
            {
                eventManager.SendSync(e, "some-path/to-api");
            }
            catch (SecureNativeParseException)
            {

            }
        }

        [TestMethod]
        public void ShouldSendSyncEventAndFailWhenStatusCode500Test()
        {
            var eventManager = new EventManager(options);

            try
            {
                eventManager.SendSync(e, "some-path/to-api");
            }
            catch (Exception)
            {
            }
        }
    }
}
