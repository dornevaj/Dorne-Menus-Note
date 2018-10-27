using Evernote.BLL.Abstract;
using Evernote.Entities.Models;
using Evernote.Filters;
using Evernote.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evernote.Controllers
{
    [EvernoteAuthorizationFilter,EvernoteExceptionFilter]
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;
        public HomeController( INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Note> notes;

            //if User Click Any Category Get Notes By This CategoryID (NoteController/GetNotesByCategory/Id)
            if (TempData["GetNotesByCategory"] != null)
            {
                notes = TempData["GetNotesByCategory"] as List<Note>;
            }
            else//Without Clicked Any Category Take Some Random Notes To Display 
            {
                notes = _noteService.GetAllNotes(includeProperties: "User,Comments,Likes",take:15).ToList();
            }
             
            return View(notes);
        }

        public ActionResult TopRatedNotes()
        {
            List<Note> notes = _noteService.GetAllNotes(orderBy:x=>x.OrderByDescending(y=>y.Likes.Count()),includeProperties:"User,Comments,Likes",take:9).ToList();
            return View("Index",notes);
        }

        public ActionResult LatestNotes()
        {
            List<Note> notes = _noteService.GetAllNotes(orderBy:x=>x.OrderByDescending(y=>y.CreatedDate !=null),includeProperties:"User,Comments,Likes",take:9).ToList();
            return View("Index",notes);
        }

        public ActionResult MostCommented()
        {
            List<Note> notes = _noteService.GetAllNotes(orderBy: x => x.OrderByDescending(y => y.Comments.Count()), includeProperties: "User,Comments,Likes", take: 9).ToList();
            return View("Index", notes);
        }

        public ActionResult About()
        {           
            return View();
        }

        public ActionResult CheckError()
        {
            //test for exception filter
            int a = 0;
            int b = 1;
            int res = b / a;
            return View();
        }
    }
}