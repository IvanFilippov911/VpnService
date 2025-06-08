using DrakarVpn.Domain.ModelDto.Subscriptions;
using FluentValidation;

namespace DrakarVpn.Core.Validators.Subscriptions;

public class SubscriptionPurchaseDtoValidator : AbstractValidator<SubscriptionPurchaseDto>
{
    public SubscriptionPurchaseDtoValidator()
    {
        RuleFor(x => x.TariffId)
            .NotEmpty().WithMessage("TariffId is required.");
    }
}
