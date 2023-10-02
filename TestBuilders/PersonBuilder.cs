using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Entities;

namespace TestBuilders;
public sealed class PersonBuilder
{
    private int _id = 123;
    private string _nome = "nome";
    private string _sobrenome = "sobre";
    private string _telefone = "41996748512";

    public static PersonBuilder NewObject() => new PersonBuilder();

    public Person DomainBuild() =>
        new Person()
        {
            Id = _id,
            Nome = _nome,
            Sobrenome = _sobrenome,
            Telefone = _telefone
        };

    public PersonSaveRequest SaveRequestBuild() =>
        new PersonSaveRequest()
        {
            Nome = _nome,
            Sobrenome = _sobrenome,
            Telefone = _telefone
        };

    public PersonUpdateRequest UpdateRequestBuild() =>
        new PersonUpdateRequest()
        {
            Id = _id,
            Nome = _nome,
            Sobrenome = _sobrenome,
            Telefone = _telefone
        };

    public PersonResponse ResponseBuild() =>
        new PersonResponse()
        {
            Id = _id,
            Nome = _nome,
            Sobrenome = _sobrenome,
            Telefone = _telefone
        };

    public PersonBuilder WithId(int id)
    {
        _id = id;

        return this;
    }


    public PersonBuilder WithNome(string nome)
    {
        _nome = nome;

        return this;
    }

    public PersonBuilder WithSobrenome(string sobrenome)
    {
        _sobrenome = sobrenome;

        return this;
    }

    public PersonBuilder WithTelefone(string telefone)
    {
        _telefone = telefone;

        return this;
    }
}
