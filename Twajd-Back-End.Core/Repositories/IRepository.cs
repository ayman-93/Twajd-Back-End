using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> GetById(Guid id,
           string includeProperties = "");
        Task<T> GetById(Guid id);
        void Insert(T entity);
        void InsertRange(T[] entity);
        void Update(T entity);
        void Delete(Guid id);
        void DeleteRange(T[] entity);
    }
}
