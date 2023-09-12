using System.Security.Claims;
using Carter;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Contracts;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Booking_Coverage.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Booking_Coverage;

public static class AddNewBookingCoverage
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
}

public class AddNewBookingCoverageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/BookingCoverage", async (CreateBookingCoverageRequest request, ISender sender, HttpContext context) =>
        {
            var response = new QueryOrCommandResult<object>();
            try
            {
                var command = request.Adapt<AddNewBookingCoverage.AddNewBookingCoverageCommand>();
                var user = context.User;
                var userIdClaim = user.FindFirst("id");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    command.AddedBy = userId;
                }
                await sender.Send(command);
                response.Success = true;
                response.Status = StatusCodes.Status200OK;
                response.Messages.Add("Booking Coverage added successfully");
                return Results.Ok(response);
            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Messages.Add(e.Message);
                return Results.Conflict(response);
            }
        });
    }
}