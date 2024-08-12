using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    class TestCollectionsContext : Entity
    {
        public string Flag { get; set; }
        public int Count { get; set; }
    }
}
