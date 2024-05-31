namespace Shared.Users.Doctors.Availabilities;
public abstract class AvailabilityRequest
{
	public class Index
	{
		public DateTime Date { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
