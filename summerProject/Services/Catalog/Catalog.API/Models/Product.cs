using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public Guid GlobalId = Guid.NewGuid();

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public static Product generateFromExistingProduct(Product entity)
        {
            return new Product
            {
                Name = entity.Name,
                Id = entity.Id,
                GlobalId = entity.GlobalId,
                Description = entity.Description,
                Price = entity.Price,
            };
        }
    }
}
