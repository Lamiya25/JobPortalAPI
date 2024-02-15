using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories
{                                          
    public class SkillsWriteRepository : WriteRepository<Domain.Entities.JobPortalDBContext.Skill>, ISkillsWriteRepository
    {
        public SkillsWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
