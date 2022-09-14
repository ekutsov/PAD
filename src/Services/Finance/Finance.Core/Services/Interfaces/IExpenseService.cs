namespace PAD.Finance.Core.Services;

public interface IExpenseService
{
    /// <summary>
    /// Get paged user expenses 
    /// </summary>
    /// <param name="tableState">Table params</param>
    /// <param name="userId">User identifier</param>
    /// <returns>Paged expenses and total value</returns>
    Task<TableViewModel<ExpenseViewModel>> GetPagedAsync(TableStateDTO tableState, Guid userId);

    /// <summary>
    /// Create expense for user
    /// </summary>
    /// <param name="expenseDTO">Expense data</param>
    /// <param name="userId">User identifier</param>
    /// <returns></returns>
    Task CreateAsync(ExpenseDTO expenseDTO, Guid userId);

    Task UpdateAsync(Guid id, ExpenseDTO expenseDTO, Guid userId);

    Task DeleteAsync(Guid id, Guid userId);
}