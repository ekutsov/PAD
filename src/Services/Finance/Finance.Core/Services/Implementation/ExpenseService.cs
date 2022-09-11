using PAD.Finance.API.Extensions;

namespace PAD.Finance.Core.Services;

public class ExpenseService : BaseService, IExpenseService
{
    public ExpenseService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<TableViewModel<ExpenseViewModel>> GetAllAsync(TableStateDTO tableState, string userId)
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

    public async Task CreateAsync(ExpenseDTO expenseDTO, string userId)
    {
        Expense expense = _mapper.Map<Expense>(expenseDTO);

        expense.AuthorId = userId;

        _context.Expsenses.Add(expense);

        await _context.SaveChangesAsync();
    }
}