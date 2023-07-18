using Basket.Application.Contracts.Handlers.Dtos;
using FluentValidation;

namespace Basket.Api.Validators;

public class ShoppingCartItemDtoValidator : AbstractValidator<ShoppingCartItemDto>
{
    public ShoppingCartItemDtoValidator()
    {
        RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("Quantity cannot be zero").WithSeverity(Severity.Error);
        RuleFor(item => item.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative").WithSeverity(Severity.Error);
        RuleFor(item => item.ProductId).NotEmpty().WithMessage("ProductId is required").WithSeverity(Severity.Error);
        RuleFor(item => item.ProductName).NotEmpty().WithMessage("ProductName is required").WithSeverity(Severity.Error);
    }
}
