using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class UpdateFreebiesInformation : ControllerBase

{
    private readonly IMediator _mediator;

    public UpdateFreebiesInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateFreebieInformation/{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateFreebiesInformationCommand command, [FromRoute] int id,
        [FromQuery] int freebieId)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;
            command.FreebieRequestId = freebieId;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Freebies are updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class UpdateFreebiesInformationCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int FreebieRequestId { get; set; }
        public List<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateFreebiesInformationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFreebiesInformationCommand request, CancellationToken cancellationToken)
        {
            var freebies = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId
                         && x.ApprovalType == "For Freebie Approval"
                         && x.FreebieRequest.Id == request.FreebieRequestId
                         && x.FreebieRequest.IsDelivered == false,
                    cancellationToken);

            if (freebies is null)
            {
                throw new Exception("No freebies found");
            }

            var requestItemIds = request.Freebies.Select(f => f.ItemId).ToList();
            var existingItemIds = freebies.FreebieRequest.FreebieItems.Select(i => i.ItemId).ToList();
            var itemsToRemove = existingItemIds.Except(requestItemIds);
            foreach (var itemId in itemsToRemove)
            {
                var itemToRemove = freebies.FreebieRequest.FreebieItems.First(i => i.ItemId == itemId);
                freebies.FreebieRequest.FreebieItems.Remove(itemToRemove);
            }

            foreach (var requestFreebie in request.Freebies)
            {
                var freebieItem =
                    freebies.FreebieRequest.FreebieItems.FirstOrDefault(x => x.ItemId == requestFreebie.ItemId);
                if (freebieItem != null)
                {
                    // If the freebie item exists in the database, update its quantity
                    freebieItem.Quantity = requestFreebie.Quantity;
                }
                else
                {
                    // If the freebie doesn't exist, add it.
                    freebies.FreebieRequest.FreebieItems.Add(
                        new FreebieItems
                        {
                            RequestId = request.FreebieRequestId,
                            ItemId = requestFreebie.ItemId,
                            Quantity = requestFreebie.Quantity
                        });
                }
            }

            freebies.IsApproved = true;
            freebies.FreebieRequest.Status = "Requested";

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}