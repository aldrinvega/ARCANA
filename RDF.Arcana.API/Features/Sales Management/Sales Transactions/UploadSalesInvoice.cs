using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_transactions;
[Route("api/sales-transaction"), ApiController]

public class UploadSalesInvoice : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadSalesInvoice(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("si/{id:int}")]
    public async Task<ActionResult> Upload([FromForm] UploadSalesInvoiceCommand command, [FromRoute]int id)
    {
        try
        {
            command.SalesTransactionId = id;

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UploadSalesInvoiceCommand : IRequest<Result>
    {
        public int SalesTransactionId { get; set; }
        public IFormFile SalesInvoice { get; set; }
    }
    
    public class Handler : IRequestHandler<UploadSalesInvoiceCommand, Result>
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

        public async Task<Result> Handle(UploadSalesInvoiceCommand request, CancellationToken cancellationToken)
        {
            var existingSalesTransaction =
                await _context.Transactions
                    .FirstOrDefaultAsync(st => 
                        st.Id == request.SalesTransactionId, 
                        cancellationToken);

            if (existingSalesTransaction is null)
            {
                return SalesTransactionErrors.NotFound();
            }
            
            if (request.SalesInvoice.Length > 0)
            {
                await using var stream = request.SalesInvoice.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(request.SalesInvoice.FileName, stream),
                    PublicId = request.SalesInvoice.FileName
                };

               var  attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

               existingSalesTransaction.InvoiceAttach = attachmentsUploadResult.SecureUrl.ToString();
               existingSalesTransaction.InvoiceAttachDateReceived = DateTime.Now;
                
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Result.Success();
        }
    }
}