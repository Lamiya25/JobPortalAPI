using JobPortalAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Repositories
{
    public interface IRepository<T>where T:BaseEntity
    {
        //T ne oldugunu bilmedikde bilmir nedi classdi enumdu ve s. DBSEt ise class olmasini isteyir. ona gore onun chapini qisaldib where ile bildiririk
        DbSet<T>Table {  get; } //dbset tekce tablemizi aliriq set etmirik
    }
}
