using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.DTOs.SkillDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.RequestParameters;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.Identity;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillsReadRepository _skillsReadRepository;
        private readonly ISkillsWriteRepository _skillsWriteRepository;
        private readonly IUnitOfWork<Skill> _unitOfWork;
        private readonly IMapper _mapper;
        readonly UserManager<AppUser>_userManager;
        private readonly AppDbContext appDbContext;

        public SkillService(ISkillsReadRepository skillsReadRepository, ISkillsWriteRepository skillsWriteRepository, IUnitOfWork<Skill> unitOfWork, IMapper mapper, UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _skillsReadRepository = skillsReadRepository;
            _skillsWriteRepository = skillsWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public async Task<Response<bool>> AddSkillToUserProfile(string userId, SkillDTO skillDTO)
        {
            if (string.IsNullOrEmpty(userId) || skillDTO == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var user= await _userManager.FindByIdAsync(userId);
            if (user==null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404
                };
            }
            if (user.Skills == null)
            {
                user.Skills = new List<Skill>();
            }

            var skill = new Skill
            {
                SkillName = skillDTO.SkillName,
                SkillDescription = skillDTO.SkillDescription,
            };
            user.Skills.Add(skill);

            var result=await _userManager.UpdateAsync(user);
            if (result.Succeeded)
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

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Company)));
        }

        public async Task<Response<List<SkillDTO>>> GetPopularSkills()
        {
            //to get job posts and their associated skills
            var jobPosts = await appDbContext.JobPosts
           .Include(j => j.RequiredSkills)
           .ToListAsync();

            var skillOccurrences = new Dictionary<string, int>();
            foreach (var jobPost in jobPosts)
            {
                foreach (var skill in jobPost.RequiredSkills)
                {
                    if (skillOccurrences.ContainsKey(skill.SkillName))
                    {
                        skillOccurrences[skill.SkillName]++;
                    }
                    else
                    {
                        skillOccurrences[skill.SkillName] = 1;
                    }
                }
            }
            var popularSkills = skillOccurrences
            .OrderByDescending(kv => kv.Value)
            .Select(kv => new SkillDTO { SkillName = kv.Key })
            .ToList();

            return new Response<List<SkillDTO>>
            {
                Data = popularSkills,
                StatusCode = 200,
            };
        }

        public async Task<Response<List<JobPostGetDTO>>> SearchJobsBySkill(string skillName)
        {

            var jobPosts= await appDbContext.JobPosts
                .Where(j=>j.RequiredSkills.Any(js=>js.SkillName== skillName))
                .ToListAsync();

            var jobPostDTos = _mapper.Map<List<JobPostGetDTO>>(jobPosts);

            return new Response<List<JobPostGetDTO>>
            {
                Data = jobPostDTos,
                StatusCode = 200,
            };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(JobPost)));
        }

        public async Task<Response<List<JobPostGetDTO>>> SearchJobsBySkillPagination(string skillName, Pagination pagination)
        {
            var query = appDbContext.JobPosts
                .Where(j => j.RequiredSkills.Any(js => js.SkillName == skillName))
                .OrderByDescending(j => j.CreateDate);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pagination.Size);

            var jobPosts = await query
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size)
                .ToListAsync();

            var jobPostDTos = _mapper.Map<List<JobPostGetDTO>>(jobPosts);

            return new Response<List<JobPostGetDTO>>
            {
                Data = jobPostDTos,
                StatusCode = 200
            };
        }
    }
}
