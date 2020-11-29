using System;
using System.Linq;
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

        [TestMethod]
        public void ExtractPiiDataFromHeaders()
        {
            var headers = new WebHeaderCollection
            {
                {"Host", "net.example.com"},
                {"User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5 (.NET CLR 3.5.30729)"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
                {"Accept-Language", "en-us,en;q=0.5"},
                {"Accept-Encoding", "gzip,deflate"},
                {"Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7"},
                {"Keep-Alive", "300"},
                {"Connection", "keep-alive"},
                {"Cookie", "PHPSESSID=r2t5uvjq435r4q7ib3vtdjq120"},
                {"Pragma", "no-cache"},
                {"Cache-Control", "no-cache"},
                {"authorization", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"access_token", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"apikey", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"password", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"passwd", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"secret", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"api_key", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},  
            };
            
            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;
            
            var h = RequestUtils.GetHeadersFromRequest((HttpWebRequest)request, null);

            Assert.IsFalse(h.Keys.Contains("authorization"));
            Assert.IsFalse(h.Keys.Contains("access_token"));
            Assert.IsFalse(h.Keys.Contains("apikey"));
            Assert.IsFalse(h.Keys.Contains("password"));
            Assert.IsFalse(h.Keys.Contains("passwd"));
            Assert.IsFalse(h.Keys.Contains("secret"));
            Assert.IsFalse(h.Keys.Contains("api_key"));
        }
        
        [TestMethod]
        public void ExtractPiiDataFromCustomHeaders()
        {
            var headers = new WebHeaderCollection
            {
                {"Host", "net.example.com"},
                {"User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5 (.NET CLR 3.5.30729)"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
                {"Accept-Language", "en-us,en;q=0.5"},
                {"Accept-Encoding", "gzip,deflate"},
                {"Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7"},
                {"Keep-Alive", "300"},
                {"Connection", "keep-alive"},
                {"Cookie", "PHPSESSID=r2t5uvjq435r4q7ib3vtdjq120"},
                {"Pragma", "no-cache"},
                {"Cache-Control", "no-cache"},
                {"authorization", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"access_token", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"apikey", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"password", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"passwd", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"secret", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"api_key", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},  
            };
            
            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;
            
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            options.SetPiiHeaders(new[] {"authorization", "access_token", "apikey", "password", "passwd", "secret", "api_key"});
            var h = RequestUtils.GetHeadersFromRequest((HttpWebRequest)request, options);

            Assert.IsFalse(h.Keys.Contains("authorization"));
            Assert.IsFalse(h.Keys.Contains("access_token"));
            Assert.IsFalse(h.Keys.Contains("apikey"));
            Assert.IsFalse(h.Keys.Contains("password"));
            Assert.IsFalse(h.Keys.Contains("passwd"));
            Assert.IsFalse(h.Keys.Contains("secret"));
            Assert.IsFalse(h.Keys.Contains("api_key"));
        }
        
        [TestMethod]
        public void ExtractPiiDataFromRegexPattern()
        {
            var headers = new WebHeaderCollection
            {
                {"Host", "net.example.com"},
                {"User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5 (.NET CLR 3.5.30729)"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
                {"Accept-Language", "en-us,en;q=0.5"},
                {"Accept-Encoding", "gzip,deflate"},
                {"Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7"},
                {"Keep-Alive", "300"},
                {"Connection", "keep-alive"},
                {"Cookie", "PHPSESSID=r2t5uvjq435r4q7ib3vtdjq120"},
                {"Pragma", "no-cache"},
                {"Cache-Control", "no-cache"},
                {"http_auth_authorization", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_access_token", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_apikey", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_password", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_passwd", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_secret", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},
                {"http_auth_api_key", "ylSkZIjbdWybfs4fUQe9BqP0LH5Z"},  
            };
            
            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Headers = headers;
            
            var options = SecureNativeConfigurationBuilder.DefaultConfigBuilder().Build();
            options.SetPiiRegexPattern(@"((?i)(http_auth_)(\w+)?)");
            var h = RequestUtils.GetHeadersFromRequest((HttpWebRequest)request, options);

            Assert.IsFalse(h.Keys.Contains("http_auth_authorization"));
            Assert.IsFalse(h.Keys.Contains("http_auth_access_token"));
            Assert.IsFalse(h.Keys.Contains("http_auth_apikey"));
            Assert.IsFalse(h.Keys.Contains("http_auth_password"));
            Assert.IsFalse(h.Keys.Contains("http_auth_passwd"));
            Assert.IsFalse(h.Keys.Contains("http_auth_secret"));
            Assert.IsFalse(h.Keys.Contains("http_auth_api_key"));
        }
    }
}