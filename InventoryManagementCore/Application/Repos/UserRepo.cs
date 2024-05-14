using InventoryManagementCore.API.ViewModels;
using InventoryManagementCore.Application.Interfaces;
using InventoryManagementCore.Domain.Entities;
using InventoryManagementCore.Infrastructure.Services;
using MongoDB.Driver;

namespace InventoryManagementCore.Application.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly IDbContext dbContext;

        public UserRepo(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> CreateUserAsync(Al_UserItem userInfo)
        {
            userInfo.Id = Guid.NewGuid().ToString();
            var col = dbContext.GetCollection<Al_UserItem>();
            await col.InsertOneAsync(userInfo);
            return userInfo.Id;
        }

        public async Task<Al_UserItem> GetUserAsync(string id)
        {
            var col = dbContext.GetCollection<Al_UserItem>();
            var item = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<UserLoginResult> GetUserLoginAsync(string userName, string password)
        {
            var col = dbContext.GetCollection<Al_UserItem>();
            var item = await col.Find(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password).FirstOrDefaultAsync();
            if (item != null)
                return new UserLoginResult { JwtToken = Guid.NewGuid().ToString(), UserRole = item?.Role};
            throw new Exception("User Not Found");
        }
    }
}
