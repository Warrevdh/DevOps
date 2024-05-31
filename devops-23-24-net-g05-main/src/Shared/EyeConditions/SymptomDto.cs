using FluentValidation;


namespace Shared.EyeConditions;

public abstract class SymptomDto
{
    public class Index
    {
        public long Id { get; set; }
        public string? Name { get; set; }
    }

    public class Mutate
    {
        public string? Name { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Gelieve een symptoomnaam in te voeren");
            }
        }
    }
}
