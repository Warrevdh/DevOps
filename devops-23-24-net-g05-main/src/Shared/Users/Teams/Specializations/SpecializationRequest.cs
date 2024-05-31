namespace Shared.Users.Doctors.Specializations;
public abstract class SpecializationRequest
{
	public class Index
	{
		public string? Searchterm { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
