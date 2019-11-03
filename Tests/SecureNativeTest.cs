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
        ISecureNative secureNative;
        readonly string apiKey = "1234";

        [ClassInitialize]
        public void Setup()
        {
            secureNative = 
                SecureNative.Init(apiKey, new Models.SecureNativeOptions());
        }

        [TestMethod]
        public void TestEventOptionsWithMoreThanSixParams()
        {
            var apiKey = "1234";
            ISecureNative sn = SecureNative.GetInstance();
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
