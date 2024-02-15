using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.ApplicationExceptions
{
    public class ApplicationGetFailedException: GenericCustomException<ExceptionDTO>
    {
        public ApplicationGetFailedException(ExceptionDTO customData)
          : base(customData)
        { }

        public ApplicationGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public ApplicationGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"A problem was encountered during the lookup of the Application with ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "A problem occurred during the Application search process"
            };
        }
    }
}
