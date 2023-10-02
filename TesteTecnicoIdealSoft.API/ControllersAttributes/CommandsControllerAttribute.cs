using Microsoft.AspNetCore.Mvc;
using TesteTecnicoIdealSoft.API.Settings.NotificationSettings;

namespace TesteTecnicoIdealSoft.API.ControllersAttributes;

[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class CommandsControllerAttribute : Attribute
{
}
