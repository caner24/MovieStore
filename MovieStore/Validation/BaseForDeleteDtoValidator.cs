using FluentValidation;
using MovieStore.Entity.Dto;

namespace MovieStore.Validation
{
    public class BaseForDeleteDtoValidator:AbstractValidator<BaseForDeleteDto>
    {
        public BaseForDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage($"{nameof(BaseForDeleteDto.Id)} alani boş geçilemez");
        }

    }
}
