using FluentValidation;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Validators.CompanyValidator
{
    public class AddCompanyDTOValidator : AbstractValidator<CompanyDTO>
    {
        public AddCompanyDTOValidator()
        {
            RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("CompanyName is required.")
            .MaximumLength(100).WithMessage("CompanyName cannot exceed 100 characters.");

            RuleFor(x => x.CompanyDescription)
                .NotEmpty().WithMessage("CompanyDescription is required.");

            RuleFor(x => x.CompanyEmail)
                .NotEmpty().WithMessage("CompanyEmail is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Industry)
                .NotEmpty().WithMessage("Industry is required.");

            RuleFor(x => x.WebsiteURL)
                .NotEmpty().WithMessage("WebsiteURL is required.")
                .Matches(@"^(http(s)?://)?([\w-]+.)+[\w-]+(/[\w- ;,./?%&=])?$")
                .WithMessage("Invalid URL format.");

            RuleFor(x => x.LogoURL)
                .NotEmpty().WithMessage("LogoURL is required.")
                .Matches(@"^(http(s)?://)?([\w-]+.)+[\w-]+(/[\w- ;,./?%&=])?$")
                .WithMessage("Invalid URL format.");

            RuleFor(x => x.EstablishedYear)
                .NotEmpty().WithMessage("EstablishedYear is required.")
                .GreaterThan(0).WithMessage("EstablishedYear must be greater than 0.");
        }
    }
}