using Microsoft.AspNetCore.Components;

namespace Client.Appointment.Components { 
	public partial class ProgressBar {
		[Parameter] public int Active { get; set; } = default;
	}
}
