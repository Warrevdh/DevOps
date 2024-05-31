using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.EyeConditions;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.EyeConditions;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class SymptomController : ControllerBase
{
    private readonly ISymptomService symptomService;

    public SymptomController(ISymptomService symptomService)
    {
        this.symptomService = symptomService;
    }

    [SwaggerOperation("Returns a list of all Symptoms available in the Oogarts catalog.")]
    [HttpGet, AllowAnonymous]
    public async Task<SymptomResult.Index> GetIndex([FromQuery] SymptomRequest.Index request)
    {
        return await symptomService.GetIndexAsync(request);
    }

    [SwaggerOperation("Creates new Symptom.")]
    [HttpPost]
    public async Task<IActionResult> Create(SymptomDto.Mutate model)
    {
        var creationId = await symptomService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), creationId);
    }
    [SwaggerOperation("Edits an existing symptom.")]
    [HttpPut("{symptomId}")]
    public async Task<IActionResult> Edit(long symptomId, SymptomDto.Mutate model)
    {
        await symptomService.EditAsync(symptomId, model);
        return NoContent();
    }

    [SwaggerOperation("Deletes an existing symptom.")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await symptomService.DeleteAsync(id);
        return NoContent();
    }
}

