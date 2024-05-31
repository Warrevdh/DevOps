namespace Shared.Users.Doctors.Employees;

public abstract class EmployeeResult
{
	public class Index
	{
		public IEnumerable<EmployeeDto.Index>? Employees { get; set; }
		public int TotalAmount { get; set; }
	}
    public class Create
    {
        public long EmployeeId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string UploadUri { get; set; } = default!;
    }
}
