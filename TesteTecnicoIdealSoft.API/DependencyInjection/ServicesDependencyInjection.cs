using TesteTecnicoIdealSoft.API.Interfaces.Services;
using TesteTecnicoIdealSoft.API.Services;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
    }
}
