using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Abstractions.Storage;

namespace RDF.Arcana.API.Features.Storage
{
    [Route("api/file"), ApiController]
    public class GetFile : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetFile(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get/{fileId}")]
        public async Task<IActionResult> Get([FromRoute] Guid fileId )
        {
            var query = new GetFileQuery
            {
                FileId = fileId
            };

            var fileResponse = await _mediator.Send(query);

            return File(fileResponse.Stream, fileResponse.ContentType);
        }

        public class GetFileQuery : IRequest<FileResponse>
        {
            public Guid FileId { get; set; }
        }

        public class Hadnler : IRequestHandler<GetFileQuery, FileResponse>
        {
            private readonly IBlobService _blobSrvice;
            public Hadnler(IBlobService blobSrvice)
            {
                _blobSrvice = blobSrvice;
            }

            public async Task<FileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
            {
                FileResponse fileResponse = await _blobSrvice.DownloadAsync(request.FileId);

                return fileResponse;
            }
        }
    }
}
