namespace SecureNative.SDK.Http
{
    public interface IHttpClient
    {
        HttpResponse Post(string url, string body);
    }
}
