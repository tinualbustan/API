using InventoryManagementCore.API.ViewModels;
using InventoryManagementCore.Application.Interfaces;
using InventoryManagementCore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementCore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo userRepo;

        public UserController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync(Al_UserItem userInfo)
        {
            var res = await userRepo.CreateUserAsync(userInfo);
            return Ok(res);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            Al_UserItem res = await userRepo.GetUserAsync(id);
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LofinUserAsync(UserLoginVm loginVm)
        {
            UserLoginResult res = await userRepo.GetUserLoginAsync(loginVm.UserName,loginVm.Password);
            return Ok(res);
        }
    }
}
