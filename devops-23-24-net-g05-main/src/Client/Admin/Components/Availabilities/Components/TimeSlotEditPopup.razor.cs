using Microsoft.AspNetCore.Components;
using Shared.Appointments.Timeslots;

namespace Client.Admin.Components.Availabilities.Components;

public partial class TimeSlotEditPopup
{
    //[Parameter] public string? Message { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback ToggleClose { get; set; }
    [Parameter] public EventCallback SaveTimeslot { get; set; }
    [Parameter] public TimeslotDto.Mutate Timeslot { get; set; } = default!;

    private DateTime hour = default!;
    private int duration;

    protected override void OnParametersSet()
    {
        hour = Timeslot.Datetime;
        duration = Timeslot.Duration.Minutes;
    }

    private void ClosePopUp()
    {
        Timeslot.Datetime = hour;
        Timeslot.Duration = new TimeSpan(0, duration, 0);
        ToggleClose.InvokeAsync();
    }
}
