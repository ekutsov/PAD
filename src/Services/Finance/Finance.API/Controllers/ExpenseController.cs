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
    /// <param name="service">IExpenseService</param>
    /// <returns></returns>
    public ExpenseController(IExpenseService service) : base(service) { }

    /// <summary>
    /// User identifier
    /// </summary>
    private Guid UserId => Guid.Parse(User.FindFirstValue("sub"));

    /// <summary>
    /// Get user expenses
    /// </summary>
    /// <param name="tableState">Table params</param>
    /// <returns>Paged expenses and total value</returns>
    [HttpGet]
    public async Task<ActionResult<Response<TableViewModel<ExpenseViewModel>>>> GetPaged([FromQuery] TableStateDTO tableState)
    {
        TableViewModel<ExpenseViewModel> expenses = await _service.GetPagedAsync(tableState, UserId);

        return ResponseResult(expenses);
    }

    /// <summary>
    /// Create user expense
    /// </summary>
    /// <param name="expenseDTO">expense data</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Response<bool>>> Create([FromBody] ExpenseDTO expenseDTO)
    {
        await _service.CreateAsync(expenseDTO, UserId);

        return SuccessResult();
    }

    /// <summary>
    /// Update user expense
    /// </summary>
    /// <param name="id">expense id</param>
    /// <param name="expenseDTO">expense data</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Response<bool>>> Update([FromRoute] Guid id, [FromBody] ExpenseDTO expenseDTO)
    {
        await _service.UpdateAsync(id, expenseDTO, UserId);

        return SuccessResult();
    }

    /// <summary>
    /// Delete user expense
    /// </summary>
    /// <param name="id">expense id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        await _service.DeleteAsync(id, UserId);

        return SuccessResult();
    }
}