using InventoryManagementCore.API.ViewModels;
using InventoryManagementCore.Domain.Entities;

namespace InventoryManagementCore.Application.Interfaces
{
    public interface IUserRepo
    {
        Task<string> CreateUserAsync(UserInfo userInfo);
        Task<UserInfo> GetUserAsync(string id);
        Task<UserLoginResult> GetUserLoginAsync(string userName, string password);
    }
}
