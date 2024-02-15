using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult>ChangeApplicationStatus(string applicationId, string status)
        {
            var data = await _applicationService.UpdateApplicationStatus(applicationId, status);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TrackApplication(string userId, string applicationId)
        {
            var data = await _applicationService.TrackApplication(userId, applicationId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllApplications(bool isDelete=false)
        {
            var data = await _applicationService.GetAllApplications(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{applicationId}")]
        public async Task<IActionResult> CheckApplicationStatus(string applicationId)
        {
            var data = await _applicationService.CheckApplicationStatus(applicationId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetApplicationMetrics()
        {
            var data = await _applicationService.GetApplicationMetrics();
            return StatusCode(data.StatusCode, data);
        }

    }
}
