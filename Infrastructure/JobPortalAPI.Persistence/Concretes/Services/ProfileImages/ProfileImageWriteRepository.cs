using JobPortalAPI.Application.Repositories.ProfileImages;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using JobPortalAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services.ProfileImages
{
    public class ProfileImageWriteRepository : WriteRepository<ProfileImage>, IProfileImageWriteRepository
    {
        private readonly AppDbContext appDbContext;
        public ProfileImageWriteRepository(AppDbContext context) : base(context)
        {
            appDbContext = context;
        }

        public async Task<ProfileImage> AddAsyncProfilePhoto(ProfileImage profileImage)
        {

            EntityEntry<ProfileImage> entityEntry = await Table.AddAsync(profileImage);
            return profileImage;
        }
    }
}