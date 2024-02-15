using AutoMapper;
using FluentValidation.Internal;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.DTOs.LocationDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationReadRepository _locationReadRepository;
        private readonly ILocationWriteRepository _locationWriteRepository;
        private readonly IJobPostReadRepository _jobPostReadRepository;
        private readonly IUnitOfWork<Location> _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(ILocationReadRepository locationReadRepository, ILocationWriteRepository locationWriteRepository, IUnitOfWork<Location> unitOfWork, IMapper mapper, IJobPostReadRepository jobPostReadRepository)
        {
            _locationReadRepository = locationReadRepository;
            _locationWriteRepository = locationWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobPostReadRepository = jobPostReadRepository;
        }

        public async Task<Response<bool>> AddPreferredLocation(string userId, string city, string country)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404
                };
            }
            var existingLocation = await _locationReadRepository.GetSingleAsync(l => l.AppUserId == userId);
            if (existingLocation != null)
            {
                existingLocation.City = city;
                existingLocation.Country = country;
                _locationWriteRepository.Update(existingLocation);
            }
            else
            {
                // Create a new location entry
                var newLocation = new Location
                {
                    AppUserId = userId,
                    City = city,
                    Country = country
                };

                _locationWriteRepository.AddAsync(newLocation);
            }
            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = 201
                };
            }
            else
            {
                throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Location)));
            }
        }

        public async Task<Response<List<LocationGetDTO>>> GetLocations(bool isDelete = false)
        {
            var query = _locationReadRepository.GetAll(tracking:false);
            if (!isDelete)
            {
                query = query.Where(j => !j.IsDeleted);
            }
            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = _mapper.Map<List<LocationGetDTO>>(data);

                return new Response<List<LocationGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<LocationGetDTO>>
                {
                    Data = null,
                    StatusCode = 200
                };
            }
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Location)));
        }

        public async Task<Response<List<LocationTrendDTO>>> GetLocationTrends()
        {
            var jobPosts = await _jobPostReadRepository.GetAll().ToListAsync();

            var locationTrends = jobPosts.GroupBy(j => j.LocationID)
                                        .Select(g => new LocationTrendDTO
                                        {
                                            LocationID = g.Key.ToString(),
                                            NumberOfJobPosts = g.Count()
                                        })
                                        .ToList();
            // Order the location trends by the number of job posts in descending order
            locationTrends = locationTrends.OrderByDescending(trend => trend.NumberOfJobPosts)
                                          .ToList();
            return new Response<List<LocationTrendDTO>>
            {
                Data = locationTrends,
                StatusCode = 200,
            };
        }

        public async Task<Response<List<JobWithLocationsDTO>>> GetNearbyJobs(string userId)
        {
            if (userId != null)
            {
                var userLocations = await _locationReadRepository.GetWhere(l => l.AppUserId == userId).ToListAsync();

                if (userLocations == null || !userLocations.Any())
                {
                    return new Response<List<JobWithLocationsDTO>>
                    {
                        Data = null,
                        StatusCode = 404,
                    };
                }

                var nearbyJobs = await _jobPostReadRepository.GetJobsByLocations(userLocations.Select(l => l.City).ToList(), userLocations.Select(l => l.Country).ToList());

                var nearbyJobsDTOs = _mapper.Map<List<JobWithLocationsDTO>>(nearbyJobs);

                return new Response<List<JobWithLocationsDTO>>
                {
                    Data = nearbyJobsDTOs,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<JobWithLocationsDTO>>
                {
                    Data = null,
                    StatusCode = 400,
                };
            }
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(JobPost)));
        }

        public async Task<Response<List<JobPostGetDTO>>> SearchJobsByLocation(string city, string country)
        {
            var jobs = await _jobPostReadRepository.SearchJobsByLocation(city, country);
            if (jobs == null || !jobs.Any())
            {
                return new Response<List<JobPostGetDTO>>
                {
                    Data = null,
                    StatusCode = 404,
                };
            }
            var jobPostDTOs = _mapper.Map<List<JobPostGetDTO>>(jobs);

            return new Response<List<JobPostGetDTO>>
            {
                Data = jobPostDTOs,
                StatusCode = 200,
            };
        }
    }
}

