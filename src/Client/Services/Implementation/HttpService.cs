using System.Net.Http.Json;

namespace PAD.Client.Services;

public abstract class HttpService
{
    private readonly HttpClient _client;

    protected HttpService(HttpClient client)
    {
        _client = client;
    }

    protected async Task<TableData<T>> GetPagedCollectionAsync<T>(string path, object queryModel) =>
        await _client.GetFromJsonAsync<TableData<T>>(path.AddQueryString(queryModel));

    protected async Task<List<T>> GetCollectionAsync<T>(string path) =>
        await _client.GetFromJsonAsync<List<T>>(path);

    protected async Task CreateAsync<TValue>(string path, TValue model) =>
        await _client.PostAsJsonAsync(path, model);
}