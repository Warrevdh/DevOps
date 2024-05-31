namespace Shared.Users.Doctors.Specializations;
public abstract class SpecializationResult
{
	public class Index
	{
		public IEnumerable<SpecializationDto.Index>? Specializations { get; set; }
		public int TotalAmount { get; set; }
	}
}
