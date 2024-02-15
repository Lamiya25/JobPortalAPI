using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.LocationDTOs
{
    public class LocationTrendDTO
    {
        public string LocationID { get; set; }
        public int NumberOfJobPosts { get; set; }
    }
}
