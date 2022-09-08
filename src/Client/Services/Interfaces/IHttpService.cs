using MudBlazor;

namespace PAD.Client.Services;

public interface IHttpService
{
    Task<TableData<T>> GetCollectionAsync<T>(string path, Dictionary<string, string> queryParams);
}