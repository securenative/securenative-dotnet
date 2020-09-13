using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using SecureNative.SDK.Config;
using SecureNative.SDK.Http;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class SecureNativeHTTPClientTests
    {
        [TestMethod]
        public void ShouldMakeSimplePostCallTest()
        {
            var options = ConfigurationManager.ConfigBuilder().WithApiKey("YOUR_API_KEY").Build();
            string payload = "{\"event\":\"SOME_EVENT_NAME\"}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localost/api/user/*").Respond(HttpStatusCode.OK, "application/json", "{'SOME_BODY': 'BODY'}");
            SecureNativeHTTPClient client = new SecureNativeHTTPClient(options);
            HttpResponse response = client.Post("track", payload);

            Assert.Equals(response.IsOk(), true);
            Assert.Equals(response.GetStatusCode(), 200);
            Assert.Equals(response.GetBody(), "{'SOME_BODY': 'BODY'}");
        }
    }
}
