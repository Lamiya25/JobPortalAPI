using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Concretes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
                _companyService = companyService;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> RegisterCompany(CompanyDTO model)
        {
            var data = await _companyService.RegisterCompany(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var data = await _companyService.GetCompanies();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{companyId}")]
        public async Task<IActionResult> GetCompanyJobs(string companyId)
        {
            var data = await _companyService.GetCompanyJobs(companyId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanyDashboard(string companyId)
        {
            var data = await _companyService.GetCompanyDashboard(companyId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateCompanyProfile(string companyId, CompanyUpdateDTO companyUpdateDTO)
        {
            var data = await _companyService.UpdateCompanyProfile(companyId, companyUpdateDTO);
            return StatusCode(data.StatusCode, data);
        }


        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteCompany(string companyId)
        {
            var data = await _companyService.DeleteCompany(companyId);
            return StatusCode(data.StatusCode, data);
        }

    }
}
