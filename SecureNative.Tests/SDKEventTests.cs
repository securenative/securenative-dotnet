using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.Tests
{
    [TestClass]
    public class SdkEventTests
    {
        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event with invalid user id")]
        public void CreateSdkEventInvalidUserIdThrowTest()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            var e = new EventOptions(EventTypes.LOG_IN.ToString());
            e.SetUserId("");

            SdkEvent _ = new SdkEvent(e, options);
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event without user id")]
        public void CreateSdkEventWithoutUserIdThrowTest()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            var e = new EventOptions(EventTypes.LOG_IN.ToString());

            var _ = new SdkEvent(e, options);
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event without event type")]
        public void CreateSdkEventWithoutEventTypeThrowTest()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            var e = new EventOptions("");

            var _ = new SdkEvent(e, options);
        }
    }
}
