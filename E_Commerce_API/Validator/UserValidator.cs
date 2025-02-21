using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(50).WithMessage("Password cannot exceed 50 characters.");

         

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.")
                .When(u => !string.IsNullOrEmpty(u.PhoneNumber));

            RuleFor(u => u.Address)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.")
                .When(u => !string.IsNullOrEmpty(u.Address));
        }
    }
}
