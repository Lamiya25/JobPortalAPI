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
    public class ApplicationReadRepository : ReadRepository<Domain.Entities.JobPortalDBContext.Application>, IApplicationReadRepository
    {
        private readonly AppDbContext _context;

        public ApplicationReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> GetTotalApplicationsCount()
        {
            return await _context.Applications.CountAsync();
        }

        public async Task<int> GetSuccessfulApplicationsCount()
        {
            return await _context.Applications.CountAsync(app => app.Status == "Accepted");
        }

        public async Task<Domain.Entities.JobPortalDBContext.Application> GetApplicationByUserIdAndAppIdAsync(string userId, string applicationId)
        {
            return await _context.Applications.FirstOrDefaultAsync(app => app.UserID == userId && app.Id.ToString() == applicationId);
        }
    }
}
