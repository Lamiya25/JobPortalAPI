using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Application.UnitOfWork;
using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Persistence.Context;
using JobPortalAPI.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.UnitOfWork
{
    public class UnitOfWork<TRepo> : IUnitOfWork<TRepo> where TRepo : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private IRepository<TRepo> repository;

        public UnitOfWork(AppDbContext appDbContext)
        {
                _appDbContext = appDbContext;
        }
        public IRepository<TRepo> Repository
        {
            get {

                if (repository==null)
                {
                    repository = new WriteRepository<TRepo>(_appDbContext);
                }
                return repository;
            }

        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
           int result=await _appDbContext.SaveChangesAsync();
            return result;
        }
    }
}
