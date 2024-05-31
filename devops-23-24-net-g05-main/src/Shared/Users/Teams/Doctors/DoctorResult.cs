namespace Shared.Users.Team.Doctors;

public abstract class DoctorResult
{
	public class Index
	{
		public IEnumerable<DoctorDto.Index>? Doctors { get; set; }
		public int TotalAmount { get; set; }
	}
}
