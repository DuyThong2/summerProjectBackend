using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Package : Product
    {
        public List<PackageIngredient> Ingredients { get; set; } = new();

    }
}
