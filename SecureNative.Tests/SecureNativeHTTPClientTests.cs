using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using SecureNative.SDK.Config;
using SecureNative.SDK.Http;

namespace SecureNative.Tests
{
    [TestClass]
    public class SecureNativeHttpClientTests
    {
        [TestMethod]
        public void ShouldMakeSimplePostCallTest()
        {
            var options = ConfigurationManager.ConfigBuilder().WithApiKey("YOUR_API_KEY").WithApiUrl("http://localhost/api").Build();
            const string payload = "{\"event\":\"SOME_EVENT_NAME\"}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost/api/*").Respond(HttpStatusCode.OK, "application/json", "{'SOME_BODY': 'BODY'}");
            var client = new SecureNativeHttpClient(options, mockHttp);
            var response = client.Post("track", payload);

            Assert.AreEqual(true, response.IsOk());
            Assert.AreEqual(200, response.GetStatusCode());
        }
    }
}
