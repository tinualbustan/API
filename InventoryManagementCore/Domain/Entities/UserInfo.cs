using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class UserInfo : Entity
    {
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public string? Password { get; set; }
        public required string Email { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }
}
