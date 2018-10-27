using Evernote.BLL.Abstract;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.BLL.Concrete
{
    public class CommentManager : ICommentService
    {
        public  IEnumerable<Comment> GetAllComments(Expression<Func<Comment, bool>> predicate, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy, string includeProperties, int skip, int take)
        {
            List<Comment> comments;
            using (var uow = new UnitOfWork())
            {
                comments = new List<Comment>();
                comments.AddRange(uow.GenericRepository<Comment>().GetAll(predicate: predicate, orderBy: orderBy, includeProperties: includeProperties, skip: skip, take: take).ToList());
            }

            return comments;
        }
        public Comment GetComment(Expression<Func<Comment, bool>> predicate)
        {
            Comment comment;
            using (var uow = new UnitOfWork())
            {
                comment = new Comment();
                comment = uow.GenericRepository<Comment>().Get(predicate: predicate);
            }

            return comment;
        }
        public Comment GetCommentById(long id)
        {
            Comment comment;
            using (var uow = new UnitOfWork())
            {
                comment = new Comment();
                comment = uow.GenericRepository<Comment>().GetById(id);
            }

            return comment;
        }
        public Comment GetCommentByIdInclude(long id, string includeProperties = "")
        {
            Comment comment;
            using (var uow = new UnitOfWork())
            {
                comment = new Comment();
                comment = uow.GenericRepository<Comment>().GetByIdInclude(x => x.CommentId == id, includeProperties: includeProperties);
            }

            return comment;
        }
        public void AddComment(Comment comment)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Comment>().Add(comment);
                uow.SaveChanges();
            }
        }
        public void UpdateComment(Comment comment)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Comment>().Update(comment);
                uow.SaveChanges();
            }
        }
        public void DeleteCommentById(long id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Comment>().DeleteById(id);
                uow.SaveChanges();
            }
        }
        public void DeleteComment(Comment comment)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Comment>().Delete(comment);
                uow.SaveChanges();
            }
        }
        
    }
}
