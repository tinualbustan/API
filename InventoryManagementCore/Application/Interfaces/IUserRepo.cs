using InventoryManagementCore.API.ViewModels;
using InventoryManagementCore.Domain.Entities;

namespace InventoryManagementCore.Application.Interfaces
{
    public interface IUserRepo
    {
        Task<string> CreateUserAsync(UserItem userInfo);
        Task<UserItem> GetUserAsync(string id);
        Task<UserLoginResult> GetUserLoginAsync(string userName, string password);
    }
}
