using PAD.Finance.API.Extensions;

namespace PAD.Finance.Core.Services;

public class ExpenseService : BaseService, IExpenseService
{
    public ExpenseService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    /// <summary>
    /// Get paged user expenses 
    /// </summary>
    /// <param name="tableState">Table params</param>
    /// <param name="userId">User identifier</param>
    /// <returns>Paged expenses and total value</returns>
    public async Task<TableViewModel<ExpenseViewModel>> GetPagedAsync(TableStateDTO tableState, Guid userId)
    {
        string searchString = "%" + tableState.SearchString + "%";
        IQueryable<Expense> query = _context.Expsenses
            .Where(e => e.AuthorId == userId)
            .Where(e => tableState.StartDate.ToUniversalTime() <= e.CreatedDate && tableState.EndDate.ToUniversalTime() >= e.CreatedDate)
            .Where(e => string.IsNullOrWhiteSpace(searchString) ||
                        EF.Functions.ILike(e.Category.Name, searchString) ||
                        EF.Functions.ILike(e.Description, searchString) ||
                        EF.Functions.ILike(e.Amount.ToString(), searchString));

        List<ExpenseViewModel> expenses = await query
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            // .OrderByDirection(tableState.SortLabel, tableState.SortDirection)
            .Skip(tableState.Page)
            .Take(tableState.PageSize)
            .ToListAsync();

        int totalItems = await query.CountAsync();

        return new(expenses, totalItems);
    }

    /// <summary>
    /// Create expense for user
    /// </summary>
    /// <param name="expenseDTO">Expense data</param>
    /// <param name="userId">User identifier</param>
    /// <returns></returns>
    public async Task CreateAsync(ExpenseDTO expenseDTO, Guid userId)
    {
        Expense expense = _mapper.Map<Expense>(expenseDTO);

        expense.AuthorId = userId;

        _context.Expsenses.Add(expense);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, ExpenseDTO expenseDTO, Guid userId)
    {
        Expense? expense = await _context.Expsenses.FirstOrDefaultAsync(e => e.Id == id && e.AuthorId == userId);

        if (expense != null)
        {
            _mapper.Map(expenseDTO, expense);

            _context.Expsenses.Update(expense);

            await _context.SaveChangesAsync();
        }
    }
}