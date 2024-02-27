using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Application.Exceptions.PasswordExceptions;
using JobPortalAPI.Application.Exceptions.UserExceptions;
using JobPortalAPI.Application.Helpers;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Validators;
using JobPortalAPI.Domain.Entities.Identity;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using FluentValidation.Results;
using JobPortalAPI.Application.Consts;
using Microsoft.AspNetCore.Http.HttpResults;
using JobPortalAPI.Persistence.Context;

namespace JobPortalAPI.Persistence.Concretes.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IFileService fileService, AppDbContext context = null)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileService = fileService;
            _context = context;
        }

        public async Task<Response<CreateUserResponseDTO>> CreateAsync(CreateUserDTO createUser)
        {
            try
            {
                AppUser appUser = _mapper.Map<AppUser>(createUser);
            appUser.Id = Guid.NewGuid().ToString();
            ValidationResult vResult = await ValidateUserAsync(appUser);


            if (await IsEmailExist(appUser.Email))
                return new Response<CreateUserResponseDTO>
                {
                    Data = new CreateUserResponseDTO { Succeeded = false, Errors = new List<string> { Messages.UsedEmailMessage } },
                    StatusCode = 400
                };

            if (vResult.IsValid)
            {
                IdentityResult result = await _userManager.CreateAsync(appUser, createUser.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "User");
                    return new Response<CreateUserResponseDTO>
                    {
                        Data = new CreateUserResponseDTO { Succeeded = true },
                        StatusCode = 200
                    };
                }
                return new Response<CreateUserResponseDTO>
                {
                    Data = new CreateUserResponseDTO { Succeeded = false, Errors = result.Errors.Select(x => x.Description).ToList() },
                    StatusCode = 400
                };
            }
            return new Response<CreateUserResponseDTO>
            {
                Data = new CreateUserResponseDTO { Succeeded = false, Errors = vResult.Errors.Select(x => x.ErrorMessage).ToList() },
                StatusCode = 400
            };
        }
            catch (Exception ex)
            {
                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }
        }
        private async Task<bool> IsEmailExist(string email) => await _context.Users.AnyAsync(x => x.Email == email);

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpires = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Application.Exceptions.UserExceptions.NotFoundUserException();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenb = WebEncoders.Base64UrlDecode(resetToken);
                resetToken = Encoding.UTF8.GetString(tokenb);

                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded) { await _userManager.UpdateSecurityStampAsync(user); }
                else { throw new PasswordChangeFailedException(); }
            }
            else
            {
                throw new Application.Exceptions.UserExceptions.NotFoundUserException();
            }
        }

        public async Task<Response<List<UserGetDTO>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            try
            {
                if (users != null && users.Count > 0)
                {
                    var data = _mapper.Map<List<UserGetDTO>>(users);
                    return new Response<List<UserGetDTO>>
                    {
                        Data = data,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<List<UserGetDTO>>
                    {
                        Data = null,
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {

                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }

        }

        public async Task<Response<bool>> AssignRoleToUserAsync(string userID, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userID);
            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, roles);
                    return new()
                    {
                        Data = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new() { Data = false, StatusCode = 400 };
                }
            }
            catch (Exception ex)
            {

                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Response<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }
            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    return new()
                    {
                        Data = userRoles.ToArray(),
                        StatusCode = 200
                    };
                }
                return new()
                {
                    Data = null,
                    StatusCode = 400
                };
            }
            catch (Exception ex)
            {

                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Response<bool>> DeleteUserAsync(string userIdOrName)
        {

            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user == null)
                throw new Application.Exceptions.UserExceptions.NotFoundUserException();

            try
            {
                var data = await _userManager.DeleteAsync(user);
                if (data.Succeeded)
                {
                    return new()
                    {
                        Data = true,
                        StatusCode = 200
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
            }
            catch (Exception ex)
            {
                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Response<bool>> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            AppUser user = await _userManager.FindByIdAsync(userUpdateDTO.UserId);
            if (user == null)
                user = await _userManager.FindByNameAsync(userUpdateDTO.UserName);

            if (user == null)
                throw new Application.Exceptions.UserExceptions.NotFoundUserException();

            try
            {
                user.UserName = userUpdateDTO.UserName;
                user.BirthDate = userUpdateDTO.BirthDate;
                user.Email = userUpdateDTO.Email;
                user.FirstName = userUpdateDTO.FirstName;
                user.LastName = userUpdateDTO.LastName;
                user.PhoneNumber = userUpdateDTO.PhoneNumber;

                var data = await _userManager.UpdateAsync(user);

                if (data.Succeeded)
                {
                    return new()
                    {
                        Data = true,
                        StatusCode = 200
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
            }
            catch (Exception ex)
            {
                throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IdentityResult> ReplaceClaimAsync(AppUser user, Claim claim, Claim newClaim)
        {
            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                if (userClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await _userManager.RemoveClaimAsync(user, claim);

                    await _userManager.AddClaimAsync(user, newClaim);

                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Specified claim not found for user" });
                }
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "User cannot be null" });
            }
        }
        public async Task UploadProfileImageAsync(string userId, IFormFile file)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            if (user.ProfileImageId != null)
            {
                await DeleteProfileImageAsync(userId);
            }
            ProfileImage profileImage = await _fileService.WriteProfileImageAsync(file);

            user.ProfileImageId=profileImage.Id;

            await _userManager.UpdateAsync(user);
        }


        public async Task<Response<bool>> DeleteProfileImageAsync(string userId)
        {
            AppUser user= await _userManager.FindByIdAsync(userId);

            user.ProfileImageId = null;
            await _userManager.UpdateAsync(user);

            return new Response<bool>
            {
                Data = true,
                StatusCode = 200
            };
        }

        private async Task<ValidationResult> ValidateUserAsync(AppUser user)
        {
            CreateUserValidator validationRules = new();

            ValidationResult result = await validationRules.ValidateAsync(user);
            return result;

        }

    }
}