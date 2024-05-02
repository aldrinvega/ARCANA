using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RDF.Arcana.API.Abstractions.Storage;

namespace RDF.Arcana.API.Features.Storage
{
    internal sealed class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobService(BlobServiceClient blobServiceClient)
        {
             _blobServiceClient = blobServiceClient;
        }
        private const string ContainerName = "files";

        
        public async Task DeleteASync(Guid fileId, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public async Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

            return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
        }

        public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationtoken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            var fileId = Guid.NewGuid();
            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.UploadAsync(
                stream, 
                new BlobHttpHeaders { ContentType = contentType }, 
                cancellationToken: cancellationtoken);

            return fileId;
        }
    }
}
