using TesteTecnicoIdealSoft.API.Entities;

namespace TesteTecnicoIdealSoft.API.Interfaces.Repositories;

public interface IPersonRepository : IDisposable
{
    Task<bool> AddAsync(Person person);
    Task<bool> UpdateAsync(Person person);
    Task<bool> DeleteAsync(int id);
    Task<List<Person>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
}
