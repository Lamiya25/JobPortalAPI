using JobPortalAPI.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.DTOs.UserDTOs
{
    public class LoginUserResponse
    {
    }
    public class LoginUserSuccessedResponse : LoginUserResponse
    {
        public TokenDTO TokenDTO { get; set; }
    }

    public class LoginUserErrorResponse : LoginUserResponse
    {
        public string ErrorMessage { get; set; }
    }
}
