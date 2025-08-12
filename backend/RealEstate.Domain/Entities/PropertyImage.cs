using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("file")]
        public string File { get; set; } = string.Empty;

        [BsonElement("enabled")]
        public bool Enabled { get; set; }

        [BsonElement("idProperty")]
        public string IdProperty { get; set; } = string.Empty;
    }
}
