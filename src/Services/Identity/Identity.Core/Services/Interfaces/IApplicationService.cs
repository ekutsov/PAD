namespace PAD.Identity.Core.Services;

public interface IApplicationService
{
    Task<string> GetIdAsync(object application);

    Task<string> GetConsentTypeAsync(object application);

    Task<object> FindByClientIdAsync(string clientId);

    Task<string> GetLocalizedDisplayNameAsync(object application);

    Task<bool> HasConsentTypeAsync(object application, string consentType);
}