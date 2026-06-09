using HanaHRMApi.Providers;
using HanaHRMApi.Repositories;
using HanaHRMApi.Repositories.Interfaces;

namespace HanaHRMApi.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IClientProvider, ClientProvider>();
        services.AddScoped<ICommonRepository, CommonRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        return services;
    }
}
