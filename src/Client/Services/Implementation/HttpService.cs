using System.Net.Http.Json;
using System.Reflection;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;

namespace PAD.Client.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _client;
    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<TableData<T>> GetCollectionAsync<T>(string path, Dictionary<string, string> queryParams) =>
        await _client.GetFromJsonAsync<TableData<T>>(QueryHelpers.AddQueryString(path, queryParams));
}