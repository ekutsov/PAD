namespace PAD.Client.Services;

public interface IFinanceService
{
    Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState);

    Task<List<ExpenseCategory>> GetExpenseCategoriesAsync();

    Task CreateExpense(ExpenseDTO expense);
}