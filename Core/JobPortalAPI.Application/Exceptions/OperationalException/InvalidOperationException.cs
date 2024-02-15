using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Exceptions.OperationalException
{
    public class InvalidOperationException:Exception
    {
        public InvalidOperationException(): base("An error occurred while processing user data.") { }

        public InvalidOperationException(string? message) : base(message) { }

        public InvalidOperationException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
