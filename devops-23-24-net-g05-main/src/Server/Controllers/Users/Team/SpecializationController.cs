using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Users.Doctors;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Specializations;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Doctors;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class SpecializationController : ControllerBase
{
	private readonly ISpecializationService specializationService;

	public SpecializationController(ISpecializationService specializationService)
	{
		this.specializationService = specializationService;
	}

	[SwaggerOperation("Returns a list of specializations.")]
	[HttpGet, AllowAnonymous]
	public async Task<SpecializationResult.Index> GetIndex([FromQuery] SpecializationRequest.Index request)
	{
		return await specializationService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of specializations from a given doctor.")]
	[HttpGet("Doctor/{doctorId}"), AllowAnonymous]
	public async Task<SpecializationResult.Index> GetSpecializationsFromDoctor(long doctorId)
	{
		return await specializationService.GetSpecializationsFromDoctorAsync(doctorId);
	}
}
