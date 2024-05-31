using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Radzen;
using Shared.Appointments;
using Shared.Appointments.Timeslots;
using Shared.Users.Team.Doctors;
using System;

namespace Client.Admin.Components.Availabilities;

public partial class AvailabilityGrid
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject] public ITimeslotService TimeslotService { get; set; } = default!;
    [Inject] public IDoctorService DoctorService { get; set; } = default!;
    [Inject] public IAppointmentService AppointmentService { get; set; } = default!;

    private DoctorDto.Detail currentUser = new();
    private IEnumerable<AppointmentDto.UltraDetail> currentAppointments = default!;

	private AppointmentDto.UltraDetail? currentAppointment = default!;
	private bool showPop = false;

	protected override async Task OnParametersSetAsync()
    {
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

        var email = user.Identity!.Name;

        var resp = await DoctorService.GetByEmailAsync(email!);
        currentUser = resp;

        var respA = await AppointmentService.GetAllDetailAppointmentsAsync(currentUser.Id);
        currentAppointments = respA.Appointments;
	}

    public void CreateAvailability()
    {
        //TODO ADMIN
        NavigationManager.NavigateTo($"/Team/Beschikbaarheid");
    }

	private void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<AppointmentDto.UltraDetail> args)
	{
		var copy = new AppointmentDto.UltraDetail
		{
			Timeslot = args.Data.Timeslot,
			Patient = args.Data.Patient,
			Reason = args.Data.Reason,
			Note = args.Data.Note,
		};

		currentAppointment = copy;
		showPop = true;
		//var data = await DialogService.OpenAsync<EditAppointmentPage>("Edit Appointment", new Dictionary<string, object> { { "Appointment", copy } });

		//await scheduler.Reload();
	}

	public void ClosePopup()
	{
		showPop = false;
		//StateHasChanged();
	}
}
