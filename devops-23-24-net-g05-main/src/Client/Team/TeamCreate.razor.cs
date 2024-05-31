using Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Users.Doctors.Employees;
using Microsoft.JSInterop;
using Shared.Users.Teams.Groups;
using Client.Admin.Components.Team;
using Shared.Users;

namespace Client.Team
{
    public partial class TeamCreate
    {
        private IBrowserFile? image;
        private EmployeeDto.Mutate employee = new();
        [Inject] public TeamService TeamService { get; set; } = default!;
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IGroupService GroupService { get; set; }
        [Inject] public IStorageService StorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        public string[] teamFunction = { "Dokter", "Assistent", "Secretariaat" };
        public string chosenFunction;
        EmployeeDto.Mutate.Validator validator = new EmployeeDto.Mutate.Validator();

        public async Task CreateEmployeeAsync()
        {
            if (chosenFunction == "Dokter")
            {
                GroupRequest.Index req = new GroupRequest.Index()
                {
                    Name = "Oogartsen",
                };
                GroupResult.Index group = await GroupService.GetIndexAsync(req);
                employee.Group = group.Groups.FirstOrDefault();
            }
            else
            {
                GroupRequest.Index req = new GroupRequest.Index()
                {
                    
                };
                GroupResult.Index group = await GroupService.GetIndexAsync(req);
                employee.Group = group.Groups.FirstOrDefault();
            }

            employee.Function = chosenFunction;

            var results = validator.Validate(employee);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                EmployeeResult.Create result = await EmployeeService.CreateAsync(employee);
                await StorageService.UploadImageAsync(result.UploadUri, image!);
                AddToAuth(result);
                NavigationManager.NavigateTo($"Team/{result.EmployeeId}");
            }
        }

        private async void AddToAuth(EmployeeResult.Create employee)
        {
            UserDto.Mutate newUser = new UserDto.Mutate()
            {
                Email = employee.Email,
                Password = "Password123456"
            };
            await TeamService.CreateUser(newUser);
        }

        private void LoadImage(InputFileChangeEventArgs e)
        {
            image = e.File;
            employee.Image = image.ContentType;
        }
    }
}
