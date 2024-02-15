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
    public class JobPostWriteRepository : WriteRepository<JobPost>, IJobPostWriteRepository
    {
        private readonly AppDbContext _context;
        public JobPostWriteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddApplicationAsync(Domain.Entities.JobPortalDBContext.Application application)
        {
            await _context.Applications.AddAsync(application);
            return true;
        }
    }
}
