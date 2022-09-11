namespace PAD.Client.Finance;

[Authorize]
[Route("finance/operations")]
public partial class Operations
{
    [Inject] private IFinanceService FinanceService { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    private Expense _selectedExpense = null;

    private int _selectedRowNumber = -1;

    private MudTable<Expense> _table;

    private string _searchString = String.Empty;

    private DateRange _dateRange = new DateRange(DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

    private DialogOptions _dialogOptions = new()
    {
        CloseButton = true,
        CloseOnEscapeKey = true,
        MaxWidth = MaxWidth.Medium
    };


    private async Task<TableData<Expense>> ServerReload(TableState state) =>
        await FinanceService.GetPagedExpensesAsync(new TableStateDTO(_searchString, _dateRange, state));

    private void OnSearchStringChanged() => _table.ReloadServerData();

    private void OnDateRangeChange(DateRange dateRange)
    {
        _dateRange = dateRange;
        _table.ReloadServerData();
    }

    private void RowClickEvent(TableRowClickEventArgs<Expense> tableRowClickEventArgs)
    {
        
    }

    private void PageChanged(int i) => _table.NavigateTo(i - 1);

    private string SelectedRowClassFunc(Expense expense, int rowNumber)
    {
        if (_selectedRowNumber == rowNumber)
        {
            _selectedRowNumber = -1;
            _selectedExpense = null;
            return string.Empty;
        }
        else if (_table.SelectedItem != null && _table.SelectedItem.Equals(expense))
        {
            _selectedRowNumber = rowNumber;
            _selectedExpense = expense;
            return "selected";
        }
        else
        {
            return string.Empty;
        }
    }

    private async Task CreateExpense()
    {
        IDialogReference expenseDialog = DialogService.Show<ExpenseDialog>("Create expense", _dialogOptions);

        DialogResult result = await expenseDialog.Result;

        if (!result.Cancelled)
        {
            await _table.ReloadServerData();
        }
    }

    private async Task UpdateExpense()
    {
        DialogParameters parameters = new() { ["Expense"] = _selectedExpense };

        IDialogReference expenseDialog = DialogService.Show<ExpenseDialog>("Update expense", parameters, _dialogOptions);

        DialogResult result = await expenseDialog.Result;

        if (!result.Cancelled)
        {
            await _table.ReloadServerData();
        }
    }
}