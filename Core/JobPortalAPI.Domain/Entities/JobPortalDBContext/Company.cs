using JobPortalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace JobPortalAPI.Domain.Entities.JobPortalDBContext
{
    public class Company:BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyEmail { get; set; }
        public string Industry { get; set; }
        public string WebsiteURL { get; set; }
        public string LogoURL { get; set; }
        public int EstablishedYear { get; set; }
        public List<JobPost>? JobPosts { get; set; }
    }

}
