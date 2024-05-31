using Microsoft.AspNetCore.Components;
using Shared.Appointments;
using Shared.Appointments.Timeslots;

namespace Client.Admin.Components.Availabilities.Components;

public partial class DetailedTimeslot
{
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback ToggleClose { get; set; }
    [Parameter] public AppointmentDto.UltraDetail Appointment { get; set; } = default!;

    private string birthdate = "";

    protected override void OnParametersSet()
    {
        if (Appointment != null)
        {
            birthdate = Appointment.Patient!.BirthDate!.Value.ToString("dd-MM-yyyy");
        }
        
    }

    private void ClosePopUp()
    {
        //Timeslot.Datetime = hour;
        //Timeslot.Duration = new TimeSpan(0, duration, 0);
        ToggleClose.InvokeAsync();
    }
}
