namespace Shared.Users.Team.Doctors;

public abstract class DoctorRequest
{
	public class Index
	{
		public string? Name { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
