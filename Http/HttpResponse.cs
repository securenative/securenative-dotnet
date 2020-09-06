using System;
namespace SecureNative.SDK.Http
{
    public class HttpResponse
    {
        private Boolean Ok { get; set; }
        private int StatusCode { get; set; }
        private string Body { get; set; }

        public HttpResponse(Boolean ok, int statusCode, string body)
        {
            this.Ok = ok;
            this.StatusCode = statusCode;
            this.Body = body;
        }
    }
}
