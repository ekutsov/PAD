namespace PAD.Finance.Core.Services;

public class ExpenseService : BaseService, IExpenseService
{
    public ExpenseService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<TableViewModel<ExpenseViewModel>> GetAllAsync(TableStateDTO tableState)
    {
        IQueryable<Expense> query =  _context.Expsenses.Where(x => true);

        List<ExpenseViewModel> expenses = await query
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            .Skip(tableState.Page)
            .Take(tableState.PageSize)
            .ToListAsync();
        
        int totalItems = await query.CountAsync();

        return new(expenses, totalItems);
    }

    public async Task<ExpenseViewModel> CreateAsync(ExpenseDTO expenseDTO)
    {
        Expense expense = _mapper.Map<Expense>(expenseDTO);

        expense.CreatedDate = DateTime.UtcNow;

        _context.Expsenses.Add(expense);

        await _context.SaveChangesAsync();

        ExpenseViewModel expenseView = _mapper.Map<ExpenseViewModel>(expense);

        return expenseView;
    }
}