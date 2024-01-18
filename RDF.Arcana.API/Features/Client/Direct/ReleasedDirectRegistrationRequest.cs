/*using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.Direct;

[Route("api/Direct")]
[ApiController]
public class ReleasedDirectRegistrationRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ReleasedDirectRegistrationRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ReleaseFreebies/{id:int}")]
    public async Task<IActionResult> ReleasedDirectFreebies([FromForm] ReleasedDirectRegistrationRequestCommand command,
        [FromRoute] int id)
    {
        try
        {
            command.FreebieRequestId = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public record ReleasedDirectRegistrationRequestCommand : IRequest<Result<Unit>>
    {
        public int FreebieRequestId { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
    }

    public class Handler : IRequestHandler<ReleasedDirectRegistrationRequestCommand, Result>
    {
        private readonly Cloudinary _cloudinary;
        private readonly ArcanaDbContext _context;

        public Handler(IOptions<CloudinaryOptions> options, ArcanaDbContext context)
        {
            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _context = context;
        }

        public async Task<Result> Handle(ReleasedDirectRegistrationRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.Approvals
                .Include(x => x.FreebieRequest)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x =>
                    x.FreebieRequest.Any(x => x.Id == request.FreebieRequestId) &&
                    x.ApprovalType == "For Freebie Approval" &&
                    x.FreebieRequest.Any(x => x.IsDelivered == false) &&
                    x.IsActive == true &&
                    x.IsApproved == true, cancellationToken);

            if (validateClientRequest is null)
            {
                return ClientErrors.NotFound();
            }

            var uploadTasks = new List<Task>();

            foreach (var freebieRequest in validateClientRequest.FreebieRequest)
            {
                if (request.PhotoProof.Length > 0 || request.ESignature.Length > 0)
                {
                    uploadTasks.Add(Task.Run(async () =>
                    {
                        await using var stream = request.PhotoProof.OpenReadStream();
                        await using var esignatureStream = request.ESignature.OpenReadStream();

                        var photoProofParams = new ImageUploadParams
                        {
                            File = new FileDescription(request.PhotoProof.FileName, stream),
                            PublicId =
                                $"{WebUtility.UrlEncode(validateClientRequest.Client.BusinessName)}/{request.PhotoProof.FileName}"
                        };

                        var eSignaturePhotoParams = new ImageUploadParams
                        {
                            File = new FileDescription(request.ESignature.FileName, esignatureStream),
                            PublicId =
                                $"{WebUtility.UrlEncode(validateClientRequest.Client.BusinessName)}/{request.ESignature.FileName}"
                        };


                        var photoProofUploadResult = await _cloudinary.UploadAsync(photoProofParams);
                        var eSignatureUploadResult = await _cloudinary.UploadAsync(eSignaturePhotoParams);

                        if (photoProofUploadResult.Error != null)
                        {
                            throw new Exception(photoProofUploadResult.Error.Message);
                        }

                        if (eSignatureUploadResult.Error != null)
                        {
                            throw new Exception(eSignatureUploadResult.Error.Message);
                        }
                        freebieRequest.Status = "Released";
                        freebieRequest.IsDelivered = true;
                        freebieRequest.PhotoProofPath = photoProofUploadResult.SecureUrl.ToString();
                        freebieRequest.ESignaturePath = eSignatureUploadResult.SecureUrl.ToString();
                    }, cancellationToken));
                };

                await Task.WhenAll(uploadTasks);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Result.Success();
        }
    }
}*/