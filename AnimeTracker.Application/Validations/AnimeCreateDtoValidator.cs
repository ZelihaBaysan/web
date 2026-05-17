using AnimeTracker.Application.DTOs.Anime;
using FluentValidation;

namespace AnimeTracker.Application.Validations
{
    public class AnimeCreateDtoValidator : AbstractValidator<AnimeCreateDto>
    {
        public AnimeCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Anime adı boş olamaz.")
                .MaximumLength(200).WithMessage("Anime adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.MalId)
                .GreaterThan(0).WithMessage("Geçerli bir MyAnimeList ID'si girilmelidir.");
                
            RuleFor(x => x.Episodes)
                .GreaterThanOrEqualTo(1).When(x => x.Episodes.HasValue).WithMessage("Bölüm sayısı en az 1 olmalıdır.");
        }
    }
}
