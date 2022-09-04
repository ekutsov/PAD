using PAD.Identity.Core.Helpers;
using PAD.Identity.Domain.DTO;

namespace PAD.Identity.API.Controllers;

[Route("connect")]
public class AccountController : Controller
{
    private readonly IAuthService _service;

    public AccountController(IAuthService service)
    {
        _service = service;
    }

    [HttpGet("authorize")]
    [HttpPost("authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        AuthorizeDTO data = await _service.AuthorizeAsync();
        return Result(data);
    }

    [Authorize]
    [HttpPost("authorize")]
    [FormValueRequired("submit.Accept")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept()
    {
        AuthorizeDTO data = await _service.AcceptAsync();
        return Result(data);
    }

    [Authorize]
    [HttpPost("authorize")]
    [FormValueRequired("submit.Deny")]
    [ValidateAntiForgeryToken]
    public IActionResult Deny()
    {
        return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return View();
    }

    [ActionName(nameof(Logout))]
    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutPost()
    {
        AuthorizeDTO data = await _service.SignOutAsync();
        return Result(data);
    }

    [HttpPost("token")]
    [Produces("application/json")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Exchange()
    {
        AuthorizeDTO data = await _service.ExchanegAsync();
        return Result(data);
    }

    private IActionResult Result(AuthorizeDTO data)
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
            return SignIn(data.Identity, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        else
        {
            return View(data.ViewModel);
        }
    }
}
