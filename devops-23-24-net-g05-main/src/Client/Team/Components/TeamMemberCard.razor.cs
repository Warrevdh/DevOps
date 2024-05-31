using Microsoft.AspNetCore.Components;
using System;

namespace Client.Team.Components;

public partial class TeamMemberCard
{
	[Parameter] public string ImageUrl { get; set; } = default!;
	[Parameter] public string Function { get; set; } = default!;
	[Parameter] public string Name { get; set; } = default!;
	[Parameter] public int Id { get; set; }

	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

	private void NavigateToDetail()
	{
		Console.WriteLine(Id);
		NavigationManager.NavigateTo($"Team/{Id}");
	}

}
