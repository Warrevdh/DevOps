using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Groups;
using static Client.Team.Components.EmployeeCard;

namespace Client.Team;

public partial class Index
{
	[Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
	[Inject] public IEmployeeService EmployeeService { get; set; } = default!;
	[Inject] public IGroupService GroupService { get; set; } = default!;
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

	[Parameter, SupplyParameterFromQuery] public string? SearchName { get; set; }
	[Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
	[Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }

	private IEnumerable<GroupDto.Index>? groups;
	private IEnumerable<EmployeeDto.Index>? employees;

	private bool admin = false;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		
		if (authState.User.Identity!.Name is not null)
		{
			admin = true;
		}
	}

	// =========== Delete Employee =========== //
	private bool open = false;
	private long deleteEmployeeId = default;

	private void ToggleEmployeeDelete(long Id) // - When button pressed open popup
	{
		open = !open;
		deleteEmployeeId = Id;
	}

	private void CloseDeletePopUp() // - When close button pressed, close popup
	{
		open = false;
		deleteEmployeeId = default;
	}

	private async Task ConfirmDelete()
	{
		// - Delete Implementation goes here 
		await EmployeeService.DeleteAsync(deleteEmployeeId);

        open = false;
        deleteEmployeeId = default!;
        var respEmployees = await EmployeeService.GetIndexAsync(new EmployeeRequest.Index { });
        employees = respEmployees.Employees;
        StateHasChanged();
	}

	// =========== Move Employee =========== //
	private bool openMove = false;
	private long requestMoveEmployee = default;
	private long requestMoveGroup = default;

	private void RequestMoveEmployee(MoveEmployeeEventArgs args)
	{
		if (args != null && long.TryParse(args.GroupId, out long groupIdLong))
		{
			requestMoveEmployee = args.EmployeeId;
			requestMoveGroup = groupIdLong;
			openMove = !openMove;
		}
		else
		{
			Console.WriteLine("COnversion failed");
		}
	}

	private void CloseMoveInfo()
	{
		openMove = false;
		requestMoveEmployee = default;
		requestMoveGroup = default;
	}

	private async void ConfirmMoveEmployee()
	{
		await EmployeeService.ChangeGroupAsync(requestMoveEmployee, requestMoveGroup);
		NavigationManager.NavigateTo("Team");

		CloseMoveInfo();
		StateHasChanged();
	}

	// =========== Delete Group =========== //
	private bool openInfo = false;
	private bool openDelete = false;

	private long requestDeleteGroup = default;

	private void ToglleInfoPopUp()
	{ 
		openInfo = !openInfo; 
	}

	private void RequestDeleteGroup(long groupId)
	{
		foreach (var employee in employees)
		{
			if (employee.Group.Id == groupId)
			{
				openInfo = true;
				return;
			}
		}
		openDelete = !openDelete;
		requestDeleteGroup = groupId;
	}

	private void CancelDeleteGroup()
	{
		openDelete = false;
		requestDeleteGroup = default;
	}

	private async void ConfirmDeleteGroup()
	{
		try
		{
			await GroupService.DeleteAsync(requestDeleteGroup);
            var respGroup = await GroupService.GetIndexAsync(new GroupRequest.Index { });
            groups = respGroup.Groups;
            openDelete = false;
			requestDeleteGroup = default;
			StateHasChanged();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
	}




	protected override async Task OnParametersSetAsync()
	{
		EmployeeRequest.Index requestE = new()
		{
			SearchName = SearchName,
			Page = Page ?? 1,
			PageSize = PageSize ?? 25
		};

		GroupRequest.Index requestG = new()
		{
			Name = SearchName,
			Page = Page ?? 1,
			PageSize = PageSize ?? 25
		};

		var respEmployees = await EmployeeService.GetIndexAsync(requestE);
		employees = respEmployees.Employees;

		var respGroup = await GroupService.GetIndexAsync(requestG);
		groups = respGroup.Groups;
	}

	private void NavigateToCreateGroup()
	{
		if (admin)
		{
			NavigationManager.NavigateTo($"Team/Groep/Nieuw");
		}
	}

	private void NavigateToCreateEmployee()
	{
		if (admin)
		{
			NavigationManager.NavigateTo($"Team/nieuw");
		}
	}

	private void NavigateToEditGroup()
	{
		if (admin)
		{
			NavigationManager.NavigateTo($"Team/Groep/Edit");
		}
	}





	//Te veranderen met echte data
	private readonly List<string> Images = new List<string> {
        "../../images/doctor1.jpg", "../../images/doctor2.jpg", "../../images/doctor3.jpg", "../../images/doctor4.jpg", "../../images/doctor5.jpg", "../../images/doctor6.png", "../../images/doctor7.png" };
	//private readonly List<string> Function = new List<string> {
	//	"Oogarts (Algemeen)", "Cornea-specialist", "Cornea-specialist", "Orthoptist", "Orthoptist", "Orthoptist", "Cornea-specialist" };
	//private readonly List<string> Name = new List<string> {
	//	"Dr. Emily Patel", "Dr. Sarah Mitchell", "Dr. Laura Ramirez", "Dr. James Anderson", "Dr. Ramin Doker", "Dr. David Wilson", "Michael Chang" };
}
