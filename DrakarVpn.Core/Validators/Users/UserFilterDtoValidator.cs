using DrakarVpn.Domain.ModelDto.Users;
using FluentValidation;

namespace DrakarVpn.Core.Validators.Users;

public class UserFilterDtoValidator : AbstractValidator<UserFilterDto>
{
    public UserFilterDtoValidator()
    {
        RuleFor(x => x.Email)
            .MaximumLength(200);

        RuleFor(x => x.Country)
            .MaximumLength(100);

    }
}
