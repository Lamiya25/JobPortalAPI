using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Concretes.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly private IUserService _userService;

        public UserController(IUserService userService)
        {
                _userService=userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDTO createUserDTO)
        {
            var data=await _userService.CreateAsync(createUserDTO);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string userId, string token, string newPassword)
        {
            _userService.UpdatePasswordAsync(userId, token, newPassword);
            return Ok();
        }

        [HttpGet]
     /*   [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]*/
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetAllUsersAsync();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetRolesToUser(string UserId)
        {
            var data = await _userService.GetRolesToUserAsync(UserId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string UserId, string[] Roles)
        {
            var data = await _userService.AssignRoleToUserAsync(UserId, Roles);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO model)
        {
            var data = await _userService.UpdateUserAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteToUser(string UserIdOrName)
        {
            var data = await _userService.DeleteUserAsync(UserIdOrName);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserProfile(string userId, IFormFile file)
        {
            await _userService.UploadProfileImageAsync(userId, file);
            return Ok(file);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProfileImage(string userId)
        {
            var data = await _userService.DeleteProfileImageAsync(userId);
            return StatusCode(data.StatusCode, data);
        }

    }
}
