using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.BLL.Abstract
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        Comment GetComment(Expression<Func<Comment, bool>> predicate);
        Comment GetCommentById(long id);
        Comment GetCommentByIdInclude(long id, string includeProperties = "");
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteCommentById(long id);
        void DeleteComment(Comment comment);
    }
}
