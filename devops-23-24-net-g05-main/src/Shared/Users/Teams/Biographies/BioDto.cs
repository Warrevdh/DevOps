using FluentValidation;

namespace Shared.Users.Teams.Biographies;

public abstract class BioDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Info { get; set; }
	}

	public class Mutate
	{
		public string? Info { get; set; }

		public class Validator : AbstractValidator<Mutate>
		{
			public Validator()
			{
				RuleFor(x => x.Info).NotEmpty();
			}
		}
	}
}
