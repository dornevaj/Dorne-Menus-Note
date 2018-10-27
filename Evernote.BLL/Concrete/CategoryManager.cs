using Evernote.BLL.Abstract;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Concrete
{
    public class CategoryManager : ICategoryService
    {
       public IEnumerable<Category> GetAllCategories(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy, string includeProperties, int skip, int take)
        {
            List<Category> categories;
            using (var uow = new UnitOfWork())
            {
                categories = new List<Category>();
                categories.AddRange(uow.GenericRepository<Category>().GetAll(predicate: predicate, orderBy:orderBy,includeProperties:includeProperties,skip:skip,take:take).ToList());
            }

            return categories;
        }
       public Category GetCategory(Expression<Func<Category, bool>> predicate)
        {
            Category category;
            using (var uow = new UnitOfWork())
            {
                category = new Category();
                category = uow.GenericRepository<Category>().Get(predicate:predicate);
            }

            return category;
        }
        public Category GetCategoryById(long id)
        {
            Category category;
            using (var uow = new UnitOfWork())
            {
                category = new Category();
                category = uow.GenericRepository<Category>().GetById(id);
            }

            return category;
        }
        public Category GetCategoryByIdInclude(long id,string includeProperties="")
        {
            Category category;
            using (var uow = new UnitOfWork())
            {
                category = new Category();
                category = uow.GenericRepository<Category>().GetByIdInclude(x => x.CategoryId == id, includeProperties: includeProperties);
            }

            return category;
        }
        public void AddCategory(Category category)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Category>().Add(category);
                uow.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Category>().Update(category);
                uow.SaveChanges();
            }
        }
        public void DeleteCategoryById(long id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Category>().DeleteById(id);
                uow.SaveChanges();
            }
        }
        public void DeleteCategory(Category category)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Category>().Delete(category);
                uow.SaveChanges();
            }
        }


    }
}
