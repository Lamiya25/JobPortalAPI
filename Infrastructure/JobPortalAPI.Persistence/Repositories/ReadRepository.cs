using JobPortalAPI.Application.Exceptions;
using JobPortalAPI.Application.Repositories;
using JobPortalAPI.Domain.Entities.Common;
using JobPortalAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public ReadRepository(AppDbContext context)
        {
                _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true, bool isDeleted = false)
        /*  =>  await Table.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));*/
        {
            bool checkId = Guid.TryParse(id, out Guid guid);
            if (checkId)
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = Table.AsNoTracking();
                }
                return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            }
            else
            {
                throw new JobPortalAPI.Application.Exceptions.CommonExceptions.InvalidIdFormatException(id);
            }
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking, bool isDeleted = false)
        /*    => await Table.FirstOrDefaultAsync(method);*/
        {
            var query = Table.AsQueryable();
            if (!tracking)query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking, bool isDeleted = false)
        /* => Table.Where(method);*/
        {
            var query= Table.Where(method);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}
