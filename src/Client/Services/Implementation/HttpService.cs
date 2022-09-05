namespace PAD.Client.Services;

public class HttpService
{
    private readonly HttpClient _client;
    public HttpService(HttpClient client)
    {
        _client = client;
    }
}