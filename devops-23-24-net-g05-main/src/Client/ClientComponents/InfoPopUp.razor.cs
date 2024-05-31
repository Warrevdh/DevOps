using Microsoft.AspNetCore.Components;

namespace Client.ClientComponents;

public partial class InfoPopUp
{
    [Parameter] public string? Message { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback ToggleClose { get; set; }

    private void ClosePopUp()
    {
        ToggleClose.InvokeAsync();
    }
}
