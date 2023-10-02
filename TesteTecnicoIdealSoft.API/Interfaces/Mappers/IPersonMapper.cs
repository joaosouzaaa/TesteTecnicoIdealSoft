using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Entities;

namespace TesteTecnicoIdealSoft.API.Interfaces.Mappers;

public interface IPersonMapper
{
    Person SaveRequestToDomain(PersonSaveRequest personSaveRequest);
    Person UpdateRequestToDomain(PersonUpdateRequest personUpdateRequest);
    PersonResponse DomainToResponse(Person person);
}
