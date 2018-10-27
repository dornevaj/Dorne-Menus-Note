using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.DAL.Generic_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        T Get(Expression<Func<T, bool>> predicate = null);
        T GetById(long id);
        T GetByIdInclude(Expression<Func<T, bool>> predicate = null, string includeProperties="");
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(long id);
    }
}
