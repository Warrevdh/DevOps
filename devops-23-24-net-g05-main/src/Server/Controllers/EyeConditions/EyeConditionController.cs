using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.EyeConditions;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers.EyeConditions;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class EyeConditionController : ControllerBase
{
    private readonly IEyeConditionService eyeConditionService;

    public EyeConditionController(IEyeConditionService eyeConditionService)
    {
        this.eyeConditionService = eyeConditionService;
    }

	[SwaggerOperation("Returns a list of eyeconditions.")]
	[HttpGet, AllowAnonymous]
	public async Task<EyeConditionResult.Index> GetIndex([FromQuery] EyeConditionRequest.Index request)
	{        
		return await eyeConditionService.GetIndexAsync(request);
	}

	[SwaggerOperation("Creates a new employee")]
	[HttpPost]
	public async Task<IActionResult> Create(EyeConditionDto.Mutate model)
    {
		var creationId = await eyeConditionService.CreateAsync(model);
		return CreatedAtAction(nameof(Create), creationId);
	}

    [SwaggerOperation("Returns a specific eyecondition available in the eyeConditions catalog.")]
    [HttpGet("{Id}"), AllowAnonymous]
    public async Task<EyeConditionDto.Detail> GetDetail(long Id)
    {
        return await eyeConditionService.GetDetailAsync(Id);
    }

    [SwaggerOperation("Returns a specific eyecondition based on Id")]
    [HttpGet("GetMutate/{Id}"), AllowAnonymous]
    public async Task<EyeConditionDto.Mutate> GetMutate(long Id)
    {
        return await eyeConditionService.GetMutateAsync(Id);
    }

    [SwaggerOperation("Edits an existing eyecondition.")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(long id, EyeConditionDto.Mutate model)
    {
        await eyeConditionService.EditAsync(id, model);
        return NoContent();
    }

	[SwaggerOperation("Deletes an existing eyecondition.")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await eyeConditionService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetEyeCondition(int id)
    // {
    //     var eyeCondition = await _context.EyeConditions.FindAsync(id);
    //     if (eyeCondition == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(eyeCondition);
    // }
}
