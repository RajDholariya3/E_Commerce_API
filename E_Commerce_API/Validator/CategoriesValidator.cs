using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class CategoryValidator : AbstractValidator<CategoriesModel>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category Name is required.")
                .Length(1, 100).WithMessage("Category Name must be between 1 and 100 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(c => !string.IsNullOrEmpty(c.Description));
        }
    }
}
