using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PAD.Finance.API.Extensions;

public static class AutomapperExtensions
{
    public static void AddAutomapperWithProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}