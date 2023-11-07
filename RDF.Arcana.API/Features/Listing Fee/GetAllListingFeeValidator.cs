using FluentValidation;

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
        return status is "Approved" or "Under review" or "Rejected";
    }
}