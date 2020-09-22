using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Context;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class SecureNativeContextBuilderTests
    {
        [TestMethod]
        public void CreateContextFromHttpServletRequestTest()
        {
            WebHeaderCollection headers = new WebHeaderCollection
            {
                { "x-securenative", "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a" },
                { "REMOTE_ADDR", "51.68.201.122" }
            };

            Uri uri = new Uri("www.securenative.com/login");
            WebRequest request = WebRequest.Create(uri);
            request.Method = "Post";
            request.Headers = headers;

            SecureNativeContext context = SecureNativeContextBuilder.FromHttpRequest((HttpWebRequest)request).Build();

            Assert.AreEqual(context.GetClientToken(), "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a");
            Assert.AreEqual(context.GetIp(), "51.68.201.122");
            Assert.AreEqual(context.GetMethod(), "Post");
            Assert.AreEqual(context.GetUrl(), "/login");
            Assert.AreEqual(context.GetRemoteIp(), "51.68.201.122");
            Assert.AreEqual(context.GetHeaders(), new Dictionary<string, string>() { { "x-securenative", "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a" } });
            Assert.IsNull(context.GetBody());
        }

        [TestMethod]
        public void CreateContextFromHttpServletRequestWithCookieTest()
        {
            WebHeaderCollection headers = new WebHeaderCollection
            {
                { "_sn", "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a" },
                { "REMOTE_ADDR", "51.68.201.122" }
            };

            Uri uri = new Uri("www.securenative.com/login");
            WebRequest request = WebRequest.Create(uri);
            request.Method = "Post";
            request.Headers = headers;

            SecureNativeContext context = SecureNativeContextBuilder.FromHttpRequest((HttpWebRequest)request).Build();

            Assert.AreEqual(context.GetClientToken(), "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a");
            Assert.AreEqual(context.GetIp(), "51.68.201.122");
            Assert.AreEqual(context.GetMethod(), "Post");
            Assert.AreEqual(context.GetUrl(), "/login");
            Assert.AreEqual(context.GetRemoteIp(), "51.68.201.122");
            Assert.AreEqual(context.GetHeaders(), new Dictionary<string, string>() { { "x-securenative", "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a" } });
            Assert.IsNull(context.GetBody());
        }

        [TestMethod]
        public void CreateDefaultContextBuilderTest()
        {
            SecureNativeContext context = SecureNativeContextBuilder.DefaultContextBuilder().Build();

            Assert.IsNull(context.GetClientToken());
            Assert.IsNull(context.GetIp());
            Assert.IsNull(context.GetMethod());
            Assert.IsNull(context.GetUrl());
            Assert.IsNull(context.GetRemoteIp());
            Assert.IsNull(context.GetHeaders());
            Assert.IsNull(context.GetBody());
        }

        [TestMethod]
        public void CreateCustomContextWithContextBuilderTest()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "_sn", "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a" },
                { "REMOTE_ADDR", "51.68.201.122" }
            };

            SecureNativeContext context = SecureNativeContextBuilder
                    .DefaultContextBuilder()
                    .WithUrl("/some-url")
                    .WithClientToken("SECRET_TOKEN")
                    .WithIp("10.0.0.0")
                    .WithBody("{ \"name\": \"YOUR_NAME\" }")
                    .WithMethod("Get")
                    .WithRemoteIp("10.0.0.1")
                    .WithHeaders(headers)
                    .Build();

            Assert.AreEqual(context.GetUrl(), "/some-url");
            Assert.AreEqual(context.GetClientToken(), "SECRET_TOKEN");
            Assert.AreEqual(context.GetIp(), "10.0.0.0");
            Assert.AreEqual(context.GetBody(), "{ \"name\": \"YOUR_NAME\" }");
            Assert.AreEqual(context.GetMethod(), "Get");
            Assert.AreEqual(context.GetRemoteIp(), "10.0.0.1");
            Assert.AreEqual(context.GetHeaders(), headers);
        }
    }
}
