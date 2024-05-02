using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Abstractions.Storage;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/attachments")]
    public class GetAttachmentByFileId : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAttachmentByFileId(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{fileId}/file")]
        public async Task<IActionResult> DownloadAsync([FromRoute] Guid fileId)
        {
            try
            {
                var query = new GetAttachmentByFileIdCommand
                {
                    FileId = fileId
                };

                var result = await _mediator.Send(query);
                return result != null ? File(result.Stream, result.ContentType) : BadRequest("Not found");
            }
            catch(Exception error)
            {
                return Conflict(error.Message);
            }
        }

        public class GetAttachmentByFileIdCommand : IRequest<FileResponse>
        {
            public Guid FileId { get; set; }
        }

        public class Handler : IRequestHandler<GetAttachmentByFileIdCommand, FileResponse>
        {
            private readonly IBlobService _blobService;

            public Handler(IBlobService blobService)
            {
                _blobService = blobService;
            }

            public async Task<FileResponse> Handle(GetAttachmentByFileIdCommand request, CancellationToken cancellationToken)
            {
                FileResponse fileResponse = await _blobService.DownloadAsync(request.FileId, cancellationToken: cancellationToken);

                return fileResponse;
            }
        }
    }
}
