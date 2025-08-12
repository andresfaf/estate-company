using MongoDB.Driver;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Application.Exceptions;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;
        public PropertyRepository(MongoDbContext context)
        {
            _collection = context.Properties;
        }
        public async Task Add(Property property, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.InsertOneAsync(property, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<List<Property>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<List<Property>> GetByFilters(string? name, string? address, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken)
        {
            try
            {
                var filterBuilder = Builders<Property>.Filter;
                var filter = filterBuilder.Empty;

                if (!string.IsNullOrWhiteSpace(name))
                    filter &= filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));

                if (!string.IsNullOrWhiteSpace(address))
                    filter &= filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));

                if (minPrice.HasValue)
                    filter &= filterBuilder.Gte(p => p.Price, minPrice.Value);

                if (maxPrice.HasValue)
                    filter &= filterBuilder.Lte(p => p.Price, maxPrice.Value);

                return await _collection.Find(filter).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.DeleteOneAsync(o => o.Id == id, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {

                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }

        public async Task<Property> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                return await _collection.Find(filter: o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }
        }
    }
}
