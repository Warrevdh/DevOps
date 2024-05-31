using Domain.Users.Doctors;

namespace Domain.Users.Employees;

public class Group : Entity
{
	private string name = default!;
	public string Name
	{
		get => name;
		set => name = Guard.Against.NullOrWhiteSpace(value);
	}

	private int sequence = default!;
	public int Sequence
	{
		get => sequence;
		set => sequence = Guard.Against.Negative(value);
	}

	private readonly List<Employee> employees = new();
	public IReadOnlyCollection<Employee> Employees => employees.AsReadOnly();

	//Database constructor
	private Group() { }

	public Group(string name, int sequence)
	{
		Name = name;
		Sequence = sequence;
	}

	public void Employee(Employee employee)
	{
		Guard.Against.Null(employee, nameof(employee));

		if (employees.Contains(employee))
		{
			throw new ApplicationException($"{nameof(Employee)} '{Name}' already contains this employee.");
		}
		employees.Add(employee);
	}

	public void RemoveEmployee(Employee employee)
	{
		Guard.Against.Null(employee, nameof(employee));

		if (!employees.Contains(employee))
		{
			throw new ApplicationException($"{nameof(Employee)} '{Name}' does not contain this employee.");
		}
		employees.Remove(employee);
	}
}
