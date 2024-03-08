using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Released;

[Route("api/Prospecting")]
[ApiController]
public class ReleasedProspectingRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ReleasedProspectingRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ReleasedProspectingRequest/{id:int}")]
    public async Task<IActionResult> ReleasedProspecting([FromForm] ReleasedProspectingRequestCommand command,
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
            return Conflict(e.Message);
        }
    }

    public class ReleasedProspectingRequestCommand : IRequest<Result>
    {
        public int FreebieRequestId { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
    }

    public class Handler : IRequestHandler<ReleasedProspectingRequestCommand, Result>
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

        public async Task<Result> Handle(ReleasedProspectingRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.FreebieRequests
                .Include(x => x.Clients)
                .FirstOrDefaultAsync(x =>
                    x.Id == request.FreebieRequestId && 
                    x.IsDelivered == false &&
                    x.Status == Status.ForReleasing, cancellationToken);

            if (validateClientRequest is null)
            {
                return ClientErrors.NotFound();
            }
            
            
            if (request.PhotoProof.Length > 0 || request.ESignature.Length > 0)
            {
                await using var stream = request.PhotoProof.OpenReadStream();
                await using var esignatureStream = request.ESignature.OpenReadStream();

                var photoProofParams = new ImageUploadParams
                {
                    File = new FileDescription(request.PhotoProof.FileName, stream),
                    PublicId =
                        $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/{request.PhotoProof.FileName}"
                };

                var eSignaturePhotoParams = new ImageUploadParams
                {
                    File = new FileDescription(request.ESignature.FileName, esignatureStream),
                    PublicId =
                        $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/{request.ESignature.FileName}"
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


                validateClientRequest.Status = Status.Released;
                validateClientRequest.IsDelivered = true;
                validateClientRequest.PhotoProofPath = photoProofUploadResult.SecureUrl.ToString();
                validateClientRequest.ESignaturePath = eSignatureUploadResult.SecureUrl.ToString();

                var notification = new Domain.Notification
                {
                    UserId = validateClientRequest.RequestedBy,
                    Status = Status.Released
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
            }

            validateClientRequest.Clients.RegistrationStatus = Status.PendingRegistration;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}