using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostController : ControllerBase
    {
        private readonly IJobPostService _jobPostService;

        public JobPostController(IJobPostService jobPostService)
        {
                _jobPostService = jobPostService;
        }

      
        [HttpPost("[action]")]
   /*     [Authorize(AuthenticationSchemes = "Admin", Roles = "Recruiter")]*/
        public async Task<IActionResult> CreateJobPost(JobPostCreateDTO model)
        {
            var data = await _jobPostService.AddJobPost(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllJobPosts(bool isDelete = false)
        {
            var data = await _jobPostService.GetAllJobPosts(isDelete);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetJobPost(string Id, bool isDelete)
        {
            var data = await _jobPostService.GetJobPostById(Id);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetApplicationsForJob(string Id, bool isDelete)
        {
            var data = await _jobPostService.GetApplicationsForJob(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ApplyToJob(string userId, string jobId, ApplicationDTO applicationDTO)
        {
            var data = await _jobPostService.ApplyToJob(userId, jobId, applicationDTO);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteJobPost(string Id)
        {
            var data = await _jobPostService.DeleteJobPost(Id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateJobPost(JobPostUpdateDTO model)
        {
            var data = await _jobPostService.UpdateJobPost(model);
            return StatusCode(data.StatusCode, data);
        }
    }
}
