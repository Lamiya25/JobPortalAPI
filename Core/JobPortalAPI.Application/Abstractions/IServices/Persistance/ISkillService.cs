using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.SkillDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.RequestParameters;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface ISkillService {
        Task<Response<bool>> AddSkillToUserProfile(string userId, SkillDTO skillDTO);
        Task<Response<List<JobPostGetDTO>>> SearchJobsBySkill(string skillName);
        Task<Response<List<SkillDTO>>> GetPopularSkills();

        Task<Response<List<JobPostGetDTO>>> SearchJobsBySkillPagination(string skillName, Pagination pagination);
    }

}
