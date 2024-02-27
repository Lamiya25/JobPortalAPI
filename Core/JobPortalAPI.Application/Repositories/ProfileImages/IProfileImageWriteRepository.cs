using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories.ProfileImages
{
    public interface IProfileImageWriteRepository:IWriteRepository<ProfileImage>
    {
        Task<ProfileImage> AddAsyncProfilePhoto(ProfileImage profileImage);
    }
}
