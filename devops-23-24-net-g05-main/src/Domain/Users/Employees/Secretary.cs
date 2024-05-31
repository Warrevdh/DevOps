using Domain.Users.Employees;
using Domain.Users.Doctors;

namespace Domain.Users.Employees;

public class Secretary : Employee
{
	//Database constructor
	private Secretary() { }

	public Secretary(string firstname, string lastname, DateTime birthdate, string phonenumber, string email, Group group, string image)
	{
		FirstName = firstname;
		LastName = lastname;
		Birthdate = birthdate;
		Email = email;
		PhoneNumber = phonenumber;
		Group = group;
		Image = image;
	}
}
