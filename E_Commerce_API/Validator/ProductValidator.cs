using E_Commerce_API.Model;
using FluentValidation;

namespace Api.Validation
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.BrandName)
                .NotEmpty().WithMessage("Brand name is required.")
                .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.Description));

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Category ID must be a positive integer.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");

            RuleFor(p => p.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be a non-negative value.")
                .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100%.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be a non-negative value.");


            RuleFor(p => p.WarrantyPeriod)
                .MaximumLength(50).WithMessage("Warranty period cannot exceed 50 characters.")
                .When(p => !string.IsNullOrEmpty(p.WarrantyPeriod));
        }
    }
}
