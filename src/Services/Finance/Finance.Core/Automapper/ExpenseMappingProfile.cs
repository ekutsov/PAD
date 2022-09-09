namespace PAD.Finance.Core.Automapper;

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<ExpenseDTO, Expense>(MemberList.Source);

        CreateMap<Expense, ExpenseViewModel>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToLocalTime()));

        CreateMap<ExpenseCategory, ExpenseCategoryViewModel>();
    }
}