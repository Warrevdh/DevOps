using Microsoft.AspNetCore.Components;
using Shared.Users.Teams.Groups;

namespace Client.Team.Components;

public partial class GroupEdit
{
    [Inject] public IGroupService GroupService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<GroupDto.Index>? groups;

    protected override async Task OnParametersSetAsync()
    {
        var groupsResp = await GroupService.GetIndexAsync(new GroupRequest.Index());
        groups = groupsResp.Groups;
    }

    private async Task EditGroupAsync()
    {
        foreach(var group in groups)
        {
            await GroupService.EditAsync(group.Id, new GroupDto.Mutate
            {
                Name = group.Name,
                Sequence = (int)group.Sequence,
            });
        }
        NavigationManager.NavigateTo("Team", true);
    }
}
