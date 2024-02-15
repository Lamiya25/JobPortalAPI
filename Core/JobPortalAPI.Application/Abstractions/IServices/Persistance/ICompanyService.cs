using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.Models.ResponseModels;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface ICompanyService
    {
        Task<Response<CompanyDTO>>RegisterCompany(CompanyDTO companyDTO);
        Task<Response<bool>>UpdateCompanyProfile(string companyId, CompanyUpdateDTO companyUpdateDTO);
        Task<Response<List<JobPostGetDTO>>>GetCompanyJobs(string companyId, bool isDelete=false);
        Task<Response<List<CompanyGetDTO>>> GetCompanies(bool isDelete = false);
        Task<Response<CompanyDashboardDTO>> GetCompanyDashboard(string companyId);
        Task<Response<bool>> DeleteCompany(string companyId);
    }

}
