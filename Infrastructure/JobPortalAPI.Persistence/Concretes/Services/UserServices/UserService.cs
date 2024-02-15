using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Application.Exceptions.PasswordExceptions;
using JobPortalAPI.Application.Exceptions.UserExceptions;
using JobPortalAPI.Application.Helpers;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<CreateUserResponseDTO>> CreateAsync(CreateUserDTO createUser)
        {
            var id = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = id,
                UserName = createUser.UserName,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                Email = createUser.Email,
                BirthDate = createUser.BirthDate,
                PhoneNumber = createUser.PhoneNumber
            }, createUser.Password);

            var response = new Response<CreateUserResponseDTO>
            {
                Data = new CreateUserResponseDTO { Succeeded = result.Succeeded },
                StatusCode = result.Succeeded ? 200 : 400
            };
            if (!result.Succeeded)
            {
                response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
            }

            AppUser user = await _userManager.FindByNameAsync(createUser.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(id);
            }
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return response;
        }

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
        /*        public async Task<Response<bool>> AssignRoleToUserAsync(string userID, string[] roles)
                {
                    try
                    {
                        // Kullanıcıyı bul
                        AppUser user = await _userManager.FindByIdAsync(userID);

                        if (user == null)
                        {
                            return new Response<bool> { Data = false, StatusCode = 400 }; // Kullanıcı bulunamadı
                        }

                        // Kullanıcının mevcut rollerini al
                        var userRoles = await _userManager.GetRolesAsync(user);

                        // Kullanıcıyı mevcut rollerinden kaldır

                        // Yeni rolleri kullanıcıya ekle
                        await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
                        IdentityResult result = await _userManager.AddToRolesAsync(user, roles);

                        if (result.Succeeded)
                        {
                          // Yeni rolleri token'ın içine ekleyin
                            foreach (var role in roles)
                            {
                                await ReplaceClaimAsync(user, new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Role, role));
                            }

                            return new Response<bool> { Data = true, StatusCode = 200 }; // Rol atama başarılı
                        }
                        else
                        {
                            return new Response<bool> { Data = false, StatusCode = 400 }; // Rol atama başarısız
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Application.Exceptions.OperationalException.InvalidOperationException(ex.Message, ex.InnerException);
                    }
                }*/


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
            // Kullanıcı nesnesi null değilse devam et
            if (user != null)
            {
                // Kullanıcının mevcut taleplerini al
                var userClaims = await _userManager.GetClaimsAsync(user);

                // Değiştirilecek talebin kullanıcı talepleri arasında olup olmadığını kontrol et
                if (userClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    // Mevcut talebi kaldır
                    await _userManager.RemoveClaimAsync(user, claim);

                    // Yeni talebi ekle
                    await _userManager.AddClaimAsync(user, newClaim);

                    // İşlem başarılı ise IdentityResult.Success döndür
                    return IdentityResult.Success;
                }
                else
                {
                    // Değiştirilecek talep bulunamadıysa hata döndür
                    return IdentityResult.Failed(new IdentityError { Description = "Specified claim not found for user" });
                }
            }
            else
            {
                // Kullanıcı nesnesi null ise hata döndür
                return IdentityResult.Failed(new IdentityError { Description = "User cannot be null" });
            }
        }
    }
}