using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users.Teams.Groups;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Team;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GroupController : ControllerBase
{
	private readonly IGroupService groupService;

	public GroupController(IGroupService groupService)
	{
		this.groupService = groupService;
	}

	[SwaggerOperation("Returns a list of groups.")]
	[HttpGet, AllowAnonymous]
	public async Task<GroupResult.Index> GetIndex([FromQuery] GroupRequest.Index request)
	{
		return await groupService.GetIndexAsync(request);
	}

	[SwaggerOperation("Creates a new group.")]
	[HttpPost, AllowAnonymous]
	public async Task<IActionResult> Create(GroupDto.Mutate model)
	{
		var groupId = await groupService.CreateAsync(model);
		return CreatedAtAction(nameof(Create), groupId);
	}

	[SwaggerOperation("Deletes an existing group.")]
    [HttpDelete("{id}"), AllowAnonymous]
    public async Task<IActionResult> Delete(long id)
	{
		await groupService.DeleteAsync(id);
		return NoContent();
	}

	[SwaggerOperation("Edites an existing group.")]
	[HttpPut("{id}"), AllowAnonymous]
	public async Task<IActionResult> Edit(long id, GroupDto.Mutate model)
	{
		await groupService.EditAsync(id, model);
		return NoContent();
	}
}
