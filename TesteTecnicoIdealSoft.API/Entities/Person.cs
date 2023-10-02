namespace TesteTecnicoIdealSoft.API.Entities;

public sealed class Person
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required string Telefone { get; set; }
}
