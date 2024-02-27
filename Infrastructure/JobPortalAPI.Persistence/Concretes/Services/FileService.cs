using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage;
using JobPortalAPI.Application.Consts;
using JobPortalAPI.Application.Repositories.ProfileImages;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageService _storageService;
        private readonly IProfileImageWriteRepository _profileImageWrite;

        public FileService(IProfileImageWriteRepository profileImageWrite, IStorageService storageService)
        {
            _profileImageWrite = profileImageWrite;
            _storageService = storageService;
        }
        public async Task<ProfileImage> WriteProfileImageAsync(IFormFile file)
        {
            string pathName = UploadPaths.ProfileImagePathName;
            var storageInfo = await _storageService.UploadAsync(pathName, file);

            Guid profileImageId = Guid.NewGuid();

            ProfileImage profileImage = await _profileImageWrite.AddAsyncProfilePhoto(new ProfileImage
            {
                Id = profileImageId,
                FileName = storageInfo.fileName,
                Path = storageInfo.pathName,
                Storage = _storageService.StorageName,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false
            });

            await _profileImageWrite.SaveAsync();
            return profileImage;
        }
    }
}
