using Client.Admin.Components.Team;
using Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using NPOI.SS.Formula.Functions;
using Shared.Users.Doctors.Employees;

namespace Client.Team
{
    public partial class TeamEdit
    {
        private IBrowserFile? image;

        private EmployeeDto.Mutate employee = new();
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IStorageService StorageService { get; set; }
        [Inject] public TeamService TeamService { get; set; } = default!;
        [Parameter] public long employeeId { get; set; }
        string employeeEmail;
        public string profileSrc;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        EmployeeDto.Mutate.Validator validator = new EmployeeDto.Mutate.Validator();

        protected override async Task OnInitializedAsync()
        {
            employee = await EmployeeService.GetMutateAsync(employeeId);
            employeeEmail = employee.Email;
            employee.Function = "d";
            profileSrc = employee.Image;
            base.OnInitializedAsync();

        }
        public async Task EditEmployeeAsync()
        {
            var results = validator.Validate(employee);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

                // Display all error messages in a single alert
                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                await EmployeeService.EditAsync(employeeId, employee);
                if (employeeEmail != employee.Email)
                {
                    UpdateUserEmailAuth(employeeEmail, employee.Email);
                }
                NavigationManager.NavigateTo($"Team/{employeeId}");
            }
        }

        private async void UpdateUserEmailAuth(string oldEmail, string newEmail)
        {
            await TeamService.UpdateUser(oldEmail, newEmail);
        }
    }
}
