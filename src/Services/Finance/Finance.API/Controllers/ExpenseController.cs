namespace PFA.Finance.Controllers;

[Route("api/v1/expenses")]
public class ExpenseController : BaseController<IExpenseService>
{
    public ExpenseController(IExpenseService service) : base(service) { }

    [HttpGet]
    public async Task<ActionResult<TableViewModel<ExpenseViewModel>>> GetAll([FromQuery] TableStateDTO tableState)
    {
        TableViewModel<ExpenseViewModel> expenses = await _service.GetAllAsync(tableState);

        return Json(expenses);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ExpenseDTO expenseDTO)
    {
        await _service.CreateAsync(expenseDTO);

        return Ok();
    }
}