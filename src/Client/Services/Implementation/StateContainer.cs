namespace PAD.Client.Services;

public class StateContainer
{
    public StateContainer(IJSRuntime jsRuntime)
    {
        TimezoneOffset = ((IJSInProcessRuntime)jsRuntime)
            .Invoke<int>("eval", "-new Date().getTimezoneOffset()");
    }
    public bool IsLoading { get; private set; }

    public int TimezoneOffset { get; private set; }

    public event Action OnChange;

    public void Change()
    {
        IsLoading = !IsLoading;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}