using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Exceptions;
using SecureNative.SDK.Interfaces;
using SecureNative.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class SecureNativeTest
    {
        public object Params { get; private set; }

        [TestMethod]
        [ExpectedException(typeof(EmptyAPIKeyException), "You must pass SecureNative API Key")]
        public void TestInitializationOfSecureNativeWithEmptyApiKey()
        {
            var apiKey = "";
            SecureNative sn = new SecureNative(apiKey, new Models.SecureNativeOptions());
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyAPIKeyException), "You must pass SecureNative API Key")]
        public void TestInitializationOfSecureNativeWithNullApiKey()
        {
            SecureNative sn = new SecureNative(null, new Models.SecureNativeOptions());
        }

        [TestMethod]
        public void TestSnOptionsNull()
        {
            var apiKey = "1234";
            SecureNative sn = new SecureNative(apiKey, null);
            Assert.AreEqual(sn.ApiKey, apiKey, "api key differences");
        }

        [TestMethod]
        public void TestEventOptionsWithMoreThanSixParams()
        {
            var apiKey = "1234";
            SecureNative sn = new SecureNative(apiKey, new Models.SecureNativeOptions());
            EventOptions eventOtion = new EventOptions("test")
            {
                IP = "",
                Params = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("p1","v1"),
                    new KeyValuePair<string, string>("p2","v2"),
                    new KeyValuePair<string, string>("p3","v3"),
                    new KeyValuePair<string, string>("p4","v4"),
                    new KeyValuePair<string, string>("p5","v5"),
                    new KeyValuePair<string, string>("p6","v6"),
                    new KeyValuePair<string, string>("p7","v7")
                }
            };
            Assert.AreEqual(eventOtion.Params.Count, 6, "max size of params is 6");
        }

    }
}
