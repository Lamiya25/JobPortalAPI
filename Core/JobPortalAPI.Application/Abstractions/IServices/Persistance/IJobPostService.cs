using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface IJobPostService
    {
        Task<Response<JobPostCreateDTO>>AddJobPost(JobPostCreateDTO jobPostCreateDTO);
        Task<Response<bool>> DeleteJobPost(string id);
        Task<Response<bool>>UpdateJobPost(JobPostUpdateDTO jobPostUpdateDTO);

        Task<Response<List<JobPostGetDTO>>> GetAllJobPosts(bool isDelete=false);
        Task<Response<JobPostGetDTO>>GetJobPostById(string id);

        Task<Response<bool>>ApplyToJob(String userId, string jobId, ApplicationDTO applicationDTO);
        Task<Response<List<ApplicationDTO>>>GetApplicationsForJob(string  jobId, bool isDelete);

        Task<Response<bool>> SaveJobPost(string userId, string jobPostID);
    }

}
