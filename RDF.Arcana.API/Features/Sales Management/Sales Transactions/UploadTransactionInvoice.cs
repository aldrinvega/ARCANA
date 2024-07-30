using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Sales_Management.Sales_transactions;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;
[Route("api/transactions-invoice"), ApiController]
public class UploadTransactionInvoice : ControllerBase
{
    private readonly IMediator _mediator;
    public UploadTransactionInvoice(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Upload([FromForm] UploadChargeInvoiceCommand command, [FromRoute] int id)
    {
        try
        {
            command.TransactionId = id;

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UploadChargeInvoiceCommand : IRequest<Result>
    {
        public int TransactionId { get; set; }
        public IFormFile InvoiceAttach { get; set; }
    }

    public class Handler : IRequestHandler<UploadChargeInvoiceCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        private readonly Cloudinary _cloudinary;

        public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> options)
        {
            _context = context;
            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<Result> Handle(UploadChargeInvoiceCommand request, CancellationToken cancellationToken)
        {
            var existingSalesTransaction =
                await _context.Transactions
                    .FirstOrDefaultAsync(t =>
                        t.Id == request.TransactionId,
                        cancellationToken);

            if (existingSalesTransaction is null)
            {
                return SalesTransactionErrors.NotFound();
            }

            if (request.InvoiceAttach.Length > 0)
            {
                await using var stream = request.InvoiceAttach.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(request.InvoiceAttach.FileName, stream),
                    PublicId = request.InvoiceAttach.FileName
                };

                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                existingSalesTransaction.InvoiceAttach = attachmentsUploadResult.SecureUrl.ToString();
                existingSalesTransaction.InvoiceAttachDateReceived = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return Result.Success();
        }
    }
}
