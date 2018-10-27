using Evernote.BLL.Abstract;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Concrete
{
    public class UserManager : IUserService
    {      
        public IEnumerable<User> GetAllUser(Expression<Func<User, bool>> predicate = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            List<User> users;
            using (var uow = new UnitOfWork())
            {
                users = new List<User>();
                users.AddRange(uow.GenericRepository<User>().GetAll(predicate: predicate, orderBy: orderBy, includeProperties: includeProperties, skip: skip, take: take));
            }

            return users;
        }
        public User AddUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<User>().Add(user);
                uow.SaveChanges();
            }

            return user;
        }

        public User UpdateUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<User>().Update(user);
                uow.SaveChanges();
            }

            return user;
        }
    }
}
