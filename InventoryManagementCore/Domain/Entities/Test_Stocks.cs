using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Test_Stocks : Entity
    {
        public string ItemName { get; set; }

        public double? Quantity { get; set; }
        public string? Unit { get; set; }
        public double? RatePerUnit { get; set; }
        public double? AlternativeQuantity { get; set; }
        public string? AlternativeUnit { get; set; }
        public double? AlternativeRatePerUnit { get; set; }
        public double? ConvertionRate { get; set; }
        public double? ActualQuantity { get; set; }
        public string? ActualUnit { get; set; }
        public string? Catagory { get; set; }
        public string? BatchId { get; set; }
        public string? Status { get; set; }
        public string? Supplier { get; set; }
        public bool? Active { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}
