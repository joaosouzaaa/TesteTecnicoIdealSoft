using TesteTecnicoIdealSoft.API.Interfaces.Mappers;
using TesteTecnicoIdealSoft.API.Mappers;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonMapper, PersonMapper>();
    }
}
