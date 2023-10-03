namespace TesteTecnicoIdealSoft.WPF.Requests.Person;

public sealed class PersonUpdateRequest
{
    public required int Id { get; set; }
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required string Telefone { get; set; }
}
