using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Test_Orders : Entity
    {
        public string? CanteenName { get; set; }
        public string? UserId { get; set; }
        public string? OrderCode { get; set; }
        public List<OrderedItem> ProductsData { get; set; } = new List<OrderedItem>();
        public string? Status { get; set; }
        public DateTime? Date { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Shopkeeper { get; set; }
    }
}
