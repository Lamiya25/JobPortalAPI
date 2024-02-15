using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Domain.Entities.Identity;

namespace JobPortalAPI.Domain.Entities.JobPortalDBContext
{
    public class Location:BaseEntity
    {
        public string City { get; set; }
        public string Country { get; set; }
        public List<JobPost> JobPosts { get; set; }

        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }

}
