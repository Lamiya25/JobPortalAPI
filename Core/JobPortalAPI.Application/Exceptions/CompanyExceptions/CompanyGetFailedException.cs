using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.CompanyExceptions
{
    public class CompanyGetFailedException: GenericCustomException<ExceptionDTO>
    {
        public CompanyGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public CompanyGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public CompanyGetFailedException()
       : base(CreateExceptionData())
        { }

        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"A problem was encountered during the lookup of the company with ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "A problem occurred during the company search process"
            };
        }
    }
}
