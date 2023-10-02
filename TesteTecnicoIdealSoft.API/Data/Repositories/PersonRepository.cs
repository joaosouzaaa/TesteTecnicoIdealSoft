using Microsoft.EntityFrameworkCore;
using TesteTecnicoIdealSoft.API.Data.DatabaseContexts;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Interfaces.Repositories;

namespace TesteTecnicoIdealSoft.API.Data.Repositories;

public sealed class PersonRepository : IPersonRepository
{
    private readonly IdealSoftDbContext _dbContext;
    private DbSet<Person> _dbContextSet => _dbContext.Set<Person>();

    public PersonRepository(IdealSoftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddAsync(Person person)
    {
        await _dbContextSet.AddAsync(person);

        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Person person)
    {
        _dbContext.Entry(person).State = EntityState.Modified;

        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _dbContextSet.FirstOrDefaultAsync(p => p.Id == id);

        _dbContextSet.Remove(person);

        return await SaveChangesAsync();
    }

    public async Task<List<Person>> GetAllAsync() =>
        await _dbContextSet.AsNoTracking().ToListAsync();

    public async Task<bool> ExistsAsync(int id) =>
        await _dbContextSet.AsNoTracking().AnyAsync(p => p.Id == id);

    public void Dispose() =>
        _dbContext.Dispose();

    private async Task<bool> SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync() > 0;
}
