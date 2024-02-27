using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.AuthenticationServices;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.Repositories.Category;
using JobPortalAPI.Application.Repositories.ProfileImages;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.Identity;
using JobPortalAPI.Persistence.Concretes.Services;
using JobPortalAPI.Persistence.Concretes.Services.ProfileImages;
using JobPortalAPI.Persistence.Concretes.Services.UserServices;
using JobPortalAPI.Persistence.Configuration;
using JobPortalAPI.Persistence.Context;
using JobPortalAPI.Persistence.Repositories;
using JobPortalAPI.Persistence.Repositories.Category;
using JobPortalAPI.Persistence.Repositories.Location;
using JobPortalAPI.Persistence.Repositories.Skills;
using JobPortalAPI.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Registration
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(DBConfiguration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IJobPostReadRepository, JobPostReadRepository>();
            services.AddScoped<IJobPostWriteRepository, JobPostWriteRepository>();

            services.AddScoped<ISkillsReadRepository, SkillsReadRepository>();
            services.AddScoped<ISkillsWriteRepository, SkillsWriteRepository>();

            services.AddScoped<IApplicationReadRepository, ApplicationReadRepository>();
            services.AddScoped<IApplicationWriteRepository, ApplicationWriteRepository>();
            services.AddScoped<IProfileImageWriteRepository,ProfileImageWriteRepository>();

            services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
            services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();

            services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();
            services.AddScoped<ILocationReadRepository, LocationReadRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAuthoService, AuthoService>();
            services.AddScoped<IExternalAuthenticationService, AuthoService>();
            services.AddScoped<IInternalAuthenticationService, AuthoService>();

            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobPostService, JobPostService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFileService, FileService>();
            


        }
    }
}
