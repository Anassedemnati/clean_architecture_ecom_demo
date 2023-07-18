using Basket.Application.Contracts.Handlers.Dtos;
using FluentValidation;

namespace Basket.Api.Validators;

public class ShoppingCartDtoValidator:AbstractValidator<ShoppingCartDto>
{
    public ShoppingCartDtoValidator()
    {
        RuleFor(basket => basket.UserName).NotEmpty().WithMessage("Username is required").WithSeverity(Severity.Error);
        RuleFor(basket => basket.Items).Must(items => items.Count > 0).WithMessage("Basket should have at least one item").WithSeverity(Severity.Error);
        RuleFor(basket => basket.TotalPrice).GreaterThanOrEqualTo(0).WithMessage("Total price cannot be negative").WithSeverity(Severity.Error);
        RuleForEach(basket => basket.Items).SetValidator(new ShoppingCartItemDtoValidator());
    }
}
