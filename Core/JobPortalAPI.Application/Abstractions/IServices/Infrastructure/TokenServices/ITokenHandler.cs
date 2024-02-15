using JobPortalAPI.Application.DTOs.TokenDTOs;
using JobPortalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Infrastructure.TokenServices
{
    public interface ITokenHandler
    {
        Task<TokenDTO> CreateAccessToken(int minute, AppUser user);
        string CreateRefreshToken();
    }
}
