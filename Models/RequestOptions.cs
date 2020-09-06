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
    }
}
