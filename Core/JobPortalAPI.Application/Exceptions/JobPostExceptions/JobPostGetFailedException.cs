using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.JobPostExceptions
{
    public class JobPostGetFailedException: GenericCustomException<ExceptionDTO>
    {
        public JobPostGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public JobPostGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public JobPostGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"A problem was encountered during the lookup of the jobpost with ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "A problem occurred during the jobpost search process"
            };
        }
    }
}
