using Microsoft.AspNetCore.Components;

namespace Client.Admin.Components.Availabilities.Components;

public partial class TimeSlotAdmin
{
    [Parameter] public DateTime Time { get; set; }
    [Parameter] public TimeSpan Duration { get; set; }

	public void HandleTimeslotClick()
	{

	}
}
