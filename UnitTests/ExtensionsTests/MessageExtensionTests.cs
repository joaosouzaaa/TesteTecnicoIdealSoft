using TesteTecnicoIdealSoft.API.Enums;
using TesteTecnicoIdealSoft.API.Extensions;

namespace UnitTests.ExtensionsTests;
public sealed class MessageExtensionTests
{
    [Fact]
    public void Description_Equals_AsIntended()
    {
        var messageDescription = EMessage.Required.Description();

        Assert.Equal(messageDescription, "{0} precisa ser preenchido.");
    }
}
