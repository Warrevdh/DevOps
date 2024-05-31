using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users.Teams.Biographies;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Team;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BioController : ControllerBase
{
	private readonly IBioService bioService;

	public BioController(IBioService bioService)
	{
		this.bioService = bioService;
	}

	[SwaggerOperation("Returns a specific biography.")]
	[HttpGet("{biographyId}"), AllowAnonymous]
	public async Task<BioDto.Index> GetDetail(long biographyId)
	{
		return await bioService.GetDetailAsync(biographyId);
	}

	[SwaggerOperation("Creates a new biography.")]
	[HttpPost, AllowAnonymous]
	public async Task<IActionResult> Create(BioDto.Mutate model)
	{
		var bioId = await bioService.CreateAsync(model);
		return CreatedAtAction(nameof(Create), bioId);
	}

	[SwaggerOperation("Edites an existing biography.")]
	[HttpPut("{biographyId}"), AllowAnonymous]
	public async Task<IActionResult> Edit(long biographyId, BioDto.Mutate model)
	{
		await bioService.EditAsync(biographyId, model);
		return NoContent();
	}

	[SwaggerOperation("Deletes an existing biography.")]
	[HttpDelete("{biographyId}"), AllowAnonymous]
	public async Task<IActionResult> Delete(long biographyId)
	{
		await bioService.DeleteAsync(biographyId);
		return NoContent();
	}
}
