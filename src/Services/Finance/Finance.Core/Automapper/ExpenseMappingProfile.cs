using AutoMapper;
using PAD.Finance.Domain.ViewModels;
using PAD.Finance.Infrastructure.Models;

namespace PAD.Finance.Core.Automapper;

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<Expense, ExpenseViewModel>();
        
        CreateMap<ExpenseCategory, ExpenseCategoryViewModel>();
    }
}