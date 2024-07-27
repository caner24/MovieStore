using FluentValidation;
using MovieStore.Entity.Dto;

namespace MovieStore.Validation
{
    public class BaseUserForCreationDtoValidator:AbstractValidator<BaseUserForCreationDto>
    {

        public BaseUserForCreationDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage($"{nameof(BaseUserForCreationDto.Email)} alani boş geçilemez");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage($"{nameof(BaseUserForCreationDto.Password)} alani boş geçilemez");

        }
    }
}
