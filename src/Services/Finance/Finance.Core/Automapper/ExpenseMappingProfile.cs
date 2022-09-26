namespace PAD.Finance.Core.Automapper;

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<ExpenseDTO, Expense>(MemberList.Source)
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToUniversalTime()));

        CreateMap<Expense, ExpenseViewModel>();

        CreateMap<ExpenseCategory, ExpenseCategoryViewModel>();
    }
}