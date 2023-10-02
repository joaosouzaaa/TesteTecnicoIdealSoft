using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using TesteTecnicoIdealSoft.API.Data.DatabaseContexts;

namespace IntegrationTests.Fixture;
public sealed class HttpClientFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;

    public HttpClientFactory()
    {
        _dbContainer = new MsSqlBuilder().WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPortBinding(8080, true)
            .WithEnvironment("-e", "MSSQL_PID=Express")
            .WithName("SqlServer-IntegrationTest")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<IdealSoftDbContext>));

            services.AddDbContext<IdealSoftDbContext>(options =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IdealSoftDbContext>();

        await dbContext.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync() =>
        await _dbContainer.StopAsync();
}
