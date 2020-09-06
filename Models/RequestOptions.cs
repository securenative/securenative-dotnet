using System;
namespace SecureNative.SDK.Models
{
    public class RequestOptions
    {
        public string Url { get; set; }
        public string Body { get; set; }
        public Boolean Retry { get; set; }

        public RequestOptions(string url, string body, Boolean retry)
        {
            this.Url = url;
            this.Body = body;
            this.Retry = retry;
        }
    }
}
