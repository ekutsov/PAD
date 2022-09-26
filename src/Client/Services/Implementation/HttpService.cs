namespace PAD.Client.Services;

public abstract class HttpService
{
    private readonly HttpClient _client;

    private readonly StateContainer _state;

    protected HttpService(HttpClient client, StateContainer state)
    {
        _client = client;
        _state = state;
    }

    protected async Task<TableData<T>> GetPagedCollectionAsync<T>(string path, object queryModel)
    {
        _state.Change();

        TableData<T> pagedCollection = await _client.GetFromJsonAsync<TableData<T>>(path.AddQueryString(queryModel));

        _state.Change();

        return pagedCollection;
    }

    protected async Task<List<T>> GetCollectionAsync<T>(string path) =>
        await _client.GetFromJsonAsync<List<T>>(path);

    protected async Task CreateAsync<TValue>(string path, TValue model) =>
        await _client.PostAsJsonAsync(path, model);

    protected async Task UpdateAsync<TValue>(string path, TValue model) =>
        await _client.PutAsJsonAsync(path, model);

    protected async Task DeleteAsync(string path) =>
        await _client.DeleteAsync(path);
}