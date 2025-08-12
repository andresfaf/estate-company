using MongoDB.Driver;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly IMongoCollection<PropertyImage> _collection;
        public PropertyImageRepository(MongoDbContext context)
        {
            _collection = context.PropertyImages;
        }

        public async Task AddMany(List<PropertyImage> propertyImages, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.InsertManyAsync(propertyImages, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task DeleteManyByPropertyId(string propertyId, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.DeleteManyAsync(img => img.IdProperty == propertyId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<PropertyImage> GetByPropertyIdEnabled(string id, CancellationToken cancellationToken)
        {
            try
            {
                return await _collection.Find(filter: i => (i.IdProperty == id && i.Enabled)).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<List<PropertyImage>> GetAllByPropertyId(string propertyId, CancellationToken cancellationToken)
        {
            try
            {
                return await _collection.Find(filter: i => i.IdProperty == propertyId).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }
    }
}
