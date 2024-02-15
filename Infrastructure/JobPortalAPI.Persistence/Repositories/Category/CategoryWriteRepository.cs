using JobPortalAPI.Application.Repositories.Category;
using JobPortalAPI.Persistence.Context;

namespace JobPortalAPI.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<Domain.Entities.JobPortalDBContext.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
