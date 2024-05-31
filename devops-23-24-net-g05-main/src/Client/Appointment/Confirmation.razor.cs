using Microsoft.AspNetCore.Components;

namespace Client.Appointment;

public partial class Confirmation
{
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;
	private void HandleClick()
	{
		NavigationManager.NavigateTo("/");
	}
}
