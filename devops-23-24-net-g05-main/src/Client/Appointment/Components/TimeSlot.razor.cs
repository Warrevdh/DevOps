using Microsoft.AspNetCore.Components;
using Shared.Appointments.Timeslots;

namespace Client.Appointment.Components;

public partial class TimeSlot
{
    [Parameter] public TimeslotDto.Index Slot { get; set; } = default!;
    [Parameter] public EventCallback SelectTimeslot { get; set; }

    private void HandleClick()
    {
        SelectTimeslot.InvokeAsync(Slot);
    }
}
