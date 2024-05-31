using Domain.Users.Doctors;

namespace Domain.Users.Employees;

public class Bio : Entity
{
	private string info = default!;
	public string Info
	{
		get => info;
		set => info = Guard.Against.NullOrEmpty(value);
	}

	private Employee employee = default!;
	public Employee Employee
	{
		get => employee;
		set => employee = Guard.Against.Null(value);
	}

	//Database constructor
	private Bio() { }

	public Bio(string info)
	{
		Info = info;
	}
}
