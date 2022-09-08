namespace PAD.Finance.Core.Services;

public class ExpenseCategoryService : BaseService, IExpenseCategoryService
{
    public ExpenseCategoryService(FinanceDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<ExpenseCategoryViewModel>> GetAllAsync()
    {
        List<ExpenseCategoryViewModel> expenseCategories = await _context.ExpsenseCategories
            .ProjectTo<ExpenseCategoryViewModel>(_mapperProvider)
            .ToListAsync();

        return expenseCategories;
    }
}