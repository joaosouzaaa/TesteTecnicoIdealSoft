using FluentValidation;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Enums;
using TesteTecnicoIdealSoft.API.Extensions;
using TesteTecnicoIdealSoft.API.Interfaces.Mappers;
using TesteTecnicoIdealSoft.API.Interfaces.Repositories;
using TesteTecnicoIdealSoft.API.Interfaces.Services;
using TesteTecnicoIdealSoft.API.Interfaces.Settings;
using TesteTecnicoIdealSoft.API.Services.BaseServices;

namespace TesteTecnicoIdealSoft.API.Services;

public sealed class PersonService : BaseService<Person>, IPersonService
{
    private readonly IPersonMapper _personMapper;
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonMapper personMapper, IPersonRepository personRepository, 
                         INotificationHandler notificationHandler, IValidator<Person> validator) 
                         : base(notificationHandler, validator)
    {
        _personMapper = personMapper;
        _personRepository = personRepository;
    }

    public async Task<bool> AddAsync(PersonSaveRequest personSaveRequest)
    {
        var person = _personMapper.SaveRequestToDomain(personSaveRequest);

        if (!await ValidateAsync(person))
            return false;

        return await _personRepository.AddAsync(person);
    }

    public async Task<bool> UpdateAsync(PersonUpdateRequest personUpdateRequest)
    {
        if (!await _personRepository.ExistsAsync(personUpdateRequest.Id))
            return _notificationHandler.AddNotification("Não existe", EMessage.DoesNotExist.Description().FormatTo("Pessoa"));

        var person = _personMapper.UpdateRequestToDomain(personUpdateRequest);

        if (!await ValidateAsync(person))
            return false;

        return await _personRepository.UpdateAsync(person);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _personRepository.ExistsAsync(id))
            return _notificationHandler.AddNotification("Não existe", EMessage.DoesNotExist.Description().FormatTo("Pessoa"));

        return await _personRepository.DeleteAsync(id);
    }

    public async Task<List<PersonResponse>> GetAllAsync()
    {
        var personList = await _personRepository.GetAllAsync();

        var personResponseList = new List<PersonResponse>();
        foreach(var person in personList)
        {
            var personResponse = _personMapper.DomainToResponse(person);
            personResponseList.Add(personResponse);
        }

        return personResponseList;
    }
}
