using DrakarVpn.Domain.ModelDto.Tariffs;
using FluentValidation;

namespace DrakarVpn.Core.Validators.Tariffs;

public class TariffCreateUpdateDtoValidator : AbstractValidator<TariffCreateUpdateDto>
{
    public TariffCreateUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.DurationInDays)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 days.");

        RuleFor(x => x.Limitations)
            .MaximumLength(1000);
    }
}
