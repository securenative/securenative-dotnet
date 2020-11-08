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
        public void ExtractRequestWithProxyHeadersIpv4()
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
        
        [TestMethod]
        public void ExtractRequestWithProxyHeadersIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder()
                .WithProxyHeaders(new[] {"CF-Connecting-IP"}).Build();
            
            var headers = new WebHeaderCollection
            {
                {"CF-Connecting-IP", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractRequestWithProxyHeadersMultipleIps()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder()
                .WithProxyHeaders(new[] {"CF-Connecting-IP"}).Build();
            
            var headers = new WebHeaderCollection
            {
                {"CF-Connecting-IP", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }
    }
}