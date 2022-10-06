namespace PAD.Client.Services;

public class FinanceService : HttpService, IFinanceService
{
    private const string BasePath = "finance/expenses/";

    public FinanceService(HttpClient client,
                          ISnackbarService snackbar,
                          StateContainer state) : base(client, snackbar, state) { }

    public async Task<TableData<Expense>> GetPagedExpensesAsync(TableStateDTO tableState) =>
        await GetPagedCollectionAsync<Expense>(BasePath, tableState);

    public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync() =>
        await GetCollectionAsync<ExpenseCategory>(BasePath + "categories");

    public async Task<bool> CreateExpenseAsync(ExpenseDTO expense) =>
        await CreateAsync(BasePath, expense);

    public async Task<bool> UpdateExpenseAsync(Guid id, ExpenseDTO expense) =>
        await UpdateAsync(BasePath + id, expense);

    public async Task<bool> DeleteExpenseAsync(Guid id) =>
        await DeleteAsync(BasePath + id);
}