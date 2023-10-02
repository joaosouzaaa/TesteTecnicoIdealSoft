using FluentValidation;
using FluentValidation.Results;
using Moq;
using TestBuilders;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Interfaces.Mappers;
using TesteTecnicoIdealSoft.API.Interfaces.Repositories;
using TesteTecnicoIdealSoft.API.Interfaces.Settings;
using TesteTecnicoIdealSoft.API.Services;

namespace UnitTests.ServicesTests;
public sealed class PersonServiceTests
{
    private readonly Mock<IPersonMapper> _personMapperMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<Person>> _validatorMock;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _personMapperMock = new Mock<IPersonMapper>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<Person>>();
        _personService = new PersonService(_personMapperMock.Object, _personRepositoryMock.Object,
            _notificationHandlerMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();
        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveRequestToDomain(It.IsAny<PersonSaveRequest>()))
            .Returns(person);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Person>()))
            .ReturnsAsync(true);

        // A
        var addPersonResult = await _personService.AddAsync(personSaveRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Once());
        
        Assert.True(addPersonResult);
    }

    [Fact]
    public async Task AddAsync_PersonInvalid_ReturnsFalse()
    {
        // A
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();
        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveRequestToDomain(It.IsAny<PersonSaveRequest>()))
            .Returns(person);

        var validationResult = new ValidationResult() 
        {
            Errors = new List<ValidationFailure> 
            { 
                new ValidationFailure("error", "error"),
                new ValidationFailure("error", "error")
            }
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addPersonResult = await _personService.AddAsync(personSaveRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(addPersonResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personUpdateRequest = PersonBuilder.NewObject().UpdateRequestBuild();

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.Is<int>(p => p == personUpdateRequest.Id)))
            .ReturnsAsync(true);

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.UpdateRequestToDomain(It.IsAny<PersonUpdateRequest>()))
            .Returns(person);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<Person>()))
            .ReturnsAsync(true);

        // A
        var updatePersonResult = await _personService.UpdateAsync(personUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(updatePersonResult);
    }

    [Fact]
    public async Task UpdateAsync_PersonDoesNotExist_ReturnsFalse()
    {
        // A
        var personUpdateRequest = PersonBuilder.NewObject().UpdateRequestBuild();

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.Is<int>(p => p == personUpdateRequest.Id)))
            .ReturnsAsync(false);

        // A
        var updatePersonResult = await _personService.UpdateAsync(personUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _personMapperMock.Verify(p => p.UpdateRequestToDomain(It.IsAny<PersonUpdateRequest>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Never());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(updatePersonResult);
    }

    [Fact]
    public async Task UpdateAsync_PersonInvalid_ReturnsFalse()
    {
        // A
        var personUpdateRequest = PersonBuilder.NewObject().UpdateRequestBuild();

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.Is<int>(p => p == personUpdateRequest.Id)))
            .ReturnsAsync(true);

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.UpdateRequestToDomain(It.IsAny<PersonUpdateRequest>()))
            .Returns(person);

        var validationResult = new ValidationResult() 
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure("error", "rad")
            }
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updatePersonResult = await _personService.UpdateAsync(personUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(updatePersonResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 1;
        _personRepositoryMock.Setup(p => p.ExistsAsync(It.Is<int>(p => p == id)))
            .ReturnsAsync(true);
        _personRepositoryMock.Setup(p => p.DeleteAsync(It.Is<int>(p => p == id)))
            .ReturnsAsync(true);

        // A
        var deletePersonResult = await _personService.DeleteAsync(id);

        // A
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.Is<int>(p => p == id)), Times.Once());

        Assert.True(deletePersonResult);
    }

    [Fact]
    public async Task DeleteAsync_PersonDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 1;

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.Is<int>(p => p == id)))
            .ReturnsAsync(false);

        // A
        var deletePersonResult = await _personService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.Is<int>(p => p == id)), Times.Never());

        Assert.False(deletePersonResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsPersonResponseList()
    {
        // A
        var personList = new List<Person>()
        {
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild()
        };

        _personRepositoryMock.Setup(p => p.GetAllAsync())
            .ReturnsAsync(personList);

        var personResponse = PersonBuilder.NewObject().ResponseBuild();
        _personMapperMock.SetupSequence(p => p.DomainToResponse(It.IsAny<Person>()))
            .Returns(personResponse)
            .Returns(personResponse)
            .Returns(personResponse)
            .Returns(personResponse);

        // A
        var personResponseListResult = await _personService.GetAllAsync();

        // A
        var personListCount = personList.Count;
        _personMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Person>()), Times.Exactly(personListCount));
        Assert.Equal(personResponseListResult.Count, personListCount);
    }
}
