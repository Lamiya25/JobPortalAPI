using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Domain.Entities.Identity;

namespace JobPortalAPI.Domain.Entities.JobPortalDBContext
{
    public class Application:BaseEntity
    {
        public string UserID { get; set; }
        public Guid JobPostID { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }

        public AppUser User { get; set; }
        public JobPost JobPost { get; set; }
    }

}
