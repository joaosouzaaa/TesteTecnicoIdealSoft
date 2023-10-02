using TestBuilders;
using TesteTecnicoIdealSoft.API.Settings.ValidationSettings;

namespace UnitTests.SettingsTests.ValidatorsTests;
public sealed class PersonValidatorTests
{
    private readonly PersonValidator _personValidator;

	public PersonValidatorTests()
	{
		_personValidator = new PersonValidator();
	}

	[Fact]
	public async Task ValidatePersonAsync_SuccessfullScenario_ReturnsTrue()
	{
		// A
		var personToValidate = PersonBuilder.NewObject().DomainBuild();

		// A
		var personValidationResult = await _personValidator.ValidateAsync(personToValidate);

		// A
		Assert.True(personValidationResult.IsValid);
	}

    [Theory]
	[MemberData(nameof(InvalidNomeParameters))]
    public async Task ValidatePersonAsync_NomeInvalid_ReturnsFalse(string nome)
    {
        // A
        var personToValidate = PersonBuilder.NewObject().WithNome(nome).DomainBuild();

        // A
        var personValidationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.False(personValidationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidSobrenomeParameters))]
    public async Task ValidatePersonAsync_SobrenomeInvalid_ReturnsFalse(string sobrenome)
    {
        // A
        var personToValidate = PersonBuilder.NewObject().WithSobrenome(sobrenome).DomainBuild();

        // A
        var personValidationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.False(personValidationResult.IsValid);
    }

    [Theory]
    [InlineData("123012903")]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("random")]
    public async Task ValidatePersonAsync_TelefoneInvalid_ReturnsFalse(string telefone)
    {
        // A
        var personToValidate = PersonBuilder.NewObject().WithTelefone(telefone).DomainBuild();

        // A
        var personValidationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.False(personValidationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidNomeParameters()
	{
        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            new string('a', 100)
        };
    }

    public static IEnumerable<object[]> InvalidSobrenomeParameters()
    {
        yield return new object[]
        {
            new string('a', 110)
        };

        yield return new object[]
        {
            ""
        };

        //yield return new object[]
        //{
        //    null
        //};

        yield return new object[]
        {
            "a"
        };
    }
}
