namespace PAD.Client.Services;

public class SnackbarClientService : ISnackbarService
{
    private readonly ISnackbar _snackbar;

    public SnackbarClientService(ISnackbar snackbar) { _snackbar = snackbar; }

    public void Success(string message) => _snackbar.Add(message, Severity.Success);
}