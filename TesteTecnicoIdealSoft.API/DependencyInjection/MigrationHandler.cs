using Microsoft.EntityFrameworkCore;
using TesteTecnicoIdealSoft.API.Data.DatabaseContexts;

namespace TesteTecnicoIdealSoft.API.DependencyInjection;

public static class MigrationHandler
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<IdealSoftDbContext>();

        try
        {
            appContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
