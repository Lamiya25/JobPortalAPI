using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Exceptions.CategoryExceptions;
using JobPortalAPI.Application.Exceptions.CompanyExceptions;
using JobPortalAPI.Application.Exceptions.JobPostExceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Repositories;
using JobPortalAPI.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IJobPostReadRepository _jobPostReadRepository;
        private readonly ICompanyWriteRepository _companyWriteRepository;
        private readonly IUnitOfWork<Company> _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyReadRepository companyReadRepository, ICompanyWriteRepository companyWriteRepository, IUnitOfWork<Company> unitOfWork, IMapper mapper, IJobPostReadRepository jobPostReadRepository)
        {
            _companyReadRepository = companyReadRepository;
            _companyWriteRepository = companyWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobPostReadRepository = jobPostReadRepository;
        }


        public async Task<Response<CompanyDTO>> RegisterCompany(CompanyDTO companyDTO)
        {
            if (companyDTO != null)
            {
                var companyEntity = _mapper.Map<Company>(companyDTO);

                await _companyWriteRepository.AddAsync(companyEntity);

                int result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return new Response<CompanyDTO>
                    {
                        Data = companyDTO,
                        StatusCode = 201
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Company)));
                }
            }
            else
            {
                return new Response<CompanyDTO>
                {
                    Data = companyDTO,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Company)));
        }

        public async Task<Response<List<CompanyGetDTO>>> GetCompanies(bool isDelete = false)
        {
            IQueryable<Company> query = _companyReadRepository.GetAll(tracking: false);
            if (!isDelete)
            {
                query = query.Where(j => !j.IsDeleted); 
            }

            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = _mapper.Map<List<CompanyGetDTO>>(data);
                return new Response<List<CompanyGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<CompanyGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new CategoryGetFailedException();
        }

        public async Task<Response<CompanyDashboardDTO>> GetCompanyDashboard(string companyId)
        {
            var companyIdGuid = Guid.Parse(companyId);
            var company = await _companyReadRepository.GetSingleAsync(c => c.Id == companyIdGuid);
            if (company == null)
            {
                return new Response<CompanyDashboardDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            var recentJobPosts = _jobPostReadRepository
                .GetWhere(j => j.CompanyID == companyIdGuid && !j.IsDeleted && j.ApplicationDeadline >= DateTime.Now)
                .OrderByDescending(j => j.PublishedDate)
                .ToList();

            var applicationAnalitics = new ApplicationAnalyticsDTO
            {
                TotalApplications = recentJobPosts.Sum(j => j.Applications.Count),
                PendingApplications = recentJobPosts.Sum(j => j.Applications.Count(a => a.Status == "Pending")),
                AcceptedApplications = recentJobPosts.Sum(j => j.Applications.Count(a => a.Status == "Accepted")),
                RejectedApplications = recentJobPosts.Sum(j => j.Applications.Count(a => a.Status == "Rejected")),
            };

            var companyDashboardDTO = new CompanyDashboardDTO
            {
                Company = _mapper.Map<CompanyDTO>(company),
                RecentJobPosts = _mapper.Map<List<JobPostSummaryDTO>>(recentJobPosts),
                ApplicationAnalytics = applicationAnalitics
            };

            return new Response<CompanyDashboardDTO>
            {
                Data = companyDashboardDTO,
                StatusCode = 200
            };
        }

        public async Task<Response<List<JobPostGetDTO>>> GetCompanyJobs(string companyId, bool isDelete=false)
        {
            var jobPosts = await _jobPostReadRepository.GetWhere(j => j.CompanyID.ToString() == companyId, isDelete=false).ToListAsync();

            var jobPostDTOs= _mapper.Map<List<JobPostGetDTO>>(jobPosts);
            return new Response<List<JobPostGetDTO>>
            {
                Data = jobPostDTOs,
                StatusCode = 200
            };
        }

        public async Task<Response<bool>> UpdateCompanyProfile(string companyId, CompanyUpdateDTO companyUpdateDTO)
        {
            var company = await _companyReadRepository.GetByIdAsync(companyId);

            if (company != null)
            {
                _mapper.Map(companyUpdateDTO, company);

                _companyWriteRepository.Update(company);
                int result = await _unitOfWork.SaveChangesAsync();
                if (result == 1)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Company)));
                }
            }
            else
            {
                return new Response<bool> { Data = false, StatusCode = 404 };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Company)));
        }

        public async Task<Response<bool>> DeleteCompany(string companyId)
        {
            if (string.IsNullOrEmpty(companyId) || !Guid.TryParse(companyId, out _))
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var category = await _companyReadRepository.GetByIdAsync(companyId);

            if (category == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404
                };
            }
            _companyWriteRepository.Remove(category);
            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 500
                };
            }
        }
    }
}
