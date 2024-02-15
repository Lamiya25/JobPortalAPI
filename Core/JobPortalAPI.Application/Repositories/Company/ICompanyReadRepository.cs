using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{

    public interface ICompanyReadRepository: IReadRepository<Domain.Entities.JobPortalDBContext.Company>
    {
    }
}
