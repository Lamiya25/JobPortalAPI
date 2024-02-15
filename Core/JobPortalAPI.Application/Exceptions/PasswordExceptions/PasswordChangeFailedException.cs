using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.PasswordExceptions
{
    public class PasswordChangeFailedException : GenericCustomException<ExceptionDTO>
    {
        public PasswordChangeFailedException(ExceptionDTO exception)
           : base(exception) { }

        public PasswordChangeFailedException()
        : base(CreateException()) { }

        private static ExceptionDTO CreateException()
        {
            return new ExceptionDTO
            {
                Message = "An error occurred while changing the password."
            };
        }
    }
}
