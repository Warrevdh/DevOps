using FluentValidation;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Teams.Biographies;
using Shared.Users.Teams.Groups;

namespace Shared.Users.Doctors.Employees;

public abstract class EmployeeDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
		public GroupDto.Index? Group { get; set; }
	}

	public class Detail
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Image { get; set; }
        public DateTime Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public GroupDto.Index? Group { get; set; }
		public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
	}

	public class Mutate
	{
		public long? Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Image { get; set; }
        public DateTime? Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public string? Type { get; set; }
		public GroupDto.Index? Group { get; set; }
		public string? Function { get; set; }

		public class Validator : AbstractValidator<Mutate>
		{
			public Validator()
			{
				RuleFor(x => x.Firstname).NotEmpty().MaximumLength(75).WithMessage("Voornaam mag niet leeg zijn.");
				RuleFor(x => x.Lastname).NotEmpty().MaximumLength(75).WithMessage("Achternaam mag niet leeg zijn.");
				RuleFor(x => x.Birthdate).NotNull().WithMessage("Geboortedatum mag niet leeg zijn.");
				RuleFor(x => x.Email).NotEmpty().MaximumLength(100).WithMessage("Email mag niet leeg zijn.");
				RuleFor(x => x.Phonenumber).MaximumLength(15).WithMessage("Telefoonnummer mag niet leeg zijn.");
                RuleFor(x => x.Image).NotNull().WithMessage("Afbeelding mag niet leeg zijn.");
                RuleFor(x => x.Group).NotNull().WithMessage("Groep mag niet leeg zijn.");
                RuleFor(x => x.Function).NotNull().WithMessage("Functie mag niet leeg zijn.");
            }
		}
	}

    public class MutateAvailabilityDaily
    {
		public AvailabilityDto.Mutate Availability { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> Timeslots { get; set; } = default!;

        public class Validator : AbstractValidator<MutateAvailabilityDaily>
        {
            public Validator()
            {
				RuleFor(x => x.Availability).NotNull().NotEmpty();
				RuleFor(x => x.Timeslots).NotNull();
            }
        }
    }

    public class MutateAvailabilityWeekly
    {
		public AvailabilityDto.Mutate Availability { get; set; } = default!;
		public IEnumerable<TimeslotDto.Mutate> TimeslotsMo { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsTu { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsWe { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsTh { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsFr { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsSa { get; set; } = default!;
        public IEnumerable<TimeslotDto.Mutate> TimeslotsSu { get; set; } = default!;

        public class Validator : AbstractValidator<MutateAvailabilityWeekly>
        {
            public Validator()
            {
				RuleFor(x => x.Availability).NotNull();
                RuleFor(x => x.TimeslotsMo).NotNull();
                RuleFor(x => x.TimeslotsTu).NotNull();
                RuleFor(x => x.TimeslotsWe).NotNull();
                RuleFor(x => x.TimeslotsTh).NotNull();
                RuleFor(x => x.TimeslotsFr).NotNull();
                RuleFor(x => x.TimeslotsSa).NotNull();
                RuleFor(x => x.TimeslotsSu).NotNull();
            }
        }
    }
}
