namespace PAD.Finance.Core.Automapper;

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<ExpenseDTO, Expense>(MemberList.Source);

        CreateMap<Expense, ExpenseViewModel>();
        
        CreateMap<ExpenseCategory, ExpenseCategoryViewModel>();
    }
}