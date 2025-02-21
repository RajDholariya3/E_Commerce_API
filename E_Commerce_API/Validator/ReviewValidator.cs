using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class ReviewValidator : AbstractValidator<ReviewModel>
    {
        public ReviewValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive integer.");

            RuleFor(r => r.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be a positive integer.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(r => r.ReviewDate)
                .NotEmpty().WithMessage("Review Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Review Date cannot be in the future.");

            RuleFor(r => r.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
        }
    }
}
