using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementCore.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Al_UserItem : Entity
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; } = false;
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Role { get; set; }
        public string? AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string? Country { get; set; }
        public bool IsVerified { get; set; } = false;

    }
}
