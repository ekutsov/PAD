namespace PAD.Finance.Core.Services;

public class ExpenseService : BaseService, IExpenseService
{
    public ExpenseService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<TableViewModel<ExpenseViewModel>> GetAllAsync(TableStateDTO tableState)
    {
        string searchString = "%" + tableState.SearchString + "%";
        IQueryable<Expense> query = _context.Expsenses
            .Where(e => tableState.StartDate.ToUniversalTime() <= e.CreatedDate && tableState.EndDate.ToUniversalTime() >= e.CreatedDate)
            .Where(e => string.IsNullOrWhiteSpace(searchString) ||
                        EF.Functions.ILike(e.Category.Name, searchString) ||
                        EF.Functions.ILike(e.Description, searchString) ||
                        EF.Functions.ILike(e.Amount.ToString(), searchString));

        List<ExpenseViewModel> expenses = await query
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            .Skip(tableState.Page)
            .Take(tableState.PageSize)
            .ToListAsync();

        int totalItems = await query.CountAsync();

        return new(expenses, totalItems);
    }

    public async Task CreateAsync(ExpenseDTO expenseDTO)
    {
        Expense expense = _mapper.Map<Expense>(expenseDTO);

        _context.Expsenses.Add(expense);

        await _context.SaveChangesAsync();
    }
}