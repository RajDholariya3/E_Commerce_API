using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
                RuleFor(o => o.UserId)
                    .GreaterThan(0).WithMessage("User ID must be a positive integer.");

            RuleFor(o => o.OrderNumber)
    .NotEmpty().WithMessage("Order Number is required.")
    .Matches(@"^ORN\d{3}$").WithMessage("Order Number must be in the format 'ORN###' (e.g., ORN001).");

            RuleFor(o => o.OrderDate)
                .NotEmpty().WithMessage("Order Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order Date cannot be in the future.");

            RuleFor(o => o.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total Amount must be a non-negative value.");

            RuleFor(o => o.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(50).WithMessage("Status cannot exceed 50 characters.");
        }
    }
}
