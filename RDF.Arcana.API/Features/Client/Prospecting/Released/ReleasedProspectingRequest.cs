using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
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
        var response = new QueryOrCommandResult<UploadPhotoResult>();
        try
        {
            command.ClientId = id;

            var result = await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Freebie Request has been released");
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class ReleasedProspectingRequestCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
    }

    public class Handler : IRequestHandler<ReleasedProspectingRequestCommand, Unit>
    {
        private const string released = "Released";
        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;

        public Handler(IOptions<CloudinarySettings> config, DataContext context)
        {
            var account = new Account(
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _context = context;
        }

        public async Task<Unit> Handle(ReleasedProspectingRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.Approvals
                .Include(x => x.FreebieRequest)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x =>
                    x.ClientId == request.ClientId &&
                    x.ApprovalType == "For Freebie Approval" &&
                    x.IsActive == true &&
                    x.IsApproved == true, cancellationToken);

            if (validateClientRequest is null)
            {
                throw new NoProspectClientFound();
            }

            foreach (var freebieRequest in validateClientRequest.FreebieRequest)
            {
                if (request.PhotoProof.Length > 0 || request.ESignature.Length > 0)
                {
                    await using var stream = request.PhotoProof.OpenReadStream();
                    await using var esignatureStream = request.ESignature.OpenReadStream();

                    var photoProofParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.PhotoProof.FileName, stream),
                        PublicId = $"{validateClientRequest.Client.BusinessName}/{request.PhotoProof.FileName}"
                    };

                    var eSignaturePhotoParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.ESignature.FileName, esignatureStream),
                        PublicId = $"{validateClientRequest.Client.BusinessName}/{request.ESignature.FileName}"
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

                    freebieRequest.Status = "Released";
                    freebieRequest.IsDelivered = true;
                    freebieRequest.PhotoProofPath = photoproofUploadResult.SecureUrl.ToString();
                    freebieRequest.ESignaturePath = eSignatureUploadResult.SecureUrl.ToString();

                    await _context.SaveChangesAsync(cancellationToken);
                }

                // If you want to update only a specific FreebieRequest, you should fetch it before updating
                // Example: 
                // var specificFreebieRequest = validateClientRequest.FreebieRequests.FirstOrDefault(x => x.SomeId == someConditions);
                // if (specificFreebieRequest != null){ /* update specificFreebieRequest */ }
            }

            return Unit.Value;
        }
    }
}