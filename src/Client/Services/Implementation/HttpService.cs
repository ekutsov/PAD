namespace PAD.Client.Services;

public abstract class HttpService
{
    private readonly HttpClient _client;

    private readonly ISnackbarService _snackbar;

    private readonly StateContainer _state;

    protected HttpService(HttpClient client, ISnackbarService snackbar, StateContainer state)
    {
        _client = client;
        _snackbar = snackbar;
        _state = state;
    }

    protected async Task<TableData<T>> GetPagedCollectionAsync<T>(string path, object queryModel)
    {
        RequestDTO request = new() { Path = QueryExtensions.AddQueryString(path, queryModel), Method = HttpMethod.Get };

        TableData<T> pagedCollection = await SendRequest<TableData<T>>(request);

        return pagedCollection;
    }

    protected async Task<List<T>> GetCollectionAsync<T>(string path)
    {
        RequestDTO request = new() { Path = path, Method = HttpMethod.Get };

        List<T> collection = await SendRequest<List<T>>(request);

        return collection;
    }


    protected async Task<bool> CreateAsync<TBody>(string path, TBody model)
    {
        RequestDTO request = new() { Path = path, Method = HttpMethod.Post, Body = model };

        bool successResult = await SendRequest<bool>(request);

        return successResult;
    }


    protected async Task<bool> UpdateAsync<TBody>(string path, TBody model)
    {
        RequestDTO request = new() { Path = path, Method = HttpMethod.Put, Body = model };

        bool successResult = await SendRequest<bool>(request);

        return successResult;
    }

    protected async Task<bool> DeleteAsync(string path)
    {
        RequestDTO request = new() { Path = path, Method = HttpMethod.Delete };

        bool successResult = await SendRequest<bool>(request);

        return successResult;
    }

    private async Task<T> SendRequest<T>(RequestDTO request)
    {
        _state.Change();

        HttpRequestMessage requestMessage = new(request.Method, request.Path);

        if (request.Body != null)
        {
            string jsonBody = JsonConvert.SerializeObject(request.Body);

            requestMessage.Content = new StringContent(jsonBody, System.Text.Encoding.Default, "application/json");
        }

        HttpResponseMessage response = await _client.SendAsync(requestMessage);

        string content = await response.Content.ReadAsStringAsync();

        Response<T> result = JsonConvert.DeserializeObject<Response<T>>(content);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                _snackbar.Error("Something went wrong");
            }
            else
            {
                _snackbar.Error(result.UserMessage);
            }
        }

        _state.Change();

        return result.Data;
    }
}