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
    internal class LocationWriteRepository : WriteRepository<Domain.Entities.JobPortalDBContext.Location>, ILocationWriteRepository
    {
        public LocationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
