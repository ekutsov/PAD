namespace PFA.Server.Controllers;

public abstract class BaseController<TService> : Controller
{
    protected readonly TService _service;

    protected BaseController(TService service)
    {
        _service = service;
    }

    protected IActionResult Result(AuthorizeDTO data)
    {
        if (data.Error == Errors.LoginRequired || data.Error == Errors.ConsentRequired)
        {
            return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = data.Error,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = data.ErrorDescription
                    }));
        }
        else if (!string.IsNullOrWhiteSpace(data.RedirectUri) && data.RedirectUri != "/")
        {
            return Challenge(
               authenticationSchemes: IdentityConstants.ApplicationScheme,
               properties: new AuthenticationProperties
               {
                   RedirectUri = data.RedirectUri
               });
        }
        else if (!string.IsNullOrWhiteSpace(data.RedirectUri) && data.RedirectUri == "/")
        {
            return SignOut(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties
            {
                RedirectUri = data.RedirectUri
            });
        }
        else if (data.Identity != default)
        {
            return SignIn(new ClaimsPrincipal(data.Identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        else
        {
            return View(data.ViewModel);
        }
    }
}
