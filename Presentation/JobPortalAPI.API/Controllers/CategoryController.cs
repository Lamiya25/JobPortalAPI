using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDTO categoryDTO)
        {
            var response = await _categoryService.AddCategory(categoryDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var response = await _categoryService.DeleteCategory(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var response = await _categoryService.GetCategoryById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}/jobposts")]
        public async Task<IActionResult> GetJobPostsByCategory(string id, bool isDelete)
        {
            var response = await _categoryService.GetJobPostsByCategory(id, isDelete);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var response = await _categoryService.UpdateCategory(categoryDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("addRange")]
        public async Task<IActionResult> AddRangeCategory(List<CategoryCreateDTO> categoryDTOs)
        {
            var response = await _categoryService.AddRangeCategoryAsync(categoryDTOs);

            return StatusCode(response.StatusCode, response);
        }
    }
}
