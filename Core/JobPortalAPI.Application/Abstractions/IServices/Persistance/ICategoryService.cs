using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance
{
    public interface ICategoryService
    {
        Task<Response<CategoryCreateDTO>> AddCategory(CategoryCreateDTO categoryDTO);
        Task<Response<bool>> UpdateCategory(CategoryDTO categoryDTO);
        Task<Response<bool>> DeleteCategory(string categoryId);
        Task<Response<CategoryDTO>> GetCategoryById(string categoryId);
        Task<Response<List<CategoryDTO>>> GetAllCategories(bool isDelete=false);
        Task<Response<List<JobPostGetDTO>>> GetJobPostsByCategory(string categoryId, bool isDelete);
        Task<Response<bool>> AddRangeCategoryAsync(List<CategoryCreateDTO> categoryDTOs);
    }
}
