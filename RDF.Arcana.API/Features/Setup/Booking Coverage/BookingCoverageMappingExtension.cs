using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Booking_Coverage;

public static class BookingCoverageMappingExtension
{
    public static GetAllBookingCoverages.GetAllBookingCoveragesResult
        ToGetAllBookingCoveragesResult(this BookingCoverages company)
    {
        return new GetAllBookingCoverages.GetAllBookingCoveragesResult
        {
            Id = company.Id,
            BookingCoverage = company.BookingCoverage
        };
    }
}