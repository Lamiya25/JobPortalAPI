using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{
    public interface IApplicationReadRepository: IReadRepository<Domain.Entities.JobPortalDBContext.Application>
    {
        Task<int> GetTotalApplicationsCount();
        Task<int> GetSuccessfulApplicationsCount();
        Task<Domain.Entities.JobPortalDBContext.Application> GetApplicationByUserIdAndAppIdAsync(string userId, string applicationId);
    }
}
