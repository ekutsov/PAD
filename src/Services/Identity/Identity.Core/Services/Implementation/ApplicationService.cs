using OpenIddict.Abstractions;

namespace PAD.Identity.Core.Services;

public class ApplicationService : IApplicationService
{
    private readonly IOpenIddictApplicationManager _applicationManager;

    public ApplicationService(IOpenIddictApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }

    public async Task<string> GetIdAsync(object application)
    {
        string applicationId = await _applicationManager.GetIdAsync(application);

        return applicationId;
    }

    public async Task<string> GetConsentTypeAsync(object application)
    {
        string consentType = await _applicationManager.GetConsentTypeAsync(application);

        return consentType;
    }

    public async Task<object> FindByClientIdAsync(string clientId)
    {
        object application = await _applicationManager.FindByClientIdAsync(clientId);

        if (application == null)
        {
            throw new InvalidOperationException("Details concerning the calling client application cannot be found.");
        }

        return application;
    }

    public async Task<string> GetLocalizedDisplayNameAsync(object application)
    {
        string appName = await _applicationManager.GetLocalizedDisplayNameAsync(application);

        return appName;
    }

    public async Task<bool> HasConsentTypeAsync(object application, string consentType)
    {
        bool result = await _applicationManager.HasConsentTypeAsync(application, consentType);

        return result;
    }
}