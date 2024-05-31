using FluentValidation;

namespace Shared.Appointments.Timeslots;

public abstract class TimeslotDto
{
	public class Index
	{
		public long Id { get; set; }
		public DateTime Datetime { get; set; }
		public TimeSpan Duration { get; set; }
	}

    public class Detail
    {
		public long Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
	}

    public class Mutate
    {
        public DateTime Datetime { get; set; }
        public TimeSpan Duration { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Datetime).NotNull();
                RuleFor(x => x.Duration).NotNull();
            }
        }
    }
}
