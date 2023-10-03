using Microsoft.EntityFrameworkCore;
using TesteTecnicoIdealSoft.API.Data.DatabaseContexts;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjectionHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        string connectionString;
        if(Environment.GetEnvironmentVariable("DOCKER_ENVIROMENT") == "DEV_DOCKER")
            connectionString = configuration.GetConnectionString("ContainerConnection");
        else
            connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<IdealSoftDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
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
