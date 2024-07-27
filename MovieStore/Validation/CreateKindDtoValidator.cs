using FluentValidation;
using MovieStore.Entity.Dto;

namespace MovieStore.Validation
{
    public class CreateKindDtoValidator : AbstractValidator<CreateKindDto>
    {
        public CreateKindDtoValidator()
        {
            RuleFor(x => x.KindName).NotNull().NotEmpty().WithMessage($"{nameof(BaseUserForCreationDto.Email)} alani boş geçilemez");
        }
    }
}
