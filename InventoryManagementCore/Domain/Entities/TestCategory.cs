using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class TestCategory : Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}
