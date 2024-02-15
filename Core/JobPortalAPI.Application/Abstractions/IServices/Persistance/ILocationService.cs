using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.LocationDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface ILocationService
    {
        Task<Response<bool>> AddPreferredLocation(string userId, string city, string country);
        Task<Response<List<JobPostGetDTO>>> SearchJobsByLocation(string city, string country);
        Task<Response<List<JobWithLocationsDTO>>> GetNearbyJobs(string userId);
        Task<Response<List<LocationGetDTO>>> GetLocations(bool isDelete = false);
        Task<Response<List<LocationTrendDTO>>> GetLocationTrends();
    }

}
