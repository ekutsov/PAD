namespace PAD.Finance.Core.Services;

public abstract class BaseService
{
    protected readonly FinanceDbContext _context;

    protected readonly IMapper _mapper;

    protected readonly IConfigurationProvider _mapperProvider;

    protected BaseService(FinanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _mapperProvider = mapper.ConfigurationProvider;
    }
}