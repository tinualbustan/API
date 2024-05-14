using InventoryManagementCore.Domain.Entities;

namespace InventoryManagementCore.Application.DTOs
{
    public class Al_UserItemDto
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

        public Al_UserItem ToUserItemCreate() => new()
        {
            Email = Email,
            PhoneNumber = PhoneNumber,
            Role = Role,
            AddressLineOne = AddressLineOne,
            AddressLineTwo = AddressLineTwo,
            Country = Country,
            IsVerified = IsVerified,
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Active = Active,
            Id = Guid.NewGuid().ToString(),
            Password = Password
        };
    }
}
