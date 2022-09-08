namespace PAD.Finance.Core.Services;

public class ExpenseCategoryService : BaseService
{
    public ExpenseCategoryService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<ExpenseViewModel>> GetAllAsync()
    {
        List<ExpenseViewModel> expenses = await _context.Expsenses
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            .ToListAsync();

        return expenses;
    }
}