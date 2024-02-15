using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs.SkillDTOs;
using JobPortalAPI.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Persistence.Concretes.Services;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
            _service = service;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> AddSkillToUserProfile(string userId, SkillDTO skillDTO)
        {
            var data = await _service.AddSkillToUserProfile(userId, skillDTO);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchJobsBySkill(string skillName)
        {
            var data = await _service.SearchJobsBySkill(skillName);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPopularSkills()
        {
            var data = await _service.GetPopularSkills();
            return StatusCode(data.StatusCode, data);
        }
    }
}
