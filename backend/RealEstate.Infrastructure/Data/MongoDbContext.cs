using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Property> Properties { get; }
        public IMongoCollection<Owner> Owners { get; }
        public IMongoCollection<PropertyImage> PropertyImages { get; }
        public IMongoCollection<PropertyTrace> PropertyTraces { get; }

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            Properties = _database.GetCollection<Property>(settings.Value.PropertyCollectionName);
            Owners = _database.GetCollection<Owner>(settings.Value.OwnerCollectionName);
            PropertyImages = _database.GetCollection<PropertyImage>(settings.Value.PropertyImageCollectionName);
            PropertyTraces = _database.GetCollection<PropertyTrace>(settings.Value.PropertyTraceCollectionName);
        }
    }
}
