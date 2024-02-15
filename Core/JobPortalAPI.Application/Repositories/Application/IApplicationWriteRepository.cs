using JobPortalAPI.Domain.Entities.JobPortalDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{
    public interface IApplicationWriteRepository: IWriteRepository<Domain.Entities.JobPortalDBContext.Application>
    {
    }
}
