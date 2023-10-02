namespace TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;

public sealed class PersonResponse
{
    public required int Id { get; set; }
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required string Telefone { get; set; }
}
