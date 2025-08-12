using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("dateSale")]
        public DateOnly DateSale { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("value")]
        public decimal value { get; set; }

        [BsonElement("tax")]
        public decimal Tax { get; set; }

        [BsonElement("idProperty")]
        public string IdProperty { get; set; } = string.Empty;
    }
}
