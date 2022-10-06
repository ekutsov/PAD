namespace PAD.Client.Services;

public interface ISnackbarService
{
    void Success(string message);

    void Warning(string message);

    void Error(string message);
}