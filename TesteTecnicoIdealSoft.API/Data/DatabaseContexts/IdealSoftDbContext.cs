using Microsoft.EntityFrameworkCore;
using TesteTecnicoIdealSoft.API.Entities;

namespace TesteTecnicoIdealSoft.API.Data.DatabaseContexts;

public sealed class IdealSoftDbContext : DbContext
{
	public IdealSoftDbContext(DbContextOptions<IdealSoftDbContext> options) : base(options)
	{

	}

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdealSoftDbContext).Assembly);
    }
}
