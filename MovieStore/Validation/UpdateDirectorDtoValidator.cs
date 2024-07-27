using FluentValidation;
using MovieStore.Entity.Dto;

namespace MovieStore.Validation
{
    public class UpdateDirectorDtoValidator : AbstractValidator<UpdateDirectorDto>
    {
        public UpdateDirectorDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage($"{nameof(UpdateDirectorDto.Email)} alani boş geçilemez");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage($"{nameof(UpdateDirectorDto.Password)} alani boş geçilemez");
            RuleFor(x => x.Movies).NotNull().NotEmpty().WithMessage($"{nameof(UpdateDirectorDto.Movies)} alani boş geçilemez");


        }
    }
}
