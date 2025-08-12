using MongoDB.Driver;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IMongoCollection<PropertyTrace> _collection;
        public PropertyTraceRepository(MongoDbContext context)
        {
            _collection = context.PropertyTraces;
        }

        public async Task Add(PropertyTrace propertyTrace, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.InsertOneAsync(propertyTrace, cancellationToken: cancellationToken);
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

        public async Task<List<PropertyTrace>> GetByPropertyId(string propertyId, CancellationToken cancellationToken)
        {
            try
            {
                return await _collection.Find(filter: i => i.IdProperty == propertyId).SortBy(i => i.DateSale).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }
    }
}
