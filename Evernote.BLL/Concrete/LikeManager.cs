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
    public class LikeManager : ILikeService
    {
        public void AddLike(Like like)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Like>().Add(like);
                uow.SaveChanges();
            }
        }

        public void DeleteLike(Like like)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Like>().Delete(like);
                uow.SaveChanges();
            }
        }

        public void DeleteLikeById(long id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Like>().DeleteById(id);
                uow.SaveChanges();
            }
        }

        public IEnumerable<Like> GetAllLikes(Expression<Func<Like, bool>> predicate = null, Func<IQueryable<Like>, IOrderedQueryable<Like>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            List<Like> likes;
            using (var uow = new UnitOfWork())
            {
                likes = new List<Like>();
                likes.AddRange(uow.GenericRepository<Like>().GetAll(predicate: predicate, orderBy: orderBy, includeProperties: includeProperties, skip: skip, take: take).ToList());
            }

            return likes;
        }

        public Like GetLike(Expression<Func<Like, bool>> predicate)
        {
            Like like;
            using (var uow = new UnitOfWork())
            {
                like = new Like();
                like = uow.GenericRepository<Like>().Get(predicate: predicate);
            }

            return like;
        }

        public Like GetLikeById(long id)
        {
            Like like;
            using (var uow = new UnitOfWork())
            {
                like = new Like();
                like = uow.GenericRepository<Like>().GetById(id);
            }

            return like;
        }

        public Like GetLikeByIdInclude(long id, string includeProperties = "")
        {
            Like like;
            using (var uow = new UnitOfWork())
            {
                like = new Like();
                like = uow.GenericRepository<Like>().GetByIdInclude(x => x.LikeId == id, includeProperties: includeProperties);
            }

            return like;
        }

        public void UpdateLike(Like like)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Like>().Update(like);
                uow.SaveChanges();
            }
        }
    }
}
