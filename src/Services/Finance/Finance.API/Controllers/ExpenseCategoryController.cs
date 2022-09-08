using Microsoft.AspNetCore.Mvc;
using PAD.Finance.Domain.ViewModels;

namespace PFA.Finance.Controllers;

[Route("api/v1/expenses/categories")]
public class ExpenseCategoryController : BaseController<IExpenseCategoryService>
{
    public ExpenseCategoryController(IExpenseCategoryService service) : base(service) { }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseCategoryViewModel>>> GetAll()
    {
        List<ExpenseCategoryViewModel> expenses = await _service.GetAllAsync();

        return Json(expenses);
    }
}