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
    public async Task AddAsync_SuccessfullScenario_ReturnsTrue()
    {
        // A
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();
        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveRequestToDomain(It.IsAny<PersonSaveRequest>())).Returns(person);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Person>())).ReturnsAsync(true);

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
        _personMapperMock.Setup(p => p.SaveRequestToDomain(It.IsAny<PersonSaveRequest>())).Returns(person);

        var validationResult = new ValidationResult() 
        {
            Errors = new List<ValidationFailure> 
            { 
                new ValidationFailure("error", "error"),
                new ValidationFailure("error", "error")
            }
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        // A
        var addPersonResult = await _personService.AddAsync(personSaveRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(addPersonResult);
    }
}
