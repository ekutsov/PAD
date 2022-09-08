namespace PAD.Finance.Core.Services;

public interface IExpenseService
{
    Task<List<ExpenseViewModel>> GetAllAsync();

    Task<ExpenseViewModel> CreateAsync(ExpenseDTO expenseDTO);
}