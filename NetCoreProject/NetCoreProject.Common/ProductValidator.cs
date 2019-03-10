
using FluentValidation;

namespace NetCoreProject.Common
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please fill the Name");
            RuleFor(x => x.QuantityPerUnit).MaximumLength(20).WithMessage("Only 20 symbols are allowed for free");
            RuleFor(x => x.UnitPrice).ExclusiveBetween(0m, decimal.MaxValue).WithMessage("Unit price can't be negative");
        }
    }
}
