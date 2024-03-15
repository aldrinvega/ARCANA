using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;
using RDF.Arcana.API.Features.Setup.Mode_Of_Payment;
using RDF.Arcana.API.Features.Setup.Terms;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register;

[Route("api/Registration")]
[ApiController]
public class AddTermsAndCondition : ControllerBase
{
    private readonly IMediator _mediator;

    public AddTermsAndCondition(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("AddTermsAndCondition/{id}")]
    public async Task<IActionResult> AddTermsCondition([FromBody] AddTermsAndConditionsCommand command,
        [FromRoute] int id)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            command.ClientId = id;
           var result =  await _mediator.Send(command);
           if (result.IsFailure)
           {
               return BadRequest(result);
           }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    public record AddTermsAndConditionsCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public string TypeOfCustomer { get; set; }
        public bool DirectDelivery { get; set; }
        public int BookingCoverageId { get; set; }
        public ICollection<ClientModeOfPayment> ModeOfPayments { get; set; }
        public int Terms { get; set; }
        public int? CreditLimit { get; set; }
        public int? TermDaysId { get; set; }
        public Dicount FixedDiscount { get; set; }
        public bool VariableDiscount { get; set; }
        public int AddedBy { get; set; }
        public string Freezer { get; set; }

        public class Dicount
        {
            public decimal? DiscountPercentage { get; set; }
        }
        public class ClientModeOfPayment
        {
            public int ModeOfPaymentId { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddTermsAndConditionsCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddTermsAndConditionsCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(
                x => x.Id == request.ClientId, cancellationToken
            );

            if (existingClient == null) throw new ClientIsNotFound(request.ClientId);

            if(request.Freezer != null)
            {
                var freezer = new Freezer
                {
                    AssetTag = request.Freezer
                };

                await _context.Freezers.AddAsync(freezer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                existingClient.FreezerId = freezer.Id;
            }

            existingClient.CustomerType = request.TypeOfCustomer;
            existingClient.DirectDelivery = request.DirectDelivery;
            existingClient.BookingCoverageId = request.BookingCoverageId;


            var limit = request.CreditLimit;

            if (request.CreditLimit.HasValue)
            {
                limit = request.CreditLimit.Value;
            }

            var validateTerms = await _context.Terms.FirstOrDefaultAsync(x => x.Id == request.Terms, cancellationToken);

            if (validateTerms is null)
            {
                return TermErrors.NotFound();
            }

            foreach (var modeOfPayment in request.ModeOfPayments)
            {
                var existingModePayment = await _context.ModeOfPayments.FirstOrDefaultAsync(x => 
                        x.Id == modeOfPayment.ModeOfPaymentId,
                        cancellationToken);

                if (existingModePayment is null)
                {
                    return ModeOfPaymentErrors.NotFound();
                }

                var newPaymentMethod = new ClientModeOfPayment
                {
                    ClientId = existingClient.Id,
                    ModeOfPaymentId = modeOfPayment.ModeOfPaymentId,
                };
                _context.ClientModeOfPayments.Add(newPaymentMethod);
            }

            var termsOptions = new TermOptions
            {
                TermsId = request.Terms,
                CreditLimit = limit,
                TermDaysId = request.TermDaysId,
                AddedBy = request.AddedBy
            };

            await _context.TermOptions.AddAsync(termsOptions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            existingClient.Terms = termsOptions.Id;
            //For validation
            // Check if the user can have no discount at all
            if (request.FixedDiscount?.DiscountPercentage != null)
            {
                var fixedDiscount = new FixedDiscounts
                {
                    /*ClientId = existingClient.Id,*/
                    DiscountPercentage = request.FixedDiscount.DiscountPercentage / 100
                };

                await _context.FixedDiscounts.AddAsync(fixedDiscount, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var discountId = fixedDiscount.Id;
                existingClient.FixedDiscountId = discountId;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                existingClient.VariableDiscount = request.VariableDiscount;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }

            return Result.Success();
        }
    }
}