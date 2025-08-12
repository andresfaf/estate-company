using MongoDB.Driver;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _collection;
        public OwnerRepository(MongoDbContext context)
        {
            _collection = context.Owners;
        }
        public async Task Add(Owner owner, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.InsertOneAsync(owner, cancellationToken: cancellationToken);
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

        public async Task<List<Owner>> GetAll(CancellationToken cancellationToken)
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

        public async Task<Owner> GetById(string id, CancellationToken cancellationToken)
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

        public async Task<bool> Update(Owner owner, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _collection.ReplaceOneAsync(
               filter: o => o.Id == owner.Id,
               replacement: owner
           );

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Error de conexion a la base de datos", ex);
            }

        }
    }
}
