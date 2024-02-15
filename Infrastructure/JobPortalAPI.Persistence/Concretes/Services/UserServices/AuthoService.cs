using JobPortalAPI.Application.Abstractions.IServices.Infrastructure.TokenServices;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Application.DTOs.TokenDTOs;
using JobPortalAPI.Application.Exceptions.AuthenticationExceptions;
using JobPortalAPI.Application.Exceptions.UserExceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services.UserServices
{
    public class AuthoService : IAuthoService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AuthoService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager= signInManager;
            _userManager= userManager;
            _tokenHandler= tokenHandler;
            _userService= userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<TokenDTO>> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime, int refreshTokenMoreLifeTime)
        {
            AppUser user= await _userManager.FindByNameAsync(usernameOrEmail);
            if (user==null)
            {
                user=await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            if (user==null)
            {
                throw new NotFoundUserException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                TokenDTO tokenDto = await _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(tokenDto.RefreshToken, user, tokenDto.Expiration, accessTokenLifeTime);
                return new()
                {
                    Data = tokenDto,
                    StatusCode = 200
                };
            }
            else
            {
                return new()
                {
                    Data = null,
                    StatusCode = 401
                };
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Response<TokenDTO>> RefreshTokenLoginAsync(string refreshToken, int accessTokenLifeTime, int refreshTokenMoreLifeTime)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenExpires > DateTime.UtcNow)
            {
                TokenDTO token = await _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, refreshTokenMoreLifeTime);
                return new()
                {
                    Data = token,
                    StatusCode = 200,
                };
            }
            else
            {
                return new()
                {
                    Data = null,
                    StatusCode = 401,
                };
            }
            throw new NotFoundUserException();
        }

        public async Task<Response<bool>> LogOut(string usernameOrEmail)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();


            user.RefreshTokenExpires = null;
            user.RefreshToken = null;

            var result = await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();

            if (result.Succeeded)
            {
                return new()
                {
                    Data = true,
                    StatusCode = 200,
                };
            }
            else
            {
                return new()
                {
                    Data = false,
                    StatusCode = 400
                };
            }

            throw new AuthenticationErrorException();
        }

        public async Task<string> PasswordResetAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                return resetToken;
            }
            return null;
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                resetToken = Encoding.UTF8.GetString(tokenBytes);

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
