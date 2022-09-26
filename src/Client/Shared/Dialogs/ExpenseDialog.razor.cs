using Microsoft.AspNetCore.Components.Forms;

namespace PAD.Client.Shared.Dialogs;

public partial class ExpenseDialog
{
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    [Inject] protected StateContainer State { get; set; }

    [Parameter] public Expense Expense { get; set; }

    [Parameter] public List<ExpenseCategory> Categories { get; set; }

    [Inject] protected IFinanceService FinanceService { get; set; }

    private ExpenseDTO ExpenseDTO { get; set; } = new();

    private bool IsEditMode { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Expense != null)
        {
            IsEditMode = true;
            ExpenseDTO = new(Expense, State.TimezoneOffset);
        }
    }

    protected async Task OnValidSubmit(EditContext context)
    {
        StateHasChanged();

        if (!IsEditMode)
        {
            await FinanceService.CreateExpenseAsync(ExpenseDTO);
        }
        else
        {
            await FinanceService.UpdateExpenseAsync(Expense.Id, ExpenseDTO);
        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    protected async Task OnInvalidSubmit(EditContext context) => context.Validate();
}