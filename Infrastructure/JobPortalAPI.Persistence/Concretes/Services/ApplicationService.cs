using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Exceptions.ApplicationExceptions;
using JobPortalAPI.Application.Exceptions.CategoryExceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationReadRepository _applicationReadRepository;
        private readonly IApplicationWriteRepository _applicationWriteRepository;

        private readonly IMapper _mapper;

        public ApplicationService(IApplicationReadRepository applicationReadRepository, IMapper mapper, IApplicationWriteRepository applicationWriteRepository)
        {
            _applicationReadRepository = applicationReadRepository;
            _mapper = mapper;
            _applicationWriteRepository = applicationWriteRepository;
        }

        public async Task<Response<ApplicationStatusDTO>> CheckApplicationStatus(string applicationId)
        {
            var application = await _applicationReadRepository.GetByIdAsync(applicationId);
            if (application != null)
            {
                var applicationStatusDTO = _mapper.Map<ApplicationStatusDTO>(application);

                return new Response<ApplicationStatusDTO>
                {
                    Data = applicationStatusDTO,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<ApplicationStatusDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Application)));
        }
  
        public async Task<Response<ApplicationMetricsDTO>> GetApplicationMetrics()
        {
            int totalApplicationsCount = await _applicationReadRepository.GetTotalApplicationsCount();

            int successfulApplicationsCount = await _applicationReadRepository.GetSuccessfulApplicationsCount();

            var applicationMetricsDTO = new ApplicationMetricsDTO
            {
                TotalApplications = totalApplicationsCount,
                SuccessfulApplications = successfulApplicationsCount
            };

            return new Response<ApplicationMetricsDTO>
            {
                Data = applicationMetricsDTO,
                StatusCode = 200
            };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(JobPost)));
        }

        public async Task<Response<ApplicationDTO>> TrackApplication(string userId, string applicationId)
        {
            var application= await _applicationReadRepository.GetApplicationByUserIdAndAppIdAsync(userId, applicationId);
            if (application != null)
            {
                var applicationDTO=_mapper.Map<ApplicationDTO>(application);

                return new Response<ApplicationDTO>
                {
                    Data = applicationDTO,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<ApplicationDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
        }


        public async Task<Response<List<ApplicationGetDTO>>> GetAllApplications(bool isDelete = false)
        {
            IQueryable<Domain.Entities.JobPortalDBContext.Application> query = _applicationReadRepository.GetAll(tracking: false);
            if (!isDelete)
            {
                query = query.Where(j => !j.IsDeleted);
            }
            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = _mapper.Map<List<ApplicationGetDTO>>(data);
                return new Response<List<ApplicationGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<ApplicationGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new ApplicationGetFailedException();
        }

        public async Task<Response<bool>> UpdateApplicationStatus(string applicationId, string status)
        {
            var application= await _applicationReadRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404
                };
            }

            application.Status = status;
            _applicationWriteRepository.Update(application);
            int result=await _applicationWriteRepository.SaveAsync();

            if (result>0)
            {
                return new Response<bool> { Data = true, StatusCode = 200 };

            }
            else
            {
                return new Response<bool> { Data=false, StatusCode = 500 };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Application)));
        }
    }
}
