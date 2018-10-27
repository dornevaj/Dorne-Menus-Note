using Evernote.BLL.Abstract;
using Evernote.Entities.Models;
using Evernote.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Evernote.Controllers
{
    [EvernoteAuthorizationFilter, EvernoteExceptionFilter]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult AddComment(long? NoteID, string Text)
        {
            User user = null;
            if(string.IsNullOrEmpty(Text) && string.IsNullOrWhiteSpace(Text) && NoteID ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["isLogin"] !=null)
            {
                 user = Session["isLogin"] as User;
            }

            _commentService.AddComment(new Comment {
                NoteId = (long)NoteID,
                Text = Text,
                UserId = user != null ? user.UserId : 1
            });

            return RedirectToAction("GetCommentWithNoteID", "Note", new {id = NoteID });
        }

        [HttpPost]
        public ActionResult DeleteComment(long? CommentId, long? NoteID)
        {

            if (CommentId == null && NoteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _commentService.DeleteCommentById((long)CommentId);

            return RedirectToAction("GetCommentWithNoteID", "Note", new { id = NoteID });
        }
    }
}