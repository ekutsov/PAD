namespace PFA.Finance.Controllers;

/// <summary>
/// Expense categories controller
/// </summary>
[Route("api/v1/expenses/categories")]
public class ExpenseCategoryController : BaseController<IExpenseCategoryService>
{
    /// <summary>
    /// Expense categories controller with IExpenseCategoryService injection
    /// </summary>
    /// <param name="service">IExpenseCategoryService</param>
    /// <returns></returns>
    public ExpenseCategoryController(IExpenseCategoryService service) : base(service) { }

    /// <summary>
    /// Get expense categories
    /// </summary>
    /// <returns>Collection of expense categories</returns>
    [HttpGet]
    public async Task<ActionResult<Response<List<ExpenseCategoryViewModel>>>> GetAll()
    {
        List<ExpenseCategoryViewModel> expenses = await _service.GetAllAsync();

        return ResponseResult(expenses);
    }
}