using TesteTecnicoIdealSoft.API.Interfaces.Settings;
using TesteTecnicoIdealSoft.API.Settings.NotificationSettings;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
        services.AddValidatorsDependencyInjection();
    }
}
