namespace InventoryManagementCore.API.ViewModels
{
    public class UserLoginVm
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
    public class UserLoginResult
    {
        public string? JwtToken { get; set; }
        public List<string>? UserRole { get; set; }
    }
}
