namespace PFA.Finance.Controllers;

/// <summary>
/// Expense controller
/// </summary>
[Route("api/v1/expenses")]
public class ExpenseController : BaseController<IExpenseService>
{
    /// <summary>
    /// Expense controller with IExpenseService injection
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public ExpenseController(IExpenseService service) : base(service) { }

    /// <summary>
    /// Get user expenses
    /// </summary>
    /// <param name="tableState">Table state data</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<TableViewModel<ExpenseViewModel>>> GetAll([FromQuery] TableStateDTO tableState)
    {
        TableViewModel<ExpenseViewModel> expenses = await _service.GetAllAsync(tableState, UserId);

        return Json(expenses);
    }

    /// <summary>
    /// Create user expense
    /// </summary>
    /// <param name="expenseDTO">expense data</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ExpenseDTO expenseDTO)
    {
        await _service.CreateAsync(expenseDTO, UserId);

        return Ok();
    }
}