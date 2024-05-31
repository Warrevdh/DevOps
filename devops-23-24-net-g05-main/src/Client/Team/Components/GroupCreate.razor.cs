using Microsoft.AspNetCore.Components;
using Shared.Users.Teams.Groups;

namespace Client.Team.Components;

public partial class GroupCreate
{
    private readonly GroupDto.Mutate group = new();
    [Inject] public IGroupService GroupService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private async Task CreateGroupAsync()
    {
        long groupId = await GroupService.CreateAsync(group);
        Console.WriteLine("Group created");
        NavigationManager.NavigateTo("Team");
    }
}
