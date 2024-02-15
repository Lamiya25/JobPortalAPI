using AutoMapper;
using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using JobPortalAPI.Application.Enums;
using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Exceptions.CategoryExceptions;
using JobPortalAPI.Application.Exceptions.JobPostExceptions;
using JobPortalAPI.Application.Models.ResponseModels;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.Repositories.Category;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Concretes.Services
{
    public class CategoryService : ICategoryService
    {
      private readonly ICategoryReadRepository _categoryReadRepository;
      private readonly ICategoryWriteRepository _categoryWriteRepository;
      private readonly IJobPostReadRepository _jobPostReadRepository;
        private readonly IUnitOfWork<Category> _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IJobPostReadRepository jobPostReadRepository, IUnitOfWork<Category> unitOfWork, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CategoryCreateDTO>> AddCategory(CategoryCreateDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryWriteRepository.AddAsync(categoryEntity);
            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return new Response<CategoryCreateDTO>
                {
                    Data = categoryDTO,
                    StatusCode = 201
                };
            }
            else
            {
                throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Category)));
            }
        }

        public async Task<Response<bool>> AddRangeCategoryAsync(List<CategoryCreateDTO> categoryDTOs)
        {
            if (categoryDTOs == null || !categoryDTOs.Any())
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var categoryEntities = _mapper.Map<List<Category>>(categoryDTOs);

            await _categoryWriteRepository.AddRangeAsync(categoryEntities);
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
                throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), "categories"));
            }
        }


        public async Task<Response<bool>> DeleteCategory(string categoryId)
        {
            if(string.IsNullOrEmpty(categoryId) || !Guid.TryParse(categoryId, out _))
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var category = await _categoryReadRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404
                };
            }
            _categoryWriteRepository.Remove(category);
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
        public async Task<Response<List<CategoryDTO>>> GetAllCategories(bool isDelete = false)
        {
            IQueryable<Category> query = _categoryReadRepository.GetAll(tracking: false);
            if (!isDelete)
            {
                query = query.Where(j => !j.IsDeleted); 
            }
            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var categortDTOs = _mapper.Map<List<CategoryDTO>>(data);
                return new Response<List<CategoryDTO>>
                {
                    Data = categortDTOs,
                    StatusCode = 200,
                };
            }
            else
            {
                return new Response<List<CategoryDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new CategoryGetFailedException();
        }

        public async Task<Response<CategoryDTO>> GetCategoryById(string categoryId)
        {
            var category = await _categoryReadRepository.GetByIdAsync(categoryId);

            if (category != null)
            {
                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                return new Response<CategoryDTO>
                {
                    Data = categoryDTO,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<CategoryDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
        }

        public async Task<Response<List<JobPostGetDTO>>> GetJobPostsByCategory(string categoryId, bool IsDelete)
        {
            var category = await _categoryReadRepository.GetByIdAsync(categoryId,IsDelete);
            if (category == null)
            {
                return new Response<List<JobPostGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            var jobPosts = category.JobPosts;
            if (jobPosts != null && jobPosts.Any())
            {
                var jobPostDTOs = _mapper.Map<List<JobPostGetDTO>>(jobPosts);
                return new Response<List<JobPostGetDTO>>
                {
                    Data = jobPostDTOs,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<JobPostGetDTO>>
                {
                    Data = null,
                    StatusCode = 204
                };
            }
        }

        public async Task<Response<bool>> UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = await _categoryReadRepository.GetByIdAsync(categoryDTO.CategoryID);
            if (category != null)
            {
                _mapper.Map(categoryDTO, category);
                _categoryWriteRepository.Update(category);
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
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Category)));
                }
            }
            else
            {
                return new Response<bool> { Data = false, StatusCode = 404 };
            }
        }
    }
}