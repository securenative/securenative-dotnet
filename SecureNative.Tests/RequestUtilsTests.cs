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

        [TestMethod]
        public void ExtractIpUsingXForwardedForHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-forwarded-for", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXForwardedForHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-forwarded-for", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }

        [TestMethod]
        public void ExtractIpUsingXClientIpHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-client-ip", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXClientIpHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-client-ip", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }

        [TestMethod]
        public void ExtractIpUsingXRealIpHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-real-ip", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXRealIpHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-real-ip", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXForwardedHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-forwarded", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXForwardedHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-forwarded", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }

        [TestMethod]
        public void ExtractIpUsingXClusterClientIpHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-cluster-client-ip", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingXClusterClientIpHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-cluster-client-ip", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingForwardedForHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"forwarded-for", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingForwardedForHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"forwarded-for", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingForwardedHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"forwarded", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingForwardedHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"forwarded", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }

        [TestMethod]
        public void ExtractIpUsingViaHeaderIpv6()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"via", "f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("f71f:5bf9:25ff:1883:a8c4:eeff:7b80:aa2d", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingViaHeaderMultipleIp()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"via", "141.246.115.116, 203.0.113.1, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("141.246.115.116", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingPriorityWithXForwardedFor()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-forwarded-for", "203.0.113.1"},
                {"x-real-ip", "98.51.100.101"},
                {"x-client-ip", "198.51.100.102"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("203.0.113.1", clientIp);
        }
        
        [TestMethod]
        public void ExtractIpUsingPriorityWithoutXForwardedFor()
        {
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            
            var headers = new WebHeaderCollection
            {
                {"x-real-ip", "98.51.100.101"},
                {"x-client-ip", "203.0.113.1, 141.246.115.116, 12.34.56.3"}
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;

            var clientIp = RequestUtils.GetClientIpFromRequest((HttpWebRequest)request, options);
            
            Assert.AreEqual("203.0.113.1", clientIp);
        }
    }
}