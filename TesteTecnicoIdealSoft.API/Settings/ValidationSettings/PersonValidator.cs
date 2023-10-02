using FluentValidation;
using TesteTecnicoIdealSoft.API.Entities;
using TesteTecnicoIdealSoft.API.Enums;
using TesteTecnicoIdealSoft.API.Extensions;

namespace TesteTecnicoIdealSoft.API.Settings.ValidationSettings;

public sealed class PersonValidator : AbstractValidator<Person>
{
	public PersonValidator()
	{
		RuleFor(p => p.Nome).Length(3, 50)
			.WithMessage(p => string.IsNullOrEmpty(p.Nome)
			? EMessage.Required.Description().FormatTo("Nome")
			: EMessage.InvalidLength.Description().FormatTo("Nome", "3 a 50"));

        RuleFor(p => p.Sobrenome).Length(3, 100)
            .WithMessage(p => string.IsNullOrEmpty(p.Sobrenome)
            ? EMessage.Required.Description().FormatTo("Sobrenome")
            : EMessage.InvalidLength.Description().FormatTo("Sobrenome", "3 a 100"));

        RuleFor(p => p.Telefone).Matches(@"^\d{11}$")
            .WithMessage(p => EMessage.InvalidFormat.Description().FormatTo("Telefone", "99999999999"));
    }
}
