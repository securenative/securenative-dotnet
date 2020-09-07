using System;
namespace SecureNative.SDK.Models
{
    public class RequestOptions
    {
        private string Url { get; set; }
        private string Body { get; set; }
        private Boolean Retry { get; set; }

        public RequestOptions(string url, string body, Boolean retry)
        {
            this.Url = url;
            this.Body = body;
            this.Retry = retry;
        }

        public string GetUrl()
        {
            return this.Url;
        }

        public void SetUrl(string value)
        {
            this.Url = value;
        }

        public string GetBody()
        {
            return this.Body;
        }

        public void SetBody(string value)
        {
            this.Body = value;
        }

        public Boolean GetRetry()
        {
            return this.Retry;
        }

        public void SetRetry(Boolean value)
        {
            this.Retry = value;
        }
    }
}
