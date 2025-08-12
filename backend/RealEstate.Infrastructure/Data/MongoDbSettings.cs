namespace RealEstate.Infrastructure.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string PropertyCollectionName { get; set; } = string.Empty;
        public string OwnerCollectionName { get; set; } = string.Empty;
        public string PropertyImageCollectionName { get; set; } = string.Empty;
        public string PropertyTraceCollectionName { get; set; } = string.Empty;
    }
}
