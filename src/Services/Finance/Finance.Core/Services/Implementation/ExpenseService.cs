using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PAD.Finance.Domain.ViewModels;
using PAD.Finance.Infrastructure.Data;

namespace PAD.Finance.Core.Services;

public class ExpenseService : IExpenseService
{
    private readonly FinanceDbContext _context;

    private readonly IConfigurationProvider _mapperProvider;
    public ExpenseService(FinanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapperProvider = mapper.ConfigurationProvider;
    }

    public async Task<List<ExpenseViewModel>> GetAllAsync()
    {
        List<ExpenseViewModel> expenses = await _context.Expsenses
            .ProjectTo<ExpenseViewModel>(_mapperProvider)
            .ToListAsync();

        return expenses;
    }
}