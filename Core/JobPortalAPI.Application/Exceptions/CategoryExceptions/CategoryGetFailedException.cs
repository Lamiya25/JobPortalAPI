using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.CategoryExceptions
{
    public class CategoryGetFailedException: GenericCustomException<ExceptionDTO>
    {
        public CategoryGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public CategoryGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public CategoryGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"A problem was encountered during the lookup of the Category with ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "A problem occurred during the Category search process"
            };
        }
    }
}
