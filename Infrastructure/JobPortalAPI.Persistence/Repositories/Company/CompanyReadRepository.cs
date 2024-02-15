using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using JobPortalAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories
{
    public class CompanyReadRepository : ReadRepository<Company>, ICompanyReadRepository
    {

        public CompanyReadRepository(AppDbContext context) : base(context)
        {
        }

    }
}
