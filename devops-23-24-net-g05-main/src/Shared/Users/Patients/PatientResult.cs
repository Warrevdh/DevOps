namespace Shared.Users.Patients;

public abstract class PatientResult
{
	public class Index
	{
		public IEnumerable<PatientDto.Index>? Patients { get; set; }
		public int TotalAmount { get; set; }
	}

	public class Create
	{
		public long PatientId { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public DateTime? Birthdate { get; set; }
		public string? Phonenumber { get; set; }
		public string? Email { get; set; }
	}
}