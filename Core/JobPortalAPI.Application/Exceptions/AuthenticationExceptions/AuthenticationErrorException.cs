using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.AuthenticationExceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Encountered authentication error.")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
