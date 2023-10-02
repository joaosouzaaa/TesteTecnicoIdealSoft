using Microsoft.AspNetCore.Mvc;
using TesteTecnicoIdealSoft.API.Constants.RouteConstants;
using TesteTecnicoIdealSoft.API.ControllersAttributes;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Request.Person;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Interfaces.Services;

namespace TesteTecnicoIdealSoft.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

	public PersonController(IPersonService personService)
	{
		_personService = personService;
	}

	[HttpPost(PersonRouteConstants.AddPerson)]
	[CommandsController]
    public async Task<bool> AddAsync([FromBody] PersonSaveRequest personSaveRequest) =>
		await _personService.AddAsync(personSaveRequest);

	[HttpPut(PersonRouteConstants.UpdatePerson)]
	[CommandsController]
	public async Task<bool> UpdateAsync([FromBody] PersonUpdateRequest personUpdateRequest) =>
		await _personService.UpdateAsync(personUpdateRequest);

    [HttpDelete(PersonRouteConstants.DeletePerson)]
    [CommandsController]
    public async Task<bool> DeleteAsync([FromQuery] int id) =>
        await _personService.DeleteAsync(id);

    [HttpGet(PersonRouteConstants.GetAllPeople)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PersonResponse>> GetAllAsync() =>
        await _personService.GetAllAsync();
}
