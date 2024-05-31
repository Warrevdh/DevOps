using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NPOI.SS.Formula.Functions;
using Radzen.Blazor;
using Shared.Users;
using Shared.Users.Doctors.Employees;
using System.Security.Claims;

namespace Client.Admin.Components.Team
{
    public partial class TeamGrid
    {
        RadzenDataGrid<EmployeeDto.Index> employeeGrid;
        List<EmployeeDto.Index>? employees;
        IEnumerable<UserDto.Index> users;
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public TeamService TeamService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        private EmployeeDto.Index deleteRequest = null;
        private bool open = false;
        private ClaimsPrincipal user;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            user = authState.User;

            EmployeeResult.Index res = await EmployeeService.GetIndexAsync(new EmployeeRequest.Index());
            users = await TeamService.GetUsers();
            employees = res.Employees.ToList();
        }

        public async Task ChangeRole(string email)
        {
            var userId = users.FirstOrDefault(x => x.Email == email).UserId;
            await TeamService.ChangeRole(userId);
            await employeeGrid.Reload();
        }

        public string GetRole(string email)
        {
            return users.SingleOrDefault(x => x.Email == email).Role;
        }

        public void AddEmployee()
        {
            NavigationManager.NavigateTo("Team/nieuw");
        }

        async Task EditRow(EmployeeDto.Index employee)
        {
            long employeeId = employee.Id;
            NavigationManager.NavigateTo($"/Team/edit/{employeeId}");
        }

        private void CloseDeletePopUp()
        {
            open = !open;
            deleteRequest = null;
        }

        async Task DeleteRow(EmployeeDto.Index employee)
        {
            open = !open;
            deleteRequest = employee;
        }

        private async Task ConfirmDelete()
        {
            var userId = users.FirstOrDefault(x => x.Email == deleteRequest.Email).UserId;
            await EmployeeService.DeleteAsync(deleteRequest.Id);
            open = !open;
            employees.Remove(deleteRequest);
            await TeamService.DeleteUser(userId);
            await employeeGrid.Reload();
        }
    }
}
