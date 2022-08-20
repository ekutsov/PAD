using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace PFA.Server.Services;

public class AuthorizationService
{
    private readonly HttpContext _context;
    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task AuthorizeAsync()
    {
        OpenIddictRequest request = GetOpenIddictServerRequest();

        AuthenticateResult result = await _context.AuthenticateAsync(IdentityConstants.ApplicationScheme);
    }

    private OpenIddictRequest GetOpenIddictServerRequest()
    {
        OpenIddictRequest request = _context.GetOpenIddictServerRequest();

        if (request == null)
        {
            throw new InvalidOperationException(ErrorMessages.OidcRequestCantRetrieved);
        }

        return request;
    }
}