namespace RDF.Arcana.API.Abstractions.Storage
{
    public interface IBlobService
    {
        Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

        Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

        Task DeleteASync(Guid fileId, CancellationToken cancellationToken = default);
    }
}
