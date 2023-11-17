using FluentValidation;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Listing_Fee;

public class GetAllListingFeeValidator : AbstractValidator<GetAllListingFee.GetAllListingFeeQuery>
{
    public GetAllListingFeeValidator()
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