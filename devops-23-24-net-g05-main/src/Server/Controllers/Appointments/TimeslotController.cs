using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Appointments.Timeslots;
using Shared.Users.Teams.Groups;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Appointments;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class TimeslotController : ControllerBase
{
	private readonly ITimeslotService timeslotService;
	
	public TimeslotController(ITimeslotService timeslotService)
	{
		this.timeslotService = timeslotService;
	}

	[SwaggerOperation("Returns a list of timeslots.")]
	[HttpGet, AllowAnonymous]
	public async Task<TimeslotResult.Index> GetIndex([FromQuery] TimeslotRequest.Index request)
	{
		return await timeslotService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of timeslots for a specific doctor.")]
	[HttpGet("{doctorId}"), AllowAnonymous]
	public async Task<TimeslotResult.Index> GetTimeslotsFromDoctor([FromQuery] TimeslotRequest.Index request, long doctorId)
	{
		return await timeslotService.GetTimeslotsFromDoctorAsync(request, doctorId);
	}

    [SwaggerOperation("Creates a new timeslot.")]
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Create(TimeslotDto.Mutate model)
    {
        var timeslotId = await timeslotService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), timeslotId);
    }

    [SwaggerOperation("Deletes an existing timeslot.")]
    [HttpDelete("{id}"), AllowAnonymous]
    public async Task<IActionResult> Delete(long id)
    {
        await timeslotService.DeleteAsync(id);
        return NoContent();
    }

    [SwaggerOperation("Edites an existing timeslot.")]
    [HttpPut("{id}"), AllowAnonymous]
    public async Task<IActionResult> Edit(long id, TimeslotDto.Mutate model)
    {
        await timeslotService.EditAsync(id, model);
        return NoContent();
    }
}
