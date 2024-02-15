using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface IApplicationService
    {
        Task<Response<ApplicationDTO>> TrackApplication(string userId, string applicationId);

        Task<Response<ApplicationStatusDTO>> CheckApplicationStatus(string applicationId);

        Task<Response<ApplicationMetricsDTO>> GetApplicationMetrics();

        Task<Response<List<ApplicationGetDTO>>> GetAllApplications(bool isDelete = false);

        Task<Response<bool>> UpdateApplicationStatus(string applicationId, string status);
    }


}
