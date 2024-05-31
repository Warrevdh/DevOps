using Microsoft.AspNetCore.Components;

namespace Client.ClientComponents
{

    public partial class MainLayout
    {
        bool isAdminPage = false;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized()
        {
            isAdminPage = NavigationManager.Uri.ToLower().Contains("/admin");
        }
    }
}
