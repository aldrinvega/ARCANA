using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Booking_Coverage.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Booking_Coverage;

public class AddNewBookingCoverage
{
    public class AddNewBookingCoverageCommand : IRequest<Unit>
    {
        public string BookingCoverage { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewBookingCoverageCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewBookingCoverageCommand request, CancellationToken cancellationToken)
        {
            var existingBookingCoverage =
                await _context.BookingCoverages.FirstOrDefaultAsync(x => x.BookingCoverage == request.BookingCoverage,
                    cancellationToken);

            if (existingBookingCoverage is not null)
            {
                throw new BookingCoverageIsAlreadyExist(request.BookingCoverage);
            }

           
            return Unit.Value;
            
        }
    }
}