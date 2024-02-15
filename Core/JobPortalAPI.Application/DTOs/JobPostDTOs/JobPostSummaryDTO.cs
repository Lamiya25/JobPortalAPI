namespace JobPortalAPI.Application.DTOs
{
    public class JobPostSummaryDTO
    {
        public int JobPostId { get; set; }
        public string JobTitle { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int NumberOfApplications { get; set; }
    }



}

