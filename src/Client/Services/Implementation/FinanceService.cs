namespace PAD.Client.Services;

public class FinanceService : HttpService, IFinanceService
{
    private const string BasePath = "finance/expenses/";
    public FinanceService(HttpClient client, StateContainer state) : base(client, state) { }

    public async Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState) =>
        await GetPagedCollectionAsync<Expense>(BasePath, tableState);

    public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync() =>
        await GetCollectionAsync<ExpenseCategory>(BasePath + "categories");

    public async Task CreateExpenseAsync(ExpenseDTO expense) =>
        await CreateAsync(BasePath, expense);

    public async Task UpdateExpenseAsync(Guid id, ExpenseDTO expense) =>
        await UpdateAsync(BasePath + id, expense);

    public async Task DeleteExpenseAsync(Guid id) =>
        await DeleteAsync(BasePath + id);
}