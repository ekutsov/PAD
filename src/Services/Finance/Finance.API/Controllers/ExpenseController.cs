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
    /// <param name="service">IExpense service</param>
    /// <returns></returns>
    public ExpenseController(IExpenseService service) : base(service) { }

    /// <summary>
    /// Get user expenses
    /// </summary>
    /// <param name="tableState">Table params</param>
    /// <returns>Paged expenses and total value</returns>
    [HttpGet]
    public async Task<ActionResult<TableViewModel<ExpenseViewModel>>> GetPaged([FromQuery] TableStateDTO tableState)
    {
        TableViewModel<ExpenseViewModel> expenses = await _service.GetPagedAsync(tableState, UserId);

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

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ExpenseDTO expenseDTO)
    {
        await _service.UpdateAsync(id, expenseDTO, UserId);

        return Ok();
    }
}