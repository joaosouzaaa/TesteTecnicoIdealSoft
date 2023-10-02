using System.ComponentModel;

namespace TesteTecnicoIdealSoft.API.Enums;

public enum EMessage : ushort
{
    [Description("{0} precisa ser preenchido.")]
    Required,

    [Description("Campo {0} permite {1} caracteres.")]
    InvalidLength,

    [Description("Campo {0} está no formato inválido, deveria ser: {1}.")]
    InvalidFormat,

    [Description("{0} não existe.")]
    DoesNotExist
}
