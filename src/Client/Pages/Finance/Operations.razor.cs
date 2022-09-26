namespace PAD.Client.Finance;

[Authorize]
[Route("finance/operations")]
public partial class Operations
{
    [Inject] private IFinanceService FinanceService { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    [Inject] private StateContainer State { get; set; }

    [Inject] private ISnackbarService Snackbar { get; set; }

    private List<ExpenseCategory> Categories { get; set; }

    private MudTable<Expense> _table;

    private string _searchString = String.Empty;

    private Guid? selectedRowId = null;

    private DateRange _dateRange = new DateRange(DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

    private Func<Expense, int, string> RowClass
    {
        get => new Func<Expense, int, string>(SelectedRowClassFunc);
        set { return; }
    }

    private DialogOptions _dialogOptions = new()
    {
        CloseButton = true,
        CloseOnEscapeKey = true,
        MaxWidth = MaxWidth.Medium
    };

    protected override async Task OnInitializedAsync()
    {
        Categories = await FinanceService.GetExpenseCategoriesAsync();
    }

    private async Task<TableData<Expense>> ServerReload(TableState state)
    {
        TableStateDTO tableStateDTO = new(_searchString, _dateRange, state);

        TableData<Expense> expenses = await FinanceService.GetPagedExpensesAsync(tableStateDTO);

        return expenses;
    }

    private async Task OnSearchStringChanged() => await _table.ReloadServerData();

    private async Task OnDateRangeChange(DateRange dateRange)
    {
        _dateRange = dateRange;
        await _table.ReloadServerData();
    }

    private void RowClickEvent(TableRowClickEventArgs<Expense> tableRowClickEventArgs)
    {
        if (selectedRowId == tableRowClickEventArgs.Item.Id)
        {
            selectedRowId = null;
            _table.SelectedItem = null;
        }
        else
        {
            selectedRowId = tableRowClickEventArgs.Item.Id;
        }
    }

    private string SelectedRowClassFunc(Expense element, int rowNumber) =>
        selectedRowId == element.Id ? "selected" : string.Empty;

    private async Task CreateExpense()
    {
        DialogParameters parameters = new() { ["Categories"] = Categories };

        IDialogReference expenseDialog = DialogService.Show<ExpenseDialog>("Add expense", parameters, _dialogOptions);

        DialogResult result = await expenseDialog.Result;

        if (!result.Cancelled)
        {
            Snackbar.Success("The expense was created");
            await _table.ReloadServerData();
        }
    }

    private async Task UpdateExpense()
    {
        DialogParameters parameters = new() { ["Expense"] = _table.SelectedItem, ["Categories"] = Categories };

        IDialogReference expenseDialog = DialogService.Show<ExpenseDialog>("Edit expense", parameters, _dialogOptions);

        DialogResult result = await expenseDialog.Result;

        if (!result.Cancelled)
        {
            Snackbar.Success("The expense was updated");
            await _table.ReloadServerData();
        }
    }

    private async Task DeleteExpense()
    {
        DialogParameters parameters = new()
        {
            ["ContentText"] = "Do you really want to delete this expense? This process cannot be undone.",
            ["ButtonText"] = "Delete",
            ["Color"] = Color.Error
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        IDialogReference deleteExpenseDialog = DialogService.Show<ConfirmDialog>("Delete expense", parameters, options);

        DialogResult result = await deleteExpenseDialog.Result;

        if (!result.Cancelled)
        {
            bool.TryParse(result.Data.ToString(), out bool isDelete);

            if (isDelete)
            {
                await FinanceService.DeleteExpenseAsync(_table.SelectedItem.Id);

                Snackbar.Success("The expense was deleted");

                await _table.ReloadServerData();
            }
        }
    }
}