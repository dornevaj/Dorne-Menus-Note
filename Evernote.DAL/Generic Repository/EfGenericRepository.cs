using Evernote.Entities.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.DAL.Generic_Repository
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EfGenericRepository(DataBaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = databaseContext;
            _dbSet = databaseContext.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);

            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (take != 0)
                {

                    return orderBy(query).Skip(skip).Take(take).ToList();



                }
                else
                {
                    return orderBy(query).Skip(skip).ToList();
                }

            }
            else//skip only support order by
            {
                if (take != 0)
                {

                    return query.Take(take).ToList();

                }
                else
                {

                    return query.ToList();
                }

            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public T GetByIdInclude(Expression<Func<T, bool>> predicate = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);

            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteById(long id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
        }
    }
}
