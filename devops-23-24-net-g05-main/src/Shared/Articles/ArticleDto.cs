using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Articles
{
    public abstract class ArticleDto
    {
        public class Index
        {
            public long Id { get; set; }
            public string? Title { get; set; }
            public string? ImageUrl { get; set; }
            public string? Description { get; set; }
        }

        public class Detail 
        { 
            public long Id { get; set; }
            public string? Title { get; set; }
            public string? ImageUrl { get; set;}
            public string? Description { get; set; }
            public string? Content { get; set; }
            public string? Author { get; set; }
            public DateTime? Created { get; set; }

            public class Validator : AbstractValidator<Detail>
            {
                public Validator()
                {
                    RuleFor(x => x.Title).NotEmpty().WithMessage("De titel van het artikel is verplicht.");
                    RuleFor(x => x.Description).NotEmpty().WithMessage("Je bent verplicht een korte beschrijving te geven over het artikel.");
                    RuleFor(x => x.Content).NotEmpty().WithMessage("Je bent verplicht uitgebreide informatie te geven over het artikel.");
                    /*                    RuleFor(x => x.ImageContentType).NotEmpty().WithMessage("Gelieve een afbeelding te uploaden");*/
                }
            }
        }

        public class Mutate
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Content { get; set; }
            public string? ImageContentType { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Title).NotEmpty().WithMessage("De titel van het artikel is verplicht.");
                    RuleFor(x => x.Description).NotEmpty().WithMessage("Je bent verplicht een korte beschrijving te geven over het artikel.");
                    RuleFor(x => x.Content).NotEmpty().WithMessage("Je bent verplicht uitgebreide informatie te geven over het artikel.");
/*                    RuleFor(x => x.ImageContentType).NotEmpty().WithMessage("Gelieve een afbeelding te uploaden");*/
                }
            }
        }
    }
}
