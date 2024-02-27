using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface IFileService
    {

        Task<ProfileImage> WriteProfileImageAsync(IFormFile file);
    }
}
