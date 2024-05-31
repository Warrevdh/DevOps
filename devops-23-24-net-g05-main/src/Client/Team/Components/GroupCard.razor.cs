using Microsoft.AspNetCore.Components;
using Shared.Users.Teams.Groups;
using System.Text.RegularExpressions;

namespace Client.Team.Components;

public partial class GroupCard
{
	[Parameter] public GroupDto.Index Group { get; set; } = default!;
    [Parameter] public bool Admin { get; set; } = default!;
    [Parameter] public EventCallback<long> DeleteGroup { get; set; }


    private void RequestDeleteGroup()
    {
        DeleteGroup.InvokeAsync(Group.Id);
    }
}