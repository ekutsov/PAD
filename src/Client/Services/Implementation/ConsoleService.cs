using Microsoft.JSInterop;

namespace PAD.Client.Services;

public class ConsoleService : IConsoleService
{
    private readonly IJSRuntime _jsRuntime;
    public ConsoleService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public void Log(object obj)
    {
        ((IJSInProcessRuntime)_jsRuntime).Invoke<object>("console.log", obj);
    }
}