using Microsoft.AspNetCore.Components;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Biographies;

namespace Client.Team;

public partial class DetailEmployee
{
    [Parameter] public long Id { get; set; }
    [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
    [Inject] public IBioService BioService { get; set; } = default!;
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

    
    private bool admin = true;
    private EmployeeDto.Detail? employee;
    private BioDto.Index? bio;
    private bool IsExpanded = false;

    private string ContentClass => IsExpanded ? "accordion-content expanded" : "accordion-content";

    private string ChevronImage => IsExpanded ? "./images/chevron-up.svg" : "./images/chevron-down.svg";

    private void ToggleAccordion()
    {
        IsExpanded = !IsExpanded;
    }


    protected override async Task OnParametersSetAsync()
	{
        //TODO WEEKLY HOURS
        await GetEmployeeAsync();
        await GetBioAsync();
	}

    private async Task GetEmployeeAsync()
    {
        employee = await EmployeeService.GetDetailAsync(Id);
        Console.WriteLine(employee.Availabilities is null);
    }

    private async Task GetBioAsync()
    {
        try
        {
            bio = await BioService.GetDetailAsync(Id);
        } catch (Exception ex)
        {
            bio = null;
        }
        
    }

    private void NavigateToEditEmployee()
    {
        if (admin)
        {
            NavigationManager.NavigateTo($"Team/edit/{Id}");
        }
    }
    private void GoBack()
    {
        NavigationManager.NavigateTo($"Team");
    }
}
