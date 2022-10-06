namespace PAD.Client.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T> GetItemAsync<T>(string key)
    {
        string content = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

        if (content == null)
            return default;

        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonConvert.SerializeObject(value));
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}