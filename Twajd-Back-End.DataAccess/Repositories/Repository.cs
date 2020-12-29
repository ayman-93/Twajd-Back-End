using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> dbSet;
        string errorMessage = string.Empty;
        public Repository(DatabaseContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }



        public async virtual Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        // get company by id with employees.
        public async virtual Task<T> GetById(Guid id,
           string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
           

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

           

            return await query.FirstOrDefaultAsync(com => com.Id == id);
        }


        public async Task<T> GetById(Guid id)
        {
            return await dbSet.SingleOrDefaultAsync(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dbSet.Add(entity);
            //context.SaveChanges();
        }

        public void InsertRange(T[] entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dbSet.AddRange(entity);
            //context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dbSet.Update(entity);
            //context.SaveChanges();    
        }
        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = dbSet.SingleOrDefault(s => s.Id == id);
            dbSet.Remove(entity);
            //context.SaveChanges();
        }
        public void DeleteRange(T[] entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dbSet.RemoveRange(entity);
            //context.SaveChanges();
        }
    }
}
