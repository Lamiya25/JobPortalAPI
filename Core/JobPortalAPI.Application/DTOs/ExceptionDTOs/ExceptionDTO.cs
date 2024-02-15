using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.ExceptionDTOs
{
    public class ExceptionDTO
    {
        public string Message { get; set; }
        public Exception InnerException { get; set; }
    }
}
