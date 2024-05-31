namespace Shared.Appointments.Timeslots;

public abstract class TimeslotResult
{
	public class Index
	{
		public IEnumerable<TimeslotDto.Index>? Timeslots { get; set; }
		public int TotalAmount { get; set; }
	}
}
