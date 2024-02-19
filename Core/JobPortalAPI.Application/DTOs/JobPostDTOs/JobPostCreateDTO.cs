using JobPortalAPI.Application.DTOs.SkillDTOs;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.JobPostDTOs
{
    public class JobPostCreateDTO
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string EmploymentType { get; set; }
        public float? MinSalary { get; set; }
        public float? MaxSalary { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public Guid CompanyID { get; set; }
        public Guid LocationID { get; set; }

        public List<SkillDTO>? RequiredSkills { get; set; }
    }


}

