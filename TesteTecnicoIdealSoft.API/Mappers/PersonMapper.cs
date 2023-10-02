using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Interfaces.Mappers;

namespace TesteTecnicoIdealSoft.API.Mappers;

public sealed class PersonMapper : IPersonMapper
{
    public PersonResponse DomainToResponse(Person person) =>
        new PersonResponse()
        {
            Id = person.Id,
            Nome = person.Nome,
            Sobrenome = person.Sobrenome,
            Telefone = person.Telefone
        };

    public Person SaveRequestToDomain(PersonSaveRequest personSaveRequest) =>
        new Person()
        {
            Nome = personSaveRequest.Nome,
            Sobrenome = personSaveRequest.Sobrenome,
            Telefone = personSaveRequest.Telefone
        };

    public Person UpdateRequestToDomain(PersonUpdateRequest personUpdateRequest) =>
        new Person()
        {
            Id = personUpdateRequest.Id,
            Nome = personUpdateRequest.Nome,
            Sobrenome = personUpdateRequest.Sobrenome,
            Telefone = personUpdateRequest.Telefone
        };
}
