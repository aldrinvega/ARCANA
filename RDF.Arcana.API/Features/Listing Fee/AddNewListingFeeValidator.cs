using FluentValidation;

namespace RDF.Arcana.API.Features.Listing_Fee;

public class AddNewListingFeeValidator : AbstractValidator<AddNewListingFee.AddNewListingFeeCommand>
{
    public AddNewListingFeeValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId must not be empty")
            .Must(x => true).WithMessage("ClientId must be a number");

        RuleFor(x => x.Total)
            .NotEmpty().WithMessage("Total must not be empty")
            .Must(x => true).WithMessage("Total must be a decimal number");

        RuleForEach(x => x.ListingItems)
            .ChildRules(items =>
            {
                items.RuleFor(i => i.ItemId)
                    .NotEmpty().WithMessage("ItemId must not be empty")
                    .Must(x => true).WithMessage("ItemId must be a number");

                items.RuleFor(i => i.Sku)
                    .NotEmpty().WithMessage("Sku must not be empty")
                    .Must(x => true).WithMessage("Sku must be a number");

                items.RuleFor(i => i.UnitCost)
                    .NotEmpty().WithMessage("UnitCost must not be empty")
                    .Must(x => true).WithMessage("UnitCost must be a decimal number");
            });
    }
}