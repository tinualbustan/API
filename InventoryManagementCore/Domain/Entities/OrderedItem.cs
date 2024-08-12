using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class OrderedItem
    {
        public string? ItemId { get; set; }
        public string? ItemName { get; set; }
        public double? ItemCount { get; set; }
        public double? ActualItemCount { get; set; }
        public string? Unit { get; set; }
        public double? AltItemCount { get; set; }
        public string? AltUnit { get; set; }
        public DateTime? ExpireDate { get; set; }


    }
}
