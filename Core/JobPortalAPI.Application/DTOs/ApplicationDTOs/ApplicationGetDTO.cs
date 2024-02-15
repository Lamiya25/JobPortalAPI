using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.ApplicationDTOs
{
    public class ApplicationGetDTO
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }

    }
}
