using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.UserDTOs
{
    public class CreateUserResponseDTO
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
