using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices
{
    public interface IUserService
    {
        Task<Response<CreateUserResponseDTO>> CreateAsync(CreateUserDTO createUser);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        public Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);

        public Task<Response<List<UserGetDTO>>> GetAllUsersAsync();
        public Task<Response<bool>> AssignRoleToUserAsync(string userID, string[] roles);
        public Task<Response<string[]>> GetRolesToUserAsync(string userIdOrName);
        public Task<Response<bool>> DeleteUserAsync(string userIdOrName);
        public Task<Response<bool>> UpdateUserAsync(UserUpdateDTO userUpdateDTO);

    }
}
