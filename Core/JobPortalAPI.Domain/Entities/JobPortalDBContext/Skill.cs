using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Domain.Entities.Identity;

namespace JobPortalAPI.Domain.Entities.JobPortalDBContext
{
    public class Skill: BaseEntity
    {
        public string SkillName { get; set;}
        public string SkillDescription { get; set; }
        public List<AppUser> Users { get; set; }
        public ICollection<JobPost>? JobPosts { get; set; }
    }

}
