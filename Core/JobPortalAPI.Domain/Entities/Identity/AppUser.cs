using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? RefreshToken { get; set; }   
        public DateTime? RefreshTokenExpires { get; set; }

        public ICollection<Location>? Locations { get; set; }

        public List<Application> Applications { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class AppRole:IdentityRole<string> { }
    public class AppUserRoles:IdentityUserRole<string> { }
}
