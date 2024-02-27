using FluentValidation;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Validators.JobPostValidator
{
    public class JobPostCreateDtoValidator : AbstractValidator<JobPostCreateDTO>
    {
        public JobPostCreateDtoValidator()
        {
            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

            RuleFor(x => x.JobDescription)
                .NotEmpty().WithMessage("Job description is required.");

            RuleFor(x => x.EmploymentType)
                .NotEmpty().WithMessage("Employment type is required.");

            RuleFor(x => x.MinSalary)
                .GreaterThanOrEqualTo(0).When(x => x.MinSalary.HasValue)
                .WithMessage("Minimum salary must be greater than or equal to 0.");

            RuleFor(x => x.MaxSalary)
                .GreaterThanOrEqualTo(x => x.MinSalary ?? 0).When(x => x.MaxSalary.HasValue && x.MinSalary.HasValue)
                .WithMessage("Maximum salary must be greater than or equal to minimum salary.");

            RuleFor(x => x.ApplicationDeadline)
                .Must(BeAValidDate).WithMessage("Application deadline must be a valid date.");

            RuleFor(x => x.CompanyID)
                .NotEmpty().WithMessage("Company ID is required.");

            RuleFor(x => x.LocationID)
                .NotEmpty().WithMessage("Location ID is required.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}