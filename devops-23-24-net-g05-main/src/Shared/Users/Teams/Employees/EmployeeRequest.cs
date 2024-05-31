namespace Shared.Users.Doctors.Employees;

public abstract class EmployeeRequest
{
	public class Index
	{
		public string? SearchName { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
