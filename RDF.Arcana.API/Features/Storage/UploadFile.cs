using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Abstractions.Storage;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Storage
{

    [Route("upload"), ApiController]
    public class UploadFile : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadFile(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("files")]
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result) : BadRequest(result); 
        }
        public class UploadFileCommand : IRequest<Result>
        {
            public IFormFile File { get; set; }
        }

        public class UpliadFileResult
        {
            public string FileId { get; set; }
        }

        public class Handler : IRequestHandler<UploadFileCommand, Result>
        {
            private readonly IBlobService _blobService;

            public Handler(IBlobService blobService)
            {
                _blobService = blobService;
            }

            public async Task<Result> Handle(UploadFileCommand request, CancellationToken cancellationToken)
            {
                using Stream stream = request.File.OpenReadStream();
               Guid fileId = await _blobService.UploadAsync(stream, request.File.ContentType);

                return Result.Success(fileId);
            }
        }
    }
}
