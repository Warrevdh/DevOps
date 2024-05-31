namespace Shared.Users.Patients;

public abstract class PatientRequest
{
	public class Index
	{
		public string? SearchtermName { get; set; }
		public string? SearchtermEmail { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
