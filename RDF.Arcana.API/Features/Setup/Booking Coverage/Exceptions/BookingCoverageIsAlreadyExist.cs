namespace RDF.Arcana.API.Features.Setup.Booking_Coverage.Exceptions;

public class BookingCoverageIsAlreadyExist : Exception
{
    public BookingCoverageIsAlreadyExist(string bookingCoverage) : base($"{bookingCoverage} is already exist"){}
}