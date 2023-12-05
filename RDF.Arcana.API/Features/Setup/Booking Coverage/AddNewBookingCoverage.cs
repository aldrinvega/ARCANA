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

    public class AddNewBookingCoverageCommand : IRequest<Result>
    {
        public string BookingCoverage { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewBookingCoverageCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewBookingCoverageCommand request, CancellationToken cancellationToken)
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
            return Result.Success();

        }
    }

    [HttpPost("AddNewBookingCoverage")]
    public async Task<IActionResult> Add([FromBody] AddNewBookingCoverageCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
           
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}