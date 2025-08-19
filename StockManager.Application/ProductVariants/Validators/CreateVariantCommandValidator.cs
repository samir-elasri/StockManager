using FluentValidation;
using StockManager.Application.ProductVariants.Commands;

namespace StockManager.Application.ProductVariants.Validators
{
    public class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand>
    {
        public CreateVariantCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
