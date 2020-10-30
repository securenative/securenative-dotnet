namespace SecureNative.SDK.Http
{
    public class HttpResponse
    {
        private bool Ok { get; set; }
        private int StatusCode { get; set; }
        private string Body { get; set; }

        public HttpResponse(bool ok, int statusCode, string body)
        {
            Ok = ok;
            StatusCode = statusCode;
            Body = body;
        }

        public bool IsOk()
        {
            return Ok;
        }

        public void SetOk(bool value)
        {
            Ok = value;
        }

        public int GetStatusCode()
        {
            return StatusCode;
        }

        public void SetStatusCode(int value)
        {
            StatusCode = value;
        }

        public string GetBody()
        {
            return Body;
        }

        public void SetBody(string value)
        {
            Body = value;
        }
    }
}
