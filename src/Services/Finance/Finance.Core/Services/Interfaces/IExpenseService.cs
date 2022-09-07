using PAD.Finance.Domain.ViewModels;

namespace PAD.Finance.Core.Services;

public interface IExpenseService
{
    Task<List<ExpenseViewModel>> GetAllAsync();
}