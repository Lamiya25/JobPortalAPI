using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{
    public interface IJobPostReadRepository:IReadRepository<JobPost>
    {
        Task<List<JobPost>> GetJobsByLocations(List<string> cities, List<string> countries);
        Task<List<JobPost>> SearchJobsByLocation(string city, string country);
    }
}
