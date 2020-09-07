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

        public Boolean GetOk()
        {
            return this.Ok;
        }

        public void SetOk(Boolean value)
        {
            this.Ok = value;
        }

        public int GetStatusCode()
        {
            return this.StatusCode;
        }

        public void SetStatusCode(int value)
        {
            this.StatusCode = value;
        }

        public string GetBody()
        {
            return this.Body;
        }

        public void SetBody(string value)
        {
            this.Body = value;
        }
    }
}
