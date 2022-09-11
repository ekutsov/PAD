namespace PFA.Finance.Controllers;

[ValidateModel]
[ApiController]
public abstract class BaseController<TService> : Controller
{
    protected readonly TService _service;
    protected BaseController(TService service)
    {
        _service = service;
    }

    protected Guid UserId
    {
        get
        {
            return Guid.Parse(User.FindFirstValue("sub"));
        }
    }
}