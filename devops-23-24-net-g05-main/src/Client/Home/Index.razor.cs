using Microsoft.AspNetCore.Components;

namespace Client.Home;

public partial class Index
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private void NavigateAppointments()
    {
        NavigationManager.NavigateTo("/Afspraak");
    }
}
