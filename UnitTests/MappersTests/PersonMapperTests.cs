using TestBuilders;
using TesteTecnicoIdealSoft.API.Mappers;

namespace UnitTests.MappersTests;
public sealed class PersonMapperTests
{
    private readonly PersonMapper _personMapper;

	public PersonMapperTests()
	{
		_personMapper = new PersonMapper();
	}

	[Fact]
	public void DomainToResponse_SuccessfulScenario()
	{
		// A
		var person = PersonBuilder.NewObject().DomainBuild();

		// A
		var personResponseResult = _personMapper.DomainToResponse(person);

		// A
		Assert.Equal(personResponseResult.Id, person.Id);
        Assert.Equal(personResponseResult.Nome, person.Nome);
        Assert.Equal(personResponseResult.Sobrenome, person.Sobrenome);
        Assert.Equal(personResponseResult.Telefone, person.Telefone);
    }

    [Fact]
    public void SaveRequestToDomain_SuccessfulScenario()
    {
        // A
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();

        // A
        var personResult = _personMapper.SaveRequestToDomain(personSaveRequest);

        // A
        Assert.Equal(personResult.Nome, personSaveRequest.Nome);
        Assert.Equal(personResult.Sobrenome, personSaveRequest.Sobrenome);
        Assert.Equal(personResult.Telefone, personSaveRequest.Telefone);
    }

    [Fact]
    public void UpdateRequestToDomain_SuccessfulScenario()
    {
        // A
        var personUpdateRequest = PersonBuilder.NewObject().UpdateRequestBuild();

        // A
        var personResult = _personMapper.UpdateRequestToDomain(personUpdateRequest);

        // A
        Assert.Equal(personResult.Id, personUpdateRequest.Id);
        Assert.Equal(personResult.Nome, personUpdateRequest.Nome);
        Assert.Equal(personResult.Sobrenome, personUpdateRequest.Sobrenome);
        Assert.Equal(personResult.Telefone, personUpdateRequest.Telefone);
    }
}
