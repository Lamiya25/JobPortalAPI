using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Domain.Entities.Identity;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Configuration.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company>Companies { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Domain.Entities.JobPortalDBContext.Application> Applications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var guidAdmin = Guid.NewGuid().ToString();
            var guidUser = Guid.NewGuid().ToString();
            var guidAdminCreate = Guid.NewGuid().ToString();

            builder.Entity<AppRole>().HasData(
               new AppRole { Id = guidAdmin, Name = "Admin", NormalizedName = "ADMIN" },
               new AppRole { Id = guidUser, Name = "User", NormalizedName = "USER" }
           );        

            var user = new AppUser
            {
                Id = guidAdminCreate,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "default",
                LastName = "default",
                BirthDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true

            };
            var hasher = new PasswordHasher<AppUser>();

            user.PasswordHash = hasher.HashPassword(user, "Admin.12!");

            builder.Entity<AppUser>().HasData(user);

            builder.Entity<AppUserRoles>().HasData(
                new AppUserRoles { UserId = guidAdminCreate, RoleId = guidAdmin });

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new JobPostConfiguration());
            builder.ApplyConfiguration(new SkillConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new ApplicationConfiguration());

        }

        //Interceptor
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker.Entries<BaseEntity>();

            foreach (var item in data)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreateDate = DateTime.Now;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdateDate = DateTime.Now;
                }
                else if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified;
                    item.Entity.IsDeleted = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
