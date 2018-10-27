using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Abstract
{
   public interface IUserService
    {
        IEnumerable<User> GetAllUser(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        User AddUser(User user);
        User UpdateUser(User user);
    }
}
