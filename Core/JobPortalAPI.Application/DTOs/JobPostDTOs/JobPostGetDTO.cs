namespace JobPortalAPI.Application.DTOs
{
    public class JobPostGetDTO
    {
        public string JobPostId { get; set; }
        public string JobTitle { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public float? MinSalary { get; set; }
        public float? MaxSalary { get; set; }
        public string JobDescription { get; set; }
        public string EmploymentType { get; set; }
    }



}

