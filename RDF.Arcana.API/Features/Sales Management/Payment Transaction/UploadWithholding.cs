using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Sales_Management.Sales_transactions;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;
[Route("api/payment-transaction"), ApiController]

public class UploadWithholding : ControllerBase
{
    private readonly IMediator _mediator;
    public UploadWithholding(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("withholding/{id:int}")]
    public async Task<ActionResult> Upload([FromForm] UploadWithholdingCommand command, [FromRoute] int id)
    {
        try
        {
            command.PaymentTransactionId = id;

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UploadWithholdingCommand : IRequest<Result>
    {
        public int PaymentTransactionId { get; set; }
        public IFormFile Withholding { get; set; }
    }

    public class Handler : IRequestHandler<UploadWithholdingCommand, Result>
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
        public async Task<Result> Handle(UploadWithholdingCommand request, CancellationToken cancellationToken)
        {
            var existingSalesTransaction =
                await _context.PaymentTransactions
                    .FirstOrDefaultAsync(st =>
                        st.Id == request.PaymentTransactionId,
                        cancellationToken);

            if (existingSalesTransaction is null)
            {
                return SalesTransactionErrors.NotFound();
            }

            if (request.Withholding.Length > 0)
            {
                await using var stream = request.Withholding.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(request.Withholding.FileName, stream),
                    PublicId = request.Withholding.FileName
                };

                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                existingSalesTransaction.WithholdingAttachment = attachmentsUploadResult.SecureUrl.ToString();
                existingSalesTransaction.DateReceived = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return Result.Success();
        }
    }
}
