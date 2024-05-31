using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users.Team.Doctors;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Team;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class DoctorController : ControllerBase
{
	private readonly IDoctorService doctorService;

	public DoctorController(IDoctorService doctorService)
	{
		this.doctorService = doctorService;
	}

	[SwaggerOperation("Returns a list of doctors.")]
	[HttpGet, AllowAnonymous]
	public async Task<DoctorResult.Index> GetIndex([FromQuery] DoctorRequest.Index request)
	{
		return await doctorService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a specific doctor.")]
	[HttpGet("{doctorId}"), AllowAnonymous]
	public async Task<DoctorDto.Detail> GetDetail(long doctorId)
	{
		return await doctorService.GetDetailAsync(doctorId);
	}

	[SwaggerOperation("Returns a specific doctor with email.")]
	[HttpGet("Email/{email}"), AllowAnonymous]
	public async Task<DoctorDto.Detail> GetDetailEmail(string email)
	{
		return await doctorService.GetByEmailAsync(email);
	}
}
