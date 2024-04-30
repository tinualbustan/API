namespace InventoryManagementCore.API.ViewModels
{
    public class UserLoginVm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginResult
    {
        public string JwtToken { get; set; }
        public string UserRole { get; set; }
    }
}
