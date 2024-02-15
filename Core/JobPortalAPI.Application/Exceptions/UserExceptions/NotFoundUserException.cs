using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.UserExceptions
{
    public class NotFoundUserException : GenericCustomException<ExceptionDTO>
    {
        public NotFoundUserException(ExceptionDTO exception)
            :base(exception) { }

        public NotFoundUserException()
        : base(CreateException()) { } 


        private static ExceptionDTO CreateException()
        {
            return new ExceptionDTO
            {
                Message = "Invalid username, email, or password. Please check your credentials and try again."
            };
        }
    }
}