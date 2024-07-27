using FluentValidation;
using MovieStore.Entity.Dto;

namespace MovieStore.Validation
{
    public class UpdateKindDtoValidator : AbstractValidator<UpdateKindDto>
    {
        public UpdateKindDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage($"{nameof(UpdateKindDto.Id)} alani boş geçilemez");
            RuleFor(x => x.KindName).NotNull().NotEmpty().WithMessage($"{nameof(UpdateKindDto.KindName)} alani boş geçilemez");


        }


    }
}
