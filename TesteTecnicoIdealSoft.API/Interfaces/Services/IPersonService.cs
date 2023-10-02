using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;

namespace TesteTecnicoIdealSoft.API.Interfaces.Services;

public interface IPersonService
{
    Task<bool> AddAsync(PersonSaveRequest personSaveRequest);
    Task<bool> UpdateAsync(PersonUpdateRequest personUpdateRequest);
    Task<bool> DeleteAsync(int id);
    Task<List<PersonResponse>> GetAllAsync();
}
