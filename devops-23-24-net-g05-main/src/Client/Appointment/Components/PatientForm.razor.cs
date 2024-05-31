using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Users.Patients;
using Radzen;
using System;
using Shared.Appointments;

namespace Client.Appointment.Components; 

public partial class PatientForm {

	[Parameter] public AppointmentDto.Mutate NewAppointment { get; set; } = default!;
	[Parameter] public Action? OnNextButtonClick { get; set; }
    
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IAppointmentService AppointmentService { get; set; } = default!;
        
	private AppointmentDto.Mutate.MutateValidator validator = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        NewAppointment.Patient = new PatientDto.Mutate();
    }

    private async Task CreateAppointmentAsync() {
		var results = validator.Validate(NewAppointment);

		if (!results.IsValid) {
            var errorMessage = "Alle velden moeten ingevuld zijn";
            await JSRuntime.InvokeVoidAsync("alert", errorMessage);

        } else {
			OnNextButtonClick?.Invoke();

            long rep = await AppointmentService.CreateAsync(NewAppointment);
            Console.WriteLine(rep);
			Console.WriteLine($"Afspraak gemaakt voor {NewAppointment.Patient.FirstName} {NewAppointment.Patient.LastName}, Email: {NewAppointment.Patient.Email}, Geboortedatum: {NewAppointment.Patient.BirthDate}");
            Console.WriteLine($"Bij dokter met id: {NewAppointment.Employee.Id}");
            Console.WriteLine($"Op Timeslot {NewAppointment.Timeslot.Datetime} met duur {NewAppointment.Timeslot.Duration}");
            NavigationManager.NavigateTo("/Afspraak/Confirm");
		}
	}

    public void Reset()
    {
        NavigationManager.NavigateTo($"Afspraak", true);
    }
}