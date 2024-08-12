using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Test_ItemInfo : Entity
    {
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? Catagory { get; set; }
        public string? Unit { get; set; }
        public bool? Active { get; set; }
        public double? Rate { get; set; }
        public string? AlternativeUnit { get; set; } = "";
        public double? ConversionRate { get; set; } = 1;
        public double? ItemCount { get; set; } = 0;
    }
}
