using FluentValidation;

namespace Shared.Users.Patients;
public abstract class PatientDto
{
    public class Index
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

	public class Detail
	{
		public long Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}

	public class Mutate
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public class Validator : AbstractValidator<Mutate> {
			public Validator() {
				RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("Uw voornaam mag maar 50 karakters lang zijn");
				RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("Uw achternaam mag maar 50 karakters lang zijn");
				RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Ongeldige geboortedatum");
				RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\d{10}$").WithMessage("Ongeldig gsmnr.");
				RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Ongeldig emailadres");
			}

			//private bool BeAtLeast18YearsOld(DateOnly? birthDate) {
			//	var today = DateOnly.FromDateTime(DateTime.Today);
			//	var age = today.Year - birthDate?.Year;
			//	var birthDateTime = birthDate?.ToDateTime();
			//	if (birthDateTime.DayOfYear > today.DayOfYear) {
			//		age--;
			//	}
			//	return age >= 18;
			//}
		}
    }
}
