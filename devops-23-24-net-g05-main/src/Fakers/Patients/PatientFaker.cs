using Domain.Users.Patients;

namespace Fakers.Patients;
public class PatientFaker : EntityFaker<Patient>
{
	public PatientFaker(string locale = "nl") : base(locale)
	{

	}
}
