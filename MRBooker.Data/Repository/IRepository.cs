using MRBooker.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRBooker.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
