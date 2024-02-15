using AutoMapper;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.ApplicationDTOs;
using JobPortalAPI.Application.DTOs.CategoryDTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using JobPortalAPI.Application.DTOs.LocationDTOs;
using JobPortalAPI.Application.DTOs.SkillDTOs;
using JobPortalAPI.Application.DTOs.UserDTOs;
using JobPortalAPI.Domain.Entities.Identity;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserGetDTO>();
            CreateMap<JobPostCreateDTO, JobPost>()
                .ForMember(dest => dest.RequiredSkills, opt => opt.MapFrom(src => src.RequiredSkills));

            CreateMap<JobPost, JobPostGetDTO>()
           .ForMember(dest => dest.JobPostId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<CompanyDTO, Company>().ReverseMap();
            CreateMap<CompanyGetDTO, Company>().ReverseMap();
            CreateMap<CompanyUpdateDTO, Company>();

            CreateMap<Location, LocationGetDTO>().ReverseMap();
            CreateMap<JobWithLocationsDTO, JobPost>().ReverseMap();

            CreateMap<Domain.Entities.JobPortalDBContext.Application, ApplicationStatusDTO>()
           .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).ReverseMap();

            CreateMap<ApplicationDTO, Domain.Entities.JobPortalDBContext.Application>().ReverseMap();
            CreateMap<ApplicationGetDTO, Domain.Entities.JobPortalDBContext.Application>().ReverseMap();

            CreateMap<Category, CategoryDTO>()
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap(); 
            CreateMap<Category,CategoryCreateDTO>().ReverseMap();


            CreateMap<Skill, SkillDTO>().ReverseMap();

        }
    }
}
