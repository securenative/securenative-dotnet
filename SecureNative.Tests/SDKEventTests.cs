using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Enums;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Models;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class SDKEventTests
    {
        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event with invalid user id")]
        public void CreateSDKEventInvalidUserIdThrowTest()
        {
            SecureNativeOptions options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            EventOptions e = new EventOptions(EventTypes.LOG_IN.ToString());
            e.SetUserId("");

            SdkEvent _ = new SdkEvent(e, options);
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event without user id")]
        public void CreateSDKEventWithoutUserIdThrowTest()
        {
            SecureNativeOptions options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            EventOptions e = new EventOptions(EventTypes.LOG_IN.ToString());

            SdkEvent _ = new SdkEvent(e, options);
        }

        [TestMethod]
        [ExpectedException(typeof(SecureNativeInvalidOptionsException), "Creating SDK event without event type")]
        public void CreateSDKEventWithoutEventTypeThrowTest()
        {
            SecureNativeOptions options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            EventOptions e = new EventOptions("");

            SdkEvent _ = new SdkEvent(e, options);
        }
    }
}
