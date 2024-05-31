using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Users.Doctors;
using Shared.EyeConditions;
using Shared.Users.Doctors.Availabilities;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Doctors;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AvailabilityController : ControllerBase
{

	private readonly IAvailabilityService availabilityService;

	public AvailabilityController(IAvailabilityService availabilityService)
	{
		this.availabilityService = availabilityService;
	}

	[SwaggerOperation("Returns a list of availabilities.")]
	[HttpGet, AllowAnonymous]
	public async Task<AvailabilityResult.Index> GetIndex([FromQuery] AvailabilityRequest.Index request)
	{
		return await availabilityService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of availabilities for a given employee.")]
	[HttpGet("Employee/{employeeId}"), AllowAnonymous]
	public async Task<AvailabilityResult.Index> GetAvailabilitiesFromEmployee([FromQuery] AvailabilityRequest.Index request, long employeeId)
	{
		return await availabilityService.GetAvailibilitiesFromEmployeeAsync(request, employeeId);
	}

    [SwaggerOperation("Creates a new availibility")]
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Create(AvailabilityDto.Mutate model)
    {
        var creationId = await availabilityService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), creationId);
    }
}