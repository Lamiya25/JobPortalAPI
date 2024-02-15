using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.Repositories.Category;
using JobPortalAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories.Category
{
    public class CategoryReadRepository : ReadRepository<Domain.Entities.JobPortalDBContext.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
