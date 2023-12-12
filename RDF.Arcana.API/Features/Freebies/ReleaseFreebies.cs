using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class ReleaseFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public ReleaseFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ReleaseFreebies/{id:int}")]
    public async Task<IActionResult> ReleasedDirectFreebies([FromForm] ReleaseFreebiesCommand command,
        [FromRoute] int id)
    {
        try
        {
            command.ClientId = id;
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

    public class ReleaseFreebiesCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
    }

    public class Handler : IRequestHandler<ReleaseFreebiesCommand, Result>
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

        public async Task<Result> Handle(ReleaseFreebiesCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.FreebieRequests
                .Include(x => x.Clients)
                .FirstOrDefaultAsync(x =>
                    x.ClientId == request.ClientId && 
                    x.IsDelivered == false &&
                    x.Status == Status.ForReleasing, cancellationToken);

            if (validateClientRequest is null)
            {
                return ClientErrors.NotFound();
            }

            var uploadTasks = new List<Task>();
            
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
                            $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/{request.PhotoProof.FileName}"
                    };

                    var eSignaturePhotoParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.ESignature.FileName, esignatureStream),
                        PublicId =
                            $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/{request.ESignature.FileName}"
                    };


                    var photoproofUploadResult = await _cloudinary.UploadAsync(photoProofParams);
                    var eSignatureUploadResult = await _cloudinary.UploadAsync(eSignaturePhotoParams);

                    if (photoproofUploadResult.Error != null)
                    {
                        throw new Exception(photoproofUploadResult.Error.Message);
                    }

                    if (eSignatureUploadResult.Error != null)
                    {
                        throw new Exception(eSignatureUploadResult.Error.Message);
                    }
                    validateClientRequest.Status = "Released";
                    validateClientRequest.IsDelivered = true;
                    validateClientRequest.PhotoProofPath = photoproofUploadResult.SecureUrl.ToString();
                    validateClientRequest.ESignaturePath = eSignatureUploadResult.SecureUrl.ToString();
                }, cancellationToken));
            };

            await Task.WhenAll(uploadTasks);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}