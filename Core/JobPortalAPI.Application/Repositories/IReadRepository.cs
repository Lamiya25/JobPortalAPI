using JobPortalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{

    public interface IReadRepository<T>:IRepository<T> where T : BaseEntity
    {
        //sorgu uzerinde challishiriq deye yenede oyren deqiq
        IQueryable<T> GetAll(bool tracking=true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>>method, bool tracking=true, bool isDeleted = false);// shart verirsen dogrudusa datalar
        Task<T> GetSingleAsync(Expression<Func<T, bool>>method, bool tracking = true, bool isDeleted = false);
        Task<T> GetByIdAsync(string id, bool tracking = true, bool isDeleted = false);
    }

}
