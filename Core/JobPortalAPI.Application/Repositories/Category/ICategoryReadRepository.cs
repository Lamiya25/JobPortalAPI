using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories.Category
{
    public interface ICategoryReadRepository:IReadRepository<Domain.Entities.JobPortalDBContext.Category>
    {
    }
}

