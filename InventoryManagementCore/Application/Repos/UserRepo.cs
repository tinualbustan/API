using InventoryManagementCore.API.ViewModels;
using InventoryManagementCore.Application.DTOs;
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
        public async Task<string> CreateUserAsync(UserInfoDto userInfoDto)
        {
            var userInfo = userInfoDto.ToUserInfo();
            IMongoCollection<UserInfo> col = dbContext.GetCollection<UserInfo>();
            await col.InsertOneAsync(userInfo);
            return userInfo.Id;
        }

        public async Task<long> UpdateUserAsync(UserInfoDto userInfoDto)
        {
            var userInfo = userInfoDto.ToUserInfo();
            IMongoCollection<UserInfo> col = dbContext.GetCollection<UserInfo>();
            var result = await col.ReplaceOneAsync(x => x.Id == userInfo.Id, userInfo);
            return result.ModifiedCount;
        }

        public async Task<UserInfo> GetUserAsync(string id)
        {
            var col = dbContext.GetCollection<UserInfo>();
            UserInfo item = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<UserLoginResult> GetUserLoginAsync(string userName, string password)
        {
            var col = dbContext.GetCollection<UserInfo>();
            var item = await col.Find(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password).FirstOrDefaultAsync();
            if (item != null)
                return new UserLoginResult { JwtToken = "Dummy Token Ignore", UserRole = item?.Roles };
            throw new Exception("User Not Found");
        }
    }
}
