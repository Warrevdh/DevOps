using Microsoft.AspNetCore.Components;

namespace Client.ClientComponents;

public partial class ConfirmationPopUp
{
	[Parameter] public string? ObjectHeader { get; set; }
	[Parameter] public string? ObjectBody { get; set; }
	[Parameter] public string? ConfirmButtonContent { get; set; }
	[Parameter] public bool Open { get; set; }
	[Parameter] public EventCallback ToggleClose { get; set; }
	[Parameter] public EventCallback RequestConfirm { get; set; }

	private void ConfirmRequest()
	{
		RequestConfirm.InvokeAsync();
	}

	private void CancelRequest()
	{
		ToggleClose.InvokeAsync();
	}
}
