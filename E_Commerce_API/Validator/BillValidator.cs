using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class BillValidator : AbstractValidator<BillsModel>
    {
        public BillValidator()
        {
            RuleFor(b => b.BillNumber)
                .NotEmpty().WithMessage("Bill Number is required.")
                .Length(1, 50).WithMessage("Bill Number must be between 1 and 50 characters.");

            RuleFor(b => b.BillDate)
                .NotEmpty().WithMessage("Bill Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Bill Date cannot be in the future.");

            RuleFor(b => b.OrderId)
                .GreaterThan(0).WithMessage("Order ID must be a positive integer.");

            RuleFor(b => b.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive integer.");

            RuleFor(b => b.ShippingAddress)
                .NotEmpty().WithMessage("Shipping Address is required.")
                .MaximumLength(250).WithMessage("Shipping Address cannot exceed 250 characters.");
        }
    }
}
