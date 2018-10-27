using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Abstract
{
   public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(Expression<Func<Category, bool>> predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        Category GetCategory(Expression<Func<Category, bool>> predicate);
        Category GetCategoryById(long id);
        Category GetCategoryByIdInclude(long id, string includeProperties = "");
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategoryById(long id);
        void DeleteCategory(Category category);
    }
}
