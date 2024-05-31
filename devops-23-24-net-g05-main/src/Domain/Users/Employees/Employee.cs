using Domain.Users.Employees;
using Domain.Users.Employees.Availabilities;
using System.Text.Json.Serialization;

namespace Domain.Users.Doctors;

public abstract class Employee : Entity {

	private string first_name = default!;
	public string FirstName
	{
		get => first_name;
		set => first_name = Guard.Against.NullOrWhiteSpace(value, nameof(first_name));
	}

	private string last_name = default!;
	public string LastName
	{
		get => last_name;
		set => last_name = Guard.Against.NullOrWhiteSpace(value, nameof(last_name));
	}
    private string image = default!;
    public string Image
    {
        get => image;
        set => image = Guard.Against.NullOrWhiteSpace(value, nameof(image));
    }
    private DateTime birthdate = default!;
	public DateTime Birthdate
	{
		get => birthdate;
		set => birthdate = Guard.Against.Null(value, nameof(birthdate));
	}

    private string phoneNumber = default!;
    public string PhoneNumber
    {
        get => phoneNumber;
        set => phoneNumber = Guard.Against.NullOrWhiteSpace(value, nameof(last_name));
    }

	private string email = default!;
	public string Email
	{
		get => email;
		set => email = Guard.Against.NullOrWhiteSpace(value, nameof(email));
	}

    private readonly List<Availability> availabilities = new();
    public IReadOnlyCollection<Availability> Availabilities => availabilities.AsReadOnly();

	private Group group = default!;
	public Group Group
	{
		get => group;
		set => group = Guard.Against.Null(value, nameof(value));
	}

	private Bio? bio = default!;
	public Bio? Bio
	{
		get => bio;
		set => bio = value;
	}

	//Database constructor????
	protected Employee() { }

	public void Availability(Availability availability)
	{
		Guard.Against.Null(availability, nameof(availability));

		if (availabilities.Exists(a => a.StartDate.Date.Equals(availability.StartDate.Date)))
		{
			throw new ApplicationException($"{nameof(Availability)} '{FirstName}' '{LastName}' already contains an availability for this day");
		}
		availabilities.Add(availability);
	}
}