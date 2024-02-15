using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Persistence.Concretes.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthoService _authoService;

        public AuthController(IAuthoService authoService)
        {
            _authoService = authoService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult>Login(string usernameOrEmail, string password, int accessTokenLifeTime, int refreshTokenLifetime)
        {
            var data = await _authoService.LoginAsync(usernameOrEmail, password, accessTokenLifeTime, refreshTokenLifetime);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken, int accesTokenLifeTime, int refreshTokenLifetime)
        {
            var data = await _authoService.RefreshTokenLoginAsync(refreshToken, accesTokenLifeTime, refreshTokenLifetime);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> LogOut(string userNameOrEmail)
        {
            var data = await _authoService.LogOut(userNameOrEmail);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("password-reset-token")]
        public async Task<IActionResult> PasswordReset(string email)
        {

            var data = await _authoService.PasswordResetAsync(email);
            return Ok(data);
        }

        [HttpGet("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken(string token, string userId)
        {

            var response = await _authoService.VerifyResetTokenAsync(token, userId);
            return Ok(response);
        }
    }
}
