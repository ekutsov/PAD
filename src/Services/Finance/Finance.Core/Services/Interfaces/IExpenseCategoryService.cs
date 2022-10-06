namespace PAD.Finance.Core.Services;

public interface IExpenseCategoryService
{
    Task<List<ExpenseCategoryViewModel>> GetAllAsync();
}