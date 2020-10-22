using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Context;

namespace SecureNative.Tests
{
    [TestClass]
    public class SecureNativeContextBuilderTests
    {
        [TestMethod]
        public void CreateContextFromHttpServletRequestTest()
        {
            var headers = new WebHeaderCollection
            {
                {
                    "x-securenative",
                    "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a"
                },
                {"x-forwarded-for", "51.68.201.122"}
            };

            var expectedHeaders = new Dictionary<string, string>()
            {
                {
                    "x-securenative",
                    "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a"
                }
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Method = "Post";
            request.Headers = headers;

            var context = SDK.Client.FromHttpRequest((HttpWebRequest) request).Build();

            Assert.AreEqual(
                "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a",
                context.GetClientToken());
            Assert.AreEqual("51.68.201.122", context.GetIp());
            Assert.AreEqual("Post", context.GetMethod());
            Assert.AreEqual(uri, context.GetUrl());
            Assert.AreEqual("51.68.201.122", context.GetRemoteIp());
            Assert.AreEqual(expectedHeaders["x-securenative"], context.GetHeaders()["x-securenative"]);
            Assert.IsNull(context.GetBody());
        }

        [TestMethod]
        public void CreateContextFromHttpServletRequestWithCookieTest()
        {
            var headers = new WebHeaderCollection
            {
                {
                    "_sn",
                    "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a"
                },
                {"x-forwarded-for", "51.68.201.122"}
            };

            var expectedHeaders = new Dictionary<string, string>()
            {
                {
                    "_sn",
                    "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a"
                }
            };

            var uri = new Uri("http://www.securenative.com/login");
            var request = WebRequest.Create(uri);
            request.Method = "Post";
            request.Headers = headers;

            var context = SDK.Client.FromHttpRequest((HttpWebRequest) request).Build();

            Assert.AreEqual(
                "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a",
                context.GetClientToken());
            Assert.AreEqual("51.68.201.122", context.GetIp());
            Assert.AreEqual("Post", context.GetMethod());
            Assert.AreEqual(uri, context.GetUrl());
            Assert.AreEqual("51.68.201.122", context.GetRemoteIp());
            Assert.AreEqual(expectedHeaders["_sn"], context.GetHeaders()["_sn"]);
            Assert.IsNull(context.GetBody());
        }

        [TestMethod]
        public void CreateDefaultContextBuilderTest()
        {
            var context = SecureNativeContextBuilder.DefaultContextBuilder().Build();

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
            var headers = new Dictionary<string, string>()
            {
                {
                    "_sn",
                    "71532c1fad2c7f56118f7969e401f3cf080239140d208e7934e6a530818c37e544a0c2330a487bcc6fe4f662a57f265a3ed9f37871e80529128a5e4f2ca02db0fb975ded401398f698f19bb0cafd68a239c6caff99f6f105286ab695eaf3477365bdef524f5d70d9be1d1d474506b433aed05d7ed9a435eeca357de57817b37c638b6bb417ffb101eaf856987615a77a"
                },
                {"REMOTE_ADDR", "51.68.201.122"}
            };

            var context = SecureNativeContextBuilder
                .DefaultContextBuilder()
                .WithUrl("/some-url")
                .WithClientToken("SECRET_TOKEN")
                .WithIp("10.0.0.0")
                .WithBody("{ \"name\": \"YOUR_NAME\" }")
                .WithMethod("Get")
                .WithRemoteIp("10.0.0.1")
                .WithHeaders(headers)
                .Build();

            Assert.AreEqual("/some-url", context.GetUrl());
            Assert.AreEqual("SECRET_TOKEN", context.GetClientToken());
            Assert.AreEqual("10.0.0.0", context.GetIp());
            Assert.AreEqual("{ \"name\": \"YOUR_NAME\" }", context.GetBody());
            Assert.AreEqual("Get", context.GetMethod());
            Assert.AreEqual("10.0.0.1", context.GetRemoteIp());
            Assert.AreEqual(headers, context.GetHeaders());
        }
    }
}