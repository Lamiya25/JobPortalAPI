using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.UnitOfWork
{
    public interface IUnitOfWork<TRepo>:IDisposable where TRepo : BaseEntity
    {
        public IRepository<TRepo> Repository { get; }
        Task<int> SaveChangesAsync();
    }
}
