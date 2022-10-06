namespace PAD.Client.Services;

public interface IFinanceService
{
    Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState);

    Task<List<ExpenseCategory>> GetExpenseCategoriesAsync();

    Task<bool> CreateExpenseAsync(ExpenseDTO expense);

    Task<bool> UpdateExpenseAsync(Guid id, ExpenseDTO expense);

    Task<bool> DeleteExpenseAsync(Guid id);
}