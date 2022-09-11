namespace PAD.Client.Services;

public class FinanceService : HttpService, IFinanceService
{
    public FinanceService(HttpClient client) : base(client) { }

    public async Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState)
    {
        TableData<Expense> expenses = await GetPagedCollectionAsync<Expense>("finance/expenses", tableState);

        return expenses;
    }

    public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
    {
        List<ExpenseCategory> expenseCategories = await GetCollectionAsync<ExpenseCategory>("finance/expenses/categories");

        return expenseCategories;
    }

    public async Task CreateExpense(ExpenseDTO expense) =>
        await CreateAsync("finance/expenses", expense);
}