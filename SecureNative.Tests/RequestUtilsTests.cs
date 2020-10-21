using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Config;
using SecureNative.SDK.Utils;

namespace SecureNative.Tests
{
    [TestClass]
    public class RequestUtilsTests
    {
        [TestMethod]
        public void ExtractRequestWithProxyHeaders()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder()
                .WithProxyHeaders(new[] {"CF-Connecting-IP"}).Build();
            
            var headers = new WebHeaderCollection
            {
                {"CF-Connecting-IP", "203.0.113.1"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("203.0.113.1", clientIp);
        }
    }
}