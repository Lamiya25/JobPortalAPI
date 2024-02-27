using FluentValidation;
using JobPortalAPI.Application.Consts;
using JobPortalAPI.Application.Models.ViewModels;
using JobPortalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Validators
{
    public class CreateUserValidator:AbstractValidator<AppUser>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage(Messages.EmptyNameMessage).MaximumLength(25)
                                  .WithMessage(Messages.MaximumNameSymbolMessage);

            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage(Messages.EmptyNameMessage).MaximumLength(35)
                                    .WithMessage(Messages.MaximumLastNameSymbolMessage);

            RuleFor(x => x.Email).EmailAddress().WithMessage(Messages.InvalidEmailMessage).NotNull().NotEmpty()
                                    .WithMessage(Messages.EmptyEmailMessage);

            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage(Messages.EmptyNameMessage).MaximumLength(16)
                                    .WithMessage(Messages.MaximumUsernameSymbolMessage);

            RuleFor(x => x.BirthDate).NotNull().NotEmpty();
            //+XXX (XX) XXX XX XX
            RuleFor(x => x.PhoneNumber)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage(Messages.EmptyPhoneNumberMessage)
           .Matches(@"^\+\d{3}\s\(\d{2}\)\s\d{3}\s\d{2}\s\d{2}$").WithMessage("Invalid phone number format.");
        }
    }
}
