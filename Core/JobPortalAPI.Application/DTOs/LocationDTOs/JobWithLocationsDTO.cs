using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.LocationDTOs
{
    public class JobWithLocationsDTO
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string EmploymentType { get; set; }
        public float Salary { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
