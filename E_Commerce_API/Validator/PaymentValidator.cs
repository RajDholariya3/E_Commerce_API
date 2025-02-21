using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class PaymentsValidator : AbstractValidator<PaymentsModel>
    {
        public PaymentsValidator()
        {
            RuleFor(p => p.OrderId)
                .GreaterThan(0).WithMessage("Order ID must be a positive integer.");

            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive integer.");

            RuleFor(p => p.PaymentDate)
                .NotEmpty().WithMessage("Payment Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Payment Date cannot be in the future.");

            RuleFor(p => p.AmountPaid)
                .GreaterThanOrEqualTo(0).WithMessage("Amount Paid must be a non-negative value.");

            RuleFor(p => p.PaymentMethod)
                .NotEmpty().WithMessage("Payment Method is required.")
                .MaximumLength(50).WithMessage("Payment Method cannot exceed 50 characters.");

            RuleFor(p => p.PaymentStatus)
                .NotEmpty().WithMessage("Payment Status is required.")
                .MaximumLength(50).WithMessage("Payment Status cannot exceed 50 characters.");
        }
    }
}
