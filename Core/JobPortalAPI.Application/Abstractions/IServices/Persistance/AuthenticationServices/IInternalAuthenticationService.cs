using JobPortalAPI.Application.DTOs.TokenDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance.AuthenticationServices
{
    public interface IInternalAuthenticationService
    {
        Task<Response<TokenDTO>>LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime, int refreshTokenMoreLifeTime);
        Task<Response<TokenDTO>> RefreshTokenLoginAsync(string refreshToken, int accessTokenLifeTime, int refreshTokenMoreLifeTime);
        Task<Response<bool>> LogOut(string usernameOrEmail);

        public Task<string>PasswordResetAsync(string email);
        public Task<bool> VerifyResetTokenAsync(string resetToken, string userId);

    }
}
