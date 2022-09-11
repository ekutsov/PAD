namespace PAD.Finance.Core.Services;

public interface IExpenseService
{
    Task<TableViewModel<ExpenseViewModel>> GetAllAsync(TableStateDTO tableState, string userId);

    Task CreateAsync(ExpenseDTO expenseDTO, string userId);
}