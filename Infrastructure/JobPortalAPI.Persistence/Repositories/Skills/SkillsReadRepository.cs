using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories.Skills
{
    public class SkillsReadRepository : ReadRepository<Skill>, ISkillsReadRepository
    {
        public SkillsReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
