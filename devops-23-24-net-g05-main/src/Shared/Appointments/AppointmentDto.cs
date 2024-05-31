using FluentValidation;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Employees;
using Shared.Users.Patients;

namespace Shared.Appointments;
public abstract class AppointmentDto
{
    public class Index
    {
        public long Id { get; set; }
        public TimeslotDto.Index? Timeslot { get; set; }
    }

    public class Detail
    {
        public long Id { get; set; }
        public PatientDto.Detail? Patient { get; set; }
        public TimeslotDto.Index? Timeslot { get; set; }
        public string? Reason { get; set; }
        public string? Note { get; set; }
	}

    public class UltraDetail
    {
        public long Id { get; set; }
        public PatientDto.Detail? Patient { get; set; }
		public TimeslotDto.Detail? Timeslot { get; set; }
		public string? Reason { get; set; }
		public string? Note { get; set; }
	}

    public class Mutate
    {
        public EmployeeDto.Index Employee { get; set; } = default!;
        public PatientDto.Mutate Patient { get; set; } = default!;
        public TimeslotDto.Index Timeslot { get; set; } = default!;
        public string? Reason { get; set; }
        public string? Note { get; set; }

        public class MutateValidator : AbstractValidator<Mutate>
        {
            public MutateValidator()
            {
                RuleFor(x => x.Employee).NotEmpty();
                RuleFor(x => x.Patient).NotEmpty().SetValidator(new PatientDto.Mutate.Validator());
                RuleFor(x => x.Timeslot).NotEmpty();
                RuleFor(x => x.Reason).NotEmpty();
                RuleFor(x => x.Note).NotEmpty();
            }
    }
    }

    
    //TODO DELETE
	public class Create
	{
        public int PatientId { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime? Date { get; set; }
        public string? ExtraInfo { get; set; }
	}
}