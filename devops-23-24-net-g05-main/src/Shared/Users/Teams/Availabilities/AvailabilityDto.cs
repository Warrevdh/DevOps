using FluentValidation;
using Shared.Users.Doctors.Employees;

namespace Shared.Users.Doctors.Availabilities;

public abstract class AvailabilityDto
{
    public class Index
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class Mutate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EmployeeDto.Detail Employee { get; set; } = default!;

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
                RuleFor(x => x.Employee).NotNull();
            }
        }
    }
}

