using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Groups;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Team;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class EmployeeController : ControllerBase
{
	private readonly IEmployeeService employeeService;

	public EmployeeController(IEmployeeService employeeService)
	{
		this.employeeService = employeeService;
	}

	[SwaggerOperation("Returns a list of employees.")]
	[HttpGet, AllowAnonymous]
	public async Task<EmployeeResult.Index> GetIndex([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of doctors.")]
	[HttpGet("Doctors"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetDoctors([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetDoctorsIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of assistants.")]
	[HttpGet("Assistants"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetAssistants([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetAssistantsIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of secretary members.")]
	[HttpGet("Secretary"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetSecretary([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetSecretaryIndexAsync(request);
	}

	[SwaggerOperation("Returns a specific employee.")]
	[HttpGet("{employeeId}"), AllowAnonymous]
	public async Task<EmployeeDto.Detail> GetDetail(long employeeId)
	{
		return await employeeService.GetDetailAsync(employeeId);
	}

	[SwaggerOperation("Edites an existing employee.")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Edit(long id, EmployeeDto.Mutate model)
	{
		await employeeService.EditAsync(id, model);
		return NoContent();
	}

	[SwaggerOperation("Changes group for an employee.")]
	[HttpPut("{employeeId}/{groupId}")]
	public async Task<IActionResult> ChangeGroup(long employeeId, long groupId)
	{
		await employeeService.ChangeGroupAsync(employeeId, groupId);
		return NoContent();
	}

    [SwaggerOperation("Deletes an existing employee.")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await employeeService.DeleteAsync(id);
        return NoContent();
    }

    [SwaggerOperation("Creates a new employee.")]
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Create(EmployeeDto.Mutate model)
    {
        var employeeId = await employeeService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), employeeId);
    }

    [SwaggerOperation("Add availabilities and timeslots on Daily basis.")]
    [HttpPut("{employeeId}/Availability/Daily"), AllowAnonymous]
    public async Task<IActionResult> AddAvailabilityDaily(long employeeId, EmployeeDto.MutateAvailabilityDaily model)
    {
        await employeeService.AddDailyAvailabilitiesAsync(employeeId, model);
        return NoContent();
    }

	[SwaggerOperation("Add availabilities and timeslots on Weekly basis.")]
	[HttpPut("{employeeId}/Availability/Weekly"), AllowAnonymous]
	public async Task<IActionResult> AddAvailabilityWeekly(long employeeId, EmployeeDto.MutateAvailabilityWeekly model)
	{
		await employeeService.AddWeeklyAvailabilitiesAsync(employeeId, model);
		return NoContent();
	}
}
