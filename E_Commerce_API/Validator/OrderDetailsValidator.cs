using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class OrderDetailsValidator : AbstractValidator<OrderDetailsModel>
    {
        public OrderDetailsValidator()
        {
            RuleFor(o => o.OrderID)
                .GreaterThan(0).WithMessage("Order ID must be a positive integer.");

            RuleFor(o => o.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be a positive integer.");

            RuleFor(o => o.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(o => o.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");
        }
    }
}
