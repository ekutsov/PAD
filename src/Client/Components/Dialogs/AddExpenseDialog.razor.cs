using Microsoft.AspNetCore.Components.Forms;

namespace PAD.Client.Components.Dialogs;

public partial class AddExpenseDialog
{
    [CascadingParameter]
    protected MudDialogInstance MudDialog { get; set; }

    [Inject]
    protected IFinanceService FinanceService { get; set; }

    [Inject]
    protected IConsoleService Console { get; set; }

    protected List<ExpenseCategory> Categories { get; set; }

    protected ExpenseDTO Expense = new ExpenseDTO();

    protected DateTime? CreatedDate = DateTime.Today;

    protected override async Task OnInitializedAsync()
    {
        Categories = await FinanceService.GetExpenseCategoriesAsync();
    }

    protected async Task OnValidSubmit(EditContext context)
    {
        StateHasChanged();

        Expense.CreatedDate = CreatedDate.Value.ToUniversalTime();

        await FinanceService.CreateExpense(Expense);

        MudDialog.Close(DialogResult.Ok(true));
    }

    protected async Task OnInvalidSubmit(EditContext context) => context.Validate();
}