using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace JobPortalAPI.Persistence.Configuration.EntityConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CompanyDescription)
                .IsRequired();

            builder.Property(c => c.CompanyEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Industry)
                .HasMaxLength(50);

            builder.Property(c => c.WebsiteURL)
                .HasMaxLength(200);

            builder.Property(c => c.LogoURL)
                .HasMaxLength(200);

            builder.HasMany(c => c.JobPosts)
                .WithOne(jp => jp.Company)
                .HasForeignKey(jp => jp.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class JobPostConfiguration : IEntityTypeConfiguration<JobPost>
    {
        public void Configure(EntityTypeBuilder<JobPost> builder)
        {
            builder.Property(j => j.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(j => j.JobDescription)
                .IsRequired();

            builder.Property(j => j.EmploymentType)
                .HasMaxLength(50);

            builder.HasOne(j => j.Company)
                .WithMany(c => c.JobPosts)
                .HasForeignKey(j => j.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(j => j.Location)
                .WithMany(l=>l.JobPosts)
                .HasForeignKey(j => j.LocationID)
                .IsRequired(false);

            builder.HasMany(j => j.Applications)
                .WithOne(a => a.JobPost)
                .HasForeignKey(a => a.JobPostID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(s => s.SkillName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SkillDescription);

            builder.HasMany(s => s.Users)
                .WithMany(u => u.Skills)
                .UsingEntity(j => j.ToTable("UserSkills"));

            builder.HasMany(s => s.JobPosts)
                .WithMany(u => u.RequiredSkills)
                .UsingEntity(j => j.ToTable("JobSkills"));
        }
    }

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(l => l.AppUser)
           .WithMany(u => u.Locations) 
           .HasForeignKey(l => l.AppUserId)
           .IsRequired(false); // Make the relationship optional by allowing null values for AppUserId
        }
    }

    public class ApplicationConfiguration : IEntityTypeConfiguration<JobPortalAPI.Domain.Entities.JobPortalDBContext.Application>
    {
        public void Configure(EntityTypeBuilder<JobPortalAPI.Domain.Entities.JobPortalDBContext.Application> builder)
        {
            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.JobPost)
                .WithMany(jp => jp.Applications)
                .HasForeignKey(a => a.JobPostID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
          builder.Property(c=>c.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(c=>c.JobPosts)
                .WithOne(j=>j.Category)
                .HasForeignKey(j=>j.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}