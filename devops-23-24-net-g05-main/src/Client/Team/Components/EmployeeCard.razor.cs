using Microsoft.AspNetCore.Components;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Groups;
using System;

namespace Client.Team.Components;

public partial class EmployeeCard
{
	[Parameter] public EmployeeDto.Index Employee { get; set; } = default!;
	[Parameter] public string ImageUrl { get; set; } = default!;
	[Parameter] public bool Admin { get; set; }
	[Inject] public IGroupService GroupService { get; set; } = default!;

	[Parameter] public EventCallback<long> ToggleDelete { get; set; }
	[Parameter] public EventCallback<MoveEmployeeEventArgs> SelectMoveEmployee { get; set; }

	private IEnumerable<GroupDto.Index>? groups;

	public class MoveEmployeeEventArgs
	{
		public long EmployeeId { get; set; }
		public string GroupId { get; set; }
	}

	//[Parameter] public IEnumerable<GroupDto.Index>? Groups { get; set; } = default!;
	//[Parameter] public EventCallback<long> OnGroupChanged { get; set; }

	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

	protected override async Task OnParametersSetAsync()
	{

		GroupRequest.Index requestG = new()
		{
		};

		var respGroup = await GroupService.GetIndexAsync(requestG);
		groups = respGroup.Groups;
	}

	private void NavigateToDetail()
	{
		NavigationManager.NavigateTo($"Team/{Employee.Id}");
	}

	private void RequestDeleteEmployee()
	{
		ToggleDelete.InvokeAsync(Employee.Id);
	}

	// ========== Group Select ========== //
	private void HandleOnchange(ChangeEventArgs args)
	{

		var eventArgs = new MoveEmployeeEventArgs
		{
			EmployeeId = Employee.Id,
			GroupId = args.Value?.ToString()
		};

		SelectMoveEmployee.InvokeAsync(eventArgs);
	}
}
