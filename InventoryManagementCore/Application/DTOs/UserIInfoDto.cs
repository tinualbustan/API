using InventoryManagementCore.Domain.Entities;

namespace InventoryManagementCore.Application.DTOs
{
    public class UserInfoDto
    {
        public string UserId { get; set; } = string.Empty;
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; } = false;
        public required string Email { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsVerified { get; set; } = false;


        public UserInfoDto()
        {

        }
        public UserInfoDto(UserInfo userInfo)
        {
            UserId = userInfo.Id;
            Email = userInfo.Email;
            Roles = userInfo.Roles;
            FullName = userInfo.FullName;
            IsVerified = userInfo.IsVerified;
            UserName = userInfo.UserName;
            Active = userInfo.IsActive;
            Password = userInfo.Password;
        }
        public UserInfo ToUserInfo() => new()
        {
            Email = Email,
            Roles = Roles,
            FullName = FullName,
            IsVerified = IsVerified,
            UserName = UserName,
            IsActive = Active,
            Password = Password
        };
    }

}
