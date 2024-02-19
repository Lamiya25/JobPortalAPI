using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JobPortalAPI.Application.DTOs.JobPostDTOs
{
    public class JobPostUpdateDTO
    {
        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string EmploymentType { get; set; }
        public float? MinSalary { get; set; }
        public float? MaxSalary { get; set; }
        public DateTime ApplicationDeadline { get; set; }
    }

}

