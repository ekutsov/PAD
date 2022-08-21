namespace PFA.Server.Controllers;

[Route("connect")]
public class AuthorizationController : BaseController<IAuthService>
{
    public AuthorizationController(IAuthService service) : base(service) { }

    [HttpGet("authorize"), HttpPost("authorize"), IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        AuthorizeDTO data = await _service.AuthorizeAsync();
        return Result(data);
    }

    [Authorize, FormValueRequired("submit.Accept"), HttpPost("authorize"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept()
    {
        AuthorizeDTO data = await _service.AcceptAsync();
        return Result(data);
    }

    [Authorize, FormValueRequired("submit.Deny"), HttpPost("authorize"), ValidateAntiForgeryToken]
    public IActionResult Deny()
    {
        return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return View();
    }

    [ActionName(nameof(Logout)), HttpPost("logout"), ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutPost()
    {
        AuthorizeDTO data = await _service.SignOutAsync();
        return Result(data);
    }

    [HttpPost("token"), IgnoreAntiforgeryToken, Produces("application/json")]
    public async Task<IActionResult> Exchange()
    {
        AuthorizeDTO data = await _service.ExchanegAsync();
        return Result(data);
    }
}
