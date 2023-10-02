using TesteTecnicoIdealSoft.API.Data.Repositories;
using TesteTecnicoIdealSoft.API.Interfaces.Repositories;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
