namespace Shared.Appointments.Timeslots;

public abstract class TimeslotRequest
{
	public class Index
	{
		public string? Date { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 50;
	}
}
