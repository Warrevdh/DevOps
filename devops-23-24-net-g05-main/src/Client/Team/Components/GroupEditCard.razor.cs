using Microsoft.AspNetCore.Components;
using Shared.Users.Teams.Groups;

namespace Client.Team.Components;

public partial class GroupEditCard
{
	[Parameter] public GroupDto.Index Group { get; set; } = default!;
}
