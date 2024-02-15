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
    //ReadRepository<JobPost> buna gore customere ozel olan readrepositoeyunun butun metod hamsi uyguladacaq. Interface elave etmekde meqsed concrete objectin abstraction undu yeni dependency injection da bunnan teleb edirik. Yeni ikinci olarsa birinci olmazssa uygulanmaz.
    public class JobPostReadRepository : ReadRepository<JobPost>, IJobPostReadRepository
    {
        private readonly AppDbContext _appDbContext;
        public JobPostReadRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public async Task<List<JobPost>> GetJobsByLocations(List<string> cities, List<string> countries)
        {
                var jobs = await _appDbContext.JobPosts
                    .Include(j => j.Company)
                    .Where(j => cities.Contains(j.Location.City) && countries.Contains(j.Location.Country))
                    .ToListAsync();

                return jobs;
        }

        public async Task<List<JobPost>> SearchJobsByLocation(string city, string country)
        {

                var jobs = await _appDbContext.JobPosts
                    .Include(j => j.Location)
                    .Where(j => j.Location.City == city && j.Location.Country == country)
                    .ToListAsync();

                return jobs;
            }
        }
}
