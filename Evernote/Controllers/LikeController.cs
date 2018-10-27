using Evernote.BLL.Abstract;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Evernote.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public ActionResult GetLikesNotesForCurrentUser(List<long> NoteIDList)
        {
            User user = Session["isLogin"] as User;

            if (NoteIDList == null && user != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<long> liked = _likeService.GetAllLikes(x => x.UserId == user.UserId && NoteIDList.Contains(x.NoteId)).Select(s => s.NoteId).ToList();

            if (!liked.Any())
            {
                return HttpNotFound();
            }

            return Json(liked);
        }

        public ActionResult LikeCondition(long? NoteId, bool? isLiked)
        {
            User user = Session["isLogin"] as User;

            if (NoteId != null && isLiked == null && user != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if ((bool)isLiked)//it is already liked by current user, wants make it unlike
            {
                Like like = _likeService.GetAllLikes(x => x.UserId == user.UserId && x.NoteId == NoteId).FirstOrDefault();

                if (like == null)
                {
                    return HttpNotFound();
                }
                _likeService.DeleteLikeById(like.LikeId);
            }
            else//new like by current user
            {
                _likeService.AddLike(new Like {
                    UserId = user.UserId,
                    NoteId = (long)NoteId
                });
            }

            var data = new
            {
                NoteId = NoteId,
                Count = _likeService.GetAllLikes(x=>x.NoteId == NoteId).Count()
            };

            return Json(data);
        }
    }
}