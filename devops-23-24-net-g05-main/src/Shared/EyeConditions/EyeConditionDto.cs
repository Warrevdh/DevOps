using FluentValidation;
using Shared.EyeConditions;

namespace Shared.EyeConditions;
public abstract class EyeConditionDto
{
    public class Index
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public string? ImageUrl { get; set; }
		public List<SymptomDto.Index> Symptoms { get; set; }
        public string? BrochureUrl { get; set; }
    }

    public class Detail
    {
        public long Id { get; set; }
        public string? Name { get; set; }
		public string? Body { get; set; }
		public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? BrochureUrl { get; set; }
    }

    public class Mutate
	{
        public long Id { get; set; }
        public string? Name { get; set; }
		public string? Description { get; set; }
		public string Body { get; set; } = "";
		public string? ImageContentType { get; set; }
		public string? BrochureUrl { get; set; }
		public List<SymptomDto.Index>? Symptoms { get; set; }

		public class Validator : AbstractValidator<Mutate>
		{
			public Validator()
			{
				RuleFor(x => x.Name).NotEmpty();
				RuleFor(x => x.Description).NotEmpty();
				RuleFor(x => x.Body).NotEmpty();
				/*                RuleFor(x => x.ImageContentType).NotEmpty().WithName("Image");*/
				/*RuleFor(x => x.Symptoms).NotEmpty();*/
			}
		}
	}
}
