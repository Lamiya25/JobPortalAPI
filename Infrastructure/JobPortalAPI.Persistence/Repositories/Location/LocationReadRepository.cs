using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories.Location
{
    public class LocationReadRepository : ReadRepository<Domain.Entities.JobPortalDBContext.Location>, ILocationReadRepository
    {
        public LocationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
