using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Domain.Users.Patients;
public class Patient : Entity {

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

    private DateTime birth_date = default!;
    public DateTime BirthDate
    {
        get => birth_date;
        set => birth_date = Guard.Against.Null(value, nameof(birth_date));
    }

    private string phone_number = default!;
    public string PhoneNumber
    {
        get => phone_number;
        set => phone_number = Guard.Against.NullOrWhiteSpace(value, nameof(last_name));
    }

    private string email = default!;
    public string Email
    {
        get => email;
        set => email = Guard.Against.NullOrWhiteSpace(value,nameof(email));
    }

    //Database constructor
    private Patient() { }
    public Patient(string firstname, string lastName, DateTime birthdate, string phoneNumber, string email)
    {
        FirstName = firstname;
        LastName = lastName;
        BirthDate = birthdate;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}
