using Microsoft.AspNetCore.Mvc;
using PAD.Finance.Domain.DTO;
using PAD.Finance.Domain.ViewModels;

namespace PFA.Finance.Controllers;

[Route("api/v1/expenses")]
public class ExpenseController : BaseController<IExpenseService>
{
    public ExpenseController(IExpenseService service) : base(service) { }
    
    [HttpGet]
    public async Task<ActionResult<List<ExpenseViewModel>>> GetAll()
    {
        List<ExpenseViewModel> expenses = await _service.GetAllAsync();

        return Json(expenses);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseViewModel>> Create([FromBody] ExpenseDTO expenseDTO)
    {
        ExpenseViewModel createdExpenseView = await _service.CreateAsync(expenseDTO);

        return Json(createdExpenseView);
    }
}