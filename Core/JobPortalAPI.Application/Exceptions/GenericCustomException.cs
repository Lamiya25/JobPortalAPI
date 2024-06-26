﻿using JobPortalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions
{
    public class GenericCustomException<T> : Exception where T : ExceptionDTO
    {
        public T CustomData { get; private set; }
        public GenericCustomException() : base() { }
        public GenericCustomException(string message) : base(message) { }
        public GenericCustomException(string message, Exception innerException) : base(message, innerException) { }

        public GenericCustomException(T customData) : base()
        {
            CustomData = customData;
            if (customData is ExceptionDTO)
            {
                base.Data.Add("Message", customData.Message);
                if (customData.InnerException != null)
                {
                    base.Data.Add(("InnerException"), customData.InnerException);
                }
            }
        }

        public override string Message => CustomData?.Message ?? base.Message;
    }

    public static class CustomExceptionMessages
    {
        public static ExceptionDTO UserCreateFailed()
        {
            return new ExceptionDTO()
            {
                Message = "User creation failed"
            };
        }
        public static ExceptionDTO AnyEntityOperationFailed(string operationName, string entityName)
        {
            return new ExceptionDTO()
            {
                Message = $"{entityName} {operationName} failed. Please try again"
            };
        }
        public static ExceptionDTO AnyEntityOperationFailed(string text = null)
        {
            return new ExceptionDTO()
            {
                Message = text
            };
        }

        public static ExceptionDTO TableChangeOccupiedFailed(string text = null)
        {
            if (text == null)
            {
                return new ExceptionDTO()
                {
                    Message = "Failed to change the occupied status of the table"
                };
            }
            else
            {
                return new ExceptionDTO()
                {
                    Message = text
                };
            }
        }
    }       
}
