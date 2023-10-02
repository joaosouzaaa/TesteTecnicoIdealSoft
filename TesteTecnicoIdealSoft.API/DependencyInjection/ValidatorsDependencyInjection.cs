using FluentValidation;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Settings.ValidationSettings;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class ValidatorsDependencyInjection
{
    public static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Person>, PersonValidator>();
    }
}
