namespace PAD.Client.Services;

public interface IFinanceService
{
    Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState);

    Task<List<ExpenseCategory>> GetExpenseCategoriesAsync();

    Task CreateExpenseAsync(ExpenseDTO expense);

    Task UpdateExpenseAsync(Guid id, ExpenseDTO expense);

    Task DeleteExpenseAsync(Guid id);
}