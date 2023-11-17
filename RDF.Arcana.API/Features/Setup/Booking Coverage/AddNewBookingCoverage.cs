using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Booking_Coverage.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Booking_Coverage;

[Route("api/BookingCoverage")]
[ApiController]

public class AddNewBookingCoverage : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewBookingCoverage(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewBookingCoverageCommand : IRequest<Unit>
    {
        public string BookingCoverage { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewBookingCoverageCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
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

            var bookingCoverage = new BookingCoverages
            {
                BookingCoverage = request.BookingCoverage,
                AddedBy = request.AddedBy
            };
            await _context.BookingCoverages.AddAsync(bookingCoverage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }

    [HttpPost("AddNewBookingCoverage")]
    public async Task<IActionResult> Add([FromBody] AddNewBookingCoverageCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Booking Coverage added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status404NotFound;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}