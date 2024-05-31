using Microsoft.AspNetCore.Components;

namespace Client.ClientComponents;

public partial class DeletePopUp
{
	[Parameter] public string? ObjectName { get; set; }
	[Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback ToggleClose { get; set; }
    [Parameter] public EventCallback RequestDelete { get; set; }

    private void ConfirmDelete()
    {
        RequestDelete.InvokeAsync();
    }

    private void CancelDeleteRequest()
	{
        ToggleClose.InvokeAsync();
    }
}
