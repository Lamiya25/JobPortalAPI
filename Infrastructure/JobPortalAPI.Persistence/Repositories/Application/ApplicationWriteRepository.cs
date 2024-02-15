using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories
{
    public class ApplicationWriteRepository : WriteRepository<Domain.Entities.JobPortalDBContext.Application>, IApplicationWriteRepository
    {
        public ApplicationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
