using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using RealEstate.Domain.Interfaces;
using RealEstate.Application.Exceptions;

namespace RealEstate.Infrastructure.Repositories
{
    //Utilización de GridFs para manejo de imagenes
    public class ImageRepository : IImageRepository
    {
        private readonly IGridFSBucket _bucket;

        public ImageRepository(IMongoDatabase database)
        {
            _bucket = new GridFSBucket(database);
        }

        public async Task Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = MongoDB.Bson.ObjectId.Parse(id);
                await _bucket.DeleteAsync(objectId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<string> DownloadConvertedBase64(string id, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = MongoDB.Bson.ObjectId.Parse(id);
                var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId))
                                        .FirstOrDefaultAsync(cancellationToken);

                if (fileInfo == null)
                    throw new FileNotFoundException("Imagen no encontrada");

                var bytes = await _bucket.DownloadAsBytesAsync(objectId, cancellationToken: cancellationToken);
                var contentType = fileInfo.Metadata?["contentType"]?.AsString ?? "image/jpeg";
                var codeBase64 = Convert.ToBase64String(bytes);
                var base64Complete = $"data:{contentType};base64,{codeBase64}";

                return base64Complete;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<string> getById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = MongoDB.Bson.ObjectId.Parse(id);
                var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId))
                                        .FirstOrDefaultAsync(cancellationToken);
                if (fileInfo == null)
                    throw new FileNotFoundException("Imagen no encontrada");

                return fileInfo.Filename;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<string> Upload(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
        {
            try
            {
                var options = new GridFSUploadOptions
                {
                    Metadata = new MongoDB.Bson.BsonDocument
                    {
                        { "contentType", contentType }
                    }
                };

                var fileId = await _bucket.UploadFromStreamAsync(fileName, fileStream, options, cancellationToken);
                return fileId.ToString();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }
    }
}
