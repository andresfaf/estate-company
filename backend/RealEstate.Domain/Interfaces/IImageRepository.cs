namespace RealEstate.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<string> Upload(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);
        Task<string> DownloadConvertedBase64(string id, CancellationToken cancellationToken);
        Task Delete(string id, CancellationToken cancellationToken);
        Task<string> getById(string id, CancellationToken cancellationToken);
    }
}
