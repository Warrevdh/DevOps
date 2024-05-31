using Client.Admin.Components;
using Client.Admin.Services;
using Client.ClientComponents;
using Microsoft.AspNetCore.Components;

namespace Client.Admin
{
    public partial class Admin 
    {
        public string ActiveItem { get; set; }
        [Parameter] public bool open { get; set; } = false;


        private void ToggleMenu()
        {
            open = !open;
        }
        private string GetBurgerMenuClass()
        {
            return open ? "nav-links open" : "nav-links";
        }

        private void SetActive(string active)
        {
            ActiveItem = active;
        }

    }
}
