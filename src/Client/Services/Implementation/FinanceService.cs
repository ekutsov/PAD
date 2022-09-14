namespace PAD.Client.Services;

public class FinanceService : HttpService, IFinanceService
{
    private const string BasePath = "finance/expenses/";
    public FinanceService(HttpClient client) : base(client) { }

    public async Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState)
    {
        TableData<Expense> expenses = await GetPagedCollectionAsync<Expense>(BasePath, tableState);

        return expenses;
    }

    public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
    {
        List<ExpenseCategory> expenseCategories = await GetCollectionAsync<ExpenseCategory>(BasePath + "categories");

        return expenseCategories;
    }

    public async Task CreateExpenseAsync(ExpenseDTO expense) =>
        await CreateAsync(BasePath, expense);

    public async Task UpdateExpenseAsync(Guid id, ExpenseDTO expense) =>
        await UpdateAsync(BasePath + id, expense);

    public async Task DeleteExpenseAsync(Guid id) =>
        await DeleteAsync(BasePath + id);
}