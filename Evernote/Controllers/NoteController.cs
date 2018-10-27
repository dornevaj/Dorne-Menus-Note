using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evernote.BLL.Abstract;
using Evernote.Entities.DataBaseContext;
using Evernote.Entities.Models;
using Evernote.Filters;
using Evernote.Helper;

namespace Evernote.Controllers
{
    [EvernoteAuthorizationFilter, EvernoteExceptionFilter]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        

        public NoteController(INoteService noteService,ICategoryService categoryService, IUserService userService, ICommentService commentService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
            _userService = userService;
            _commentService = commentService;
        }

        // GET: Note
        public ActionResult Index()
        {
            //Remove Category Cache 
            CatagoryCacheHelper.RemoveCategoryListFromCache();

            var notes = _noteService.GetAllNotes(includeProperties:"Category,User");
            return View(notes.ToList());
        }

        public ActionResult GetNotesByCategory(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                List<Note> notes = _noteService.GetAllNotes(x => x.CategoryId == Id, includeProperties: "User,Comments,Likes").ToList();
                TempData["GetNotesByCategory"] = notes;

                return RedirectToAction("Index", "Home");
            }


        }

        // GET: Note/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _noteService.GetNoteByIdInclude((long)id,includeProperties:"Category,User");
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                _noteService.AddNote(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _noteService.GetNoteByIdInclude((long)id, includeProperties: "Category,User");
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View(note);
        }

        // POST: Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _noteService.UpdateNote(note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _noteService.GetNoteByIdInclude((long)id, includeProperties: "Category,User");
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _noteService.DeleteNoteById(id);
            return RedirectToAction("Index");
        }

        
        public ActionResult NoteDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _noteService.GetNoteByIdInclude((long)id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoteDetails(Note note)
        {
            if (ModelState.IsValid)
            {
                _noteService.UpdateNote(note);
                return RedirectToAction("NoteDetails", note.NoteId);
            }
            ViewBag.CategoryId = new SelectList(CatagoryCacheHelper.GetCategoriesFromCache(), "CategoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAllUser(), "UserId", "Name");
            return View(note);
        }

        public ActionResult GetCommentWithNoteID(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Comment> comments = _commentService.GetAllComments(x => x.NoteId == id, includeProperties: "User").ToList();
            //if (!comments.Any())
            //{
            //    return HttpNotFound();
            //}

            return PartialView(comments);
        }
    }
}
