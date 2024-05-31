using FluentValidation;

namespace Shared.Users.Teams.Groups;

public abstract class GroupDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public int? Sequence { get; set; }
	}

	public class Mutate
	{
		public string Name { get; set; } = default!;
		public int Sequence { get; set; } = 1;

		public class Validator : AbstractValidator<Mutate>
		{
			public Validator()
			{
				RuleFor(x => x.Name).NotEmpty().MaximumLength(75);
				RuleFor(x => x.Sequence).GreaterThanOrEqualTo(1);
			}
		}
	}
}
