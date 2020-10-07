namespace SecureNative.SDK.Models
{
    public class RequestOptions
    {
        private string Url { get; set; }
        private string Body { get; set; }
        private bool Retry { get; set; }

        public RequestOptions(string url, string body, bool retry)
        {
            Url = url;
            Body = body;
            Retry = retry;
        }

        public string GetUrl()
        {
            return Url;
        }

        public void SetUrl(string value)
        {
            Url = value;
        }

        public string GetBody()
        {
            return Body;
        }

        public void SetBody(string value)
        {
            Body = value;
        }

        public bool GetRetry()
        {
            return Retry;
        }

        public void SetRetry(bool value)
        {
            Retry = value;
        }
    }
}
