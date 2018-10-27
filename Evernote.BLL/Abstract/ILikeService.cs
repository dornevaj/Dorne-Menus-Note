using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.BLL.Abstract
{
    public interface ILikeService
    {
        IEnumerable<Like> GetAllLikes(Expression<Func<Like, bool>> predicate = null, Func<IQueryable<Like>, IOrderedQueryable<Like>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        Like GetLike(Expression<Func<Like, bool>> predicate);
        Like GetLikeById(long id);
        Like GetLikeByIdInclude(long id, string includeProperties = "");
        void AddLike(Like like);
        void UpdateLike(Like like);
        void DeleteLikeById(long id);
        void DeleteLike(Like like);
    }
}
