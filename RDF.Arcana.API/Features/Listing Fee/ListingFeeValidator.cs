using FluentValidation;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Listing_Fee;

public class GetAllListingValidator : AbstractValidator<GetAllListingFee.GetAllListingFeeQuery>
{
    public GetAllListingValidator()
    {
        RuleFor(x => x.ListingFeeStatus)
            .Must(BeAValidStatus)
            .WithMessage(
                "Invalid value for ListingFeeStatus. Only 'Approved', 'Under review', and 'Rejected' are permitted.");
    }

    private static bool BeAValidStatus(string status)
    {
        return status is Status.Approved or Status.UnderReview or Status.Rejected;
    }
}

public class UpdateListingFeeValidator : AbstractValidator<UpdateListingFeeInformation.UpdateListingFeeInformationCommand>
{
    public UpdateListingFeeValidator()
    {
        RuleFor(x => x.ListingItems)
            .NotNull()
            .WithMessage("Listing fee items must not be empty");
    }
}