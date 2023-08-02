using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Freebies;

public class RequestFreebies : ControllerBase
{
    public class RequestFreebiesCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
    
    public class Handler : IRequestHandler<RequestFreebiesCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RequestFreebiesCommand request, CancellationToken cancellationToken)
        {
            var validateClient =
                await _context.RejectedClients.FirstOrDefaultAsync(x =>
                    x.ClientId == request.ClientId && x.Status == 3, cancellationToken: cancellationToken);
            if (validateClient is not null)
            {
                throw new Exception("User is rejected, cannot proceed request!");
            }

            var freebies = new Domain.Freebies
            {
                ClientId = request.ClientId,
                ItemId = request.ItemId,
                Quantity = request.Quantity,
                StatusId = 1,
                IsActive = true
            };

            await _context.Freebies.AddAsync(freebies, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}