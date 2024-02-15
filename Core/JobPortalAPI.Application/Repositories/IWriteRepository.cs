using JobPortalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> model); 
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);//overload-i addasync nin
        bool RemoveRange(List<T> datas);
        bool Update(T model);

        Task<int> SaveAsync();


    }
}
