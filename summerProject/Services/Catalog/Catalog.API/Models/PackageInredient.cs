using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class PackageIngredient
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string IngredientId { get; set; } = null!;

        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
