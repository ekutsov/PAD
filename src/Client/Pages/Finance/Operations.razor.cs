namespace PAD.Client.Finance;

[Authorize]
[Route("finance/operations")]
public partial class Operations
{
    [Inject] private IFinanceService FinanceService { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    private HashSet<Expense> selectedExpenses = new HashSet<Expense>();

    private MudTable<Expense> _table;

    private string _searchString = String.Empty;

    private DateRange _dateRange = new DateRange(DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

    private async Task<TableData<Expense>> ServerReload(TableState state) =>
        await FinanceService.GetPagedExpensesAsync(new TableStateDTO(_searchString, _dateRange, state));

    private void OnSearchStringChanged() => _table.ReloadServerData();

    private void OnDateRangeChange(DateRange dateRange)
    {
        _dateRange = dateRange;
        _table.ReloadServerData();
    }

    private async Task CreateExpense()
    {
        DialogOptions dialogOptions = new()
        {
            CloseButton = true,
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium
        };

        IDialogReference addExpenseDialog = DialogService.Show<AddExpenseDialog>("Create expense", dialogOptions);

        DialogResult result = await addExpenseDialog.Result;

        if (!result.Cancelled)
        {
            await _table.ReloadServerData();
        }
    }
}