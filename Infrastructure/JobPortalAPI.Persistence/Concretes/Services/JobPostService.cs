using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Exceptions.JobPostExceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Repositories;
using JobPortalAPI.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostReadRepository _jobPostReadRepository;
        private readonly IJobPostWriteRepository _jobPostWriteRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IUnitOfWork<JobPost> _unitOfWork;
        private readonly IMapper _mapper;


        public JobPostService(IJobPostReadRepository jobPostReadRepository, IJobPostWriteRepository jobPostWriteRepository, IUnitOfWork<JobPost> unitOfWork,IMapper mapper, ICompanyReadRepository companyReadRepository)
        {
            _jobPostReadRepository = jobPostReadRepository;
            _jobPostWriteRepository = jobPostWriteRepository;
            _companyReadRepository = companyReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
                
        }

        public async Task<Response<JobPostCreateDTO>> AddJobPost(JobPostCreateDTO jobPostCreateDTO)
        {
            if (jobPostCreateDTO != null)
            {
                var company = await _companyReadRepository.GetByIdAsync(jobPostCreateDTO.CompanyID.ToString());
                if (company == null)
                {
                    return new Response<JobPostCreateDTO>
                    {
                        Data = jobPostCreateDTO,
                        StatusCode = 400
                    };

                }
                var jobPostEntity = _mapper.Map<JobPost>(jobPostCreateDTO);
                jobPostEntity.Company = company;
                jobPostEntity.RequiredSkills = jobPostCreateDTO.RequiredSkills.Select(skillDto =>
           new Skill
           {
               SkillName = skillDto.SkillName,
               SkillDescription = skillDto.SkillDescription,
           }).ToList();


                await _jobPostWriteRepository.AddAsync(jobPostEntity);

                int result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<JobPostCreateDTO>
                    {
                        Data = jobPostCreateDTO,
                        StatusCode = 201
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(JobPost)));
                }
            }
            else
            {
                return new Response<JobPostCreateDTO>
                {
                    Data = jobPostCreateDTO,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(JobPost)));
        }

        public async Task<Response<bool>> ApplyToJob(string userId, string jobId, ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null || string.IsNullOrEmpty(userId))
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400
                };
            }
                var jobPost = await _jobPostReadRepository.GetByIdAsync(jobId);
                if (jobPost == null)
                {
                    return new Response<bool>
                    {
                        Data = false,
                        StatusCode = 404
                    };
                }

                var application = _mapper.Map<Domain.Entities.JobPortalDBContext.Application>(applicationDTO);
                application.UserID=userId;
                application.JobPostID = jobPost.Id;
                application.AppliedDate = DateTime.UtcNow;
                application.Status = "Pending";
            await _jobPostWriteRepository.AddApplicationAsync(application);
                int result=await _unitOfWork.SaveChangesAsync();

                if (result>0)
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

        public async Task<Response<bool>> DeleteJobPost(string id)
        {
            var deletedData = await _jobPostReadRepository.GetByIdAsync(id);
            if (deletedData != null)
            {
                _jobPostWriteRepository.Remove(deletedData);
                var affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
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
            else
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400
                };
            }
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 3), nameof(JobPost)));
        }

        public async Task<Response<List<JobPostGetDTO>>> GetAllJobPosts(bool isDelete = false)
        {
            IQueryable<JobPost> query = _jobPostReadRepository.GetAll(tracking: false);

            if (!isDelete)
            {
                query = query.Where(j => !j.IsDeleted);
            }

            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var jobPostDtos = _mapper.Map<List<JobPostGetDTO>>(data);
                return new Response<List<JobPostGetDTO>>
                {
                    Data = jobPostDtos,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<JobPostGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new JobPostGetFailedException();
        }


        public async Task<Response<JobPostGetDTO>> GetJobPostById(string id)
        {
            var data = await _jobPostReadRepository.GetByIdAsync(id);

            if (data != null)
            {
                var dtos = _mapper.Map<JobPostGetDTO>(data);

                return new Response<JobPostGetDTO>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<JobPostGetDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new JobPostGetFailedException(id);
        }

        public async Task<Response<bool>> UpdateJobPost(JobPostUpdateDTO jobPostUpdateDTO)
        {
            var data = await _jobPostReadRepository.GetByIdAsync(jobPostUpdateDTO.JobId);
            if (data != null)
            {
                _jobPostWriteRepository.Update(data);
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
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(JobPost)));
                }

            }
            else
                return new Response<bool> { Data = false, StatusCode = 404 };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(JobPost)));
        }

        public async Task<Response<List<ApplicationDTO>>> GetApplicationsForJob(string jobId, bool isDelete)
        {
            var jobPost=await _jobPostReadRepository.GetByIdAsync(jobId.ToString(),false, isDelete);
            if (jobPost == null)
            {
                return new Response<List<ApplicationDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            var applications=jobPost.Applications;
            if (applications !=null && applications.Any())
            {
                var applicationDTOs = _mapper.Map<List<ApplicationDTO>>(applications);
                return new Response<List<ApplicationDTO>>
                {
                    Data = applicationDTOs,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<ApplicationDTO>>
                {
                    Data = null,
                    StatusCode = 204
                };
            }
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(JobPost)));
    }

        public Task<Response<bool>> SaveJobPost(string userId, string jobPostID)
        {
            throw new NotImplementedException();
        }
    }
}
