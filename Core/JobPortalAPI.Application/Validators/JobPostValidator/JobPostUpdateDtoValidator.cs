using FluentValidation;
using JobPortalAPI.Application.DTOs.JobPostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Validators.JobPostValidator
{
    public class JobPostUpdateDTOValidator : AbstractValidator<JobPostUpdateDTO>
    {
        public JobPostUpdateDTOValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("JobId is required.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("JobTitle is required.");

            RuleFor(x => x.JobDescription)
                .NotEmpty().WithMessage("JobDescription is required.");

            RuleFor(x => x.EmploymentType)
                .NotEmpty().WithMessage("EmploymentType is required.");

            RuleFor(x => x.ApplicationDeadline)
                .NotEmpty().WithMessage("ApplicationDeadline is required.")
                .Must(BeAValidDate).WithMessage("ApplicationDeadline must be a valid date and time.")
                .GreaterThan(DateTime.UtcNow).WithMessage("ApplicationDeadline must be in the future.");
        }

        // Custom validation method to check if the date is valid
        private bool BeAValidDate(DateTime dateTime)
        {
            return dateTime != default;
        }
    }
}