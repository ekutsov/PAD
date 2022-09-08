namespace PAD.Finance.Core.Services;

public class ExpenseService : BaseService, IExpenseService
{
    public ExpenseService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<ExpenseViewModel>> GetAllAsync()
    {
        List<ExpenseViewModel> expenses = await _context.Expsenses
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            .ToListAsync();

        return expenses;
    }

    public async Task<ExpenseViewModel> CreateAsync(ExpenseDTO expenseDTO)
    {
        Expense expense = _mapper.Map<Expense>(expenseDTO);

        _context.Expsenses.Add(expense);

        await _context.SaveChangesAsync();

        ExpenseViewModel expenseView = _mapper.Map<ExpenseViewModel>(expense);

        return expenseView;
    }
}