using Microsoft.EntityFrameworkCore;
using TesteTecnicoIdealSoft.API.Data.DatabaseContexts;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjectionHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        services.AddDbContext<IdealSoftDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ContainerConnection"));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddSettingsDependencyInjection();
        services.AddServicesDependencyInjection();
        services.AddFiltersDependencyInjection();
    }
}
