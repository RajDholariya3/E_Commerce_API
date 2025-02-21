using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive integer.");

            RuleFor(c => c.LoyaltyPoint)
                .GreaterThanOrEqualTo(0).WithMessage("Loyalty Points must be non-negative.");

            RuleFor(c => c.MembershipType)
                .NotEmpty().WithMessage("Membership Type is required.")
                .MaximumLength(50).WithMessage("Membership Type cannot exceed 50 characters.");

            RuleFor(c => c.UserName)
                .MaximumLength(100).WithMessage("User Name cannot exceed 100 characters.")
                .When(c => !string.IsNullOrEmpty(c.UserName));
        }
    }
}
