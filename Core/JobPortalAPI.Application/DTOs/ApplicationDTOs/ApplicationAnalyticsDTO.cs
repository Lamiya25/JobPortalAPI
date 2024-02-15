namespace JobPortalAPI.Application.DTOs.ApplicationDTOs
{
    public class ApplicationAnalyticsDTO
    {
        public int TotalApplications { get; set; }
        public int PendingApplications { get; set; }
        public int AcceptedApplications { get; set; }
        public int RejectedApplications { get; set; }
    }

}
