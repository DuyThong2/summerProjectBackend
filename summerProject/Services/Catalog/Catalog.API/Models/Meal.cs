using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Meal : Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PackageId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }
    }
}
