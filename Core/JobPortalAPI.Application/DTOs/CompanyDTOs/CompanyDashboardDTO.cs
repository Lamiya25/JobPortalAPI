using JobPortalAPI.Application.DTOs.ApplicationDTOs;

namespace JobPortalAPI.Application.DTOs.CompanyDTOs
{
    public class CompanyDashboardDTO
    {
        public CompanyDTO Company { get; set; }
        public List<JobPostSummaryDTO> RecentJobPosts { get; set; }
        public ApplicationAnalyticsDTO ApplicationAnalytics { get; set; }
    }



}
