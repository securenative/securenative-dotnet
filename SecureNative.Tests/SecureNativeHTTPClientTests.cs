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
            var options = ConfigurationManager.ConfigBuilder().WithApiKey("YOUR_API_KEY").WithApiUrl("http://localhost/api").Build();
            string payload = "{\"event\":\"SOME_EVENT_NAME\"}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost/api/*").Respond(HttpStatusCode.OK, "application/json", "{'SOME_BODY': 'BODY'}");
            SecureNativeHttpClient client = new SecureNativeHttpClient(options, mockHttp);
            HttpResponse response = client.Post("track", payload);

            Assert.AreEqual(true, response.IsOk());
            Assert.AreEqual(200, response.GetStatusCode());
        }
    }
}
