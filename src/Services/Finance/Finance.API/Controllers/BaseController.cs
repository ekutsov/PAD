namespace PFA.Finance.Controllers;

[ValidateModel]
public abstract class BaseController<TService> : Controller
{
    protected readonly TService _service;
    protected BaseController(TService service)
    {
        _service = service;
    }

    public string UserId => User.FindFirstValue("sub");
}