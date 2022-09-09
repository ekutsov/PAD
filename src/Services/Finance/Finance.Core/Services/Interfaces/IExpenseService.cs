namespace PAD.Finance.Core.Services;

public interface IExpenseService
{
    Task<TableViewModel<ExpenseViewModel>> GetAllAsync(TableStateDTO tableState);

    Task CreateAsync(ExpenseDTO expenseDTO);
}