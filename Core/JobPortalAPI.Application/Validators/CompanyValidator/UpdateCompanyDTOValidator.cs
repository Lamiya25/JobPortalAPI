using FluentValidation;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Validators.CompanyValidator
{
    public class UpdateCompanyDTOValidator : AbstractValidator<CompanyUpdateDTO>
    {
        public UpdateCompanyDTOValidator()
        {
            RuleFor(x => x.CompanyName)
              .NotEmpty().WithMessage("CompanyName is required.");

            RuleFor(x => x.CompanyDescription)
                .NotEmpty().WithMessage("CompanyDescription is required.");

            RuleFor(x => x.CompanyEmail)
                .NotEmpty().WithMessage("CompanyEmail is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.Industry)
                .NotEmpty().WithMessage("Industry is required.");

            RuleFor(x => x.WebsiteURL)
                .NotEmpty().WithMessage("WebsiteURL is required.")
                .Must(BeAValidUrl).WithMessage("Invalid URL format.");

            RuleFor(x => x.LogoURL)
                .Must(BeAValidUrl).WithMessage("Invalid LogoURL format.");

            RuleFor(x => x.EstablishedYear)
                .NotEmpty().WithMessage("EstablishedYear is required.")
                .GreaterThan(0).WithMessage("EstablishedYear must be greater than 0.");
        }

        private bool BeAValidUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url);
        }
    }
}