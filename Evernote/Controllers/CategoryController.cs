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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
      
        // GET: Category
        public ActionResult Index()
        {
            //After Category CRUD operations Remove Category Cache 
            CatagoryCacheHelper.RemoveCategoryListFromCache();
            
            return View(CatagoryCacheHelper.GetCategoriesFromCache());//Get Category With Cache Operations
            //return View(_categoryService.GetAllCategories().ToList());//without cache
        }

        //Get All Category for left side menu
        public ActionResult GetAllCategories()
        {
            //IQueryable<Category> categories = _categoryService.GetAllCategories().AsQueryable();
            //ViewBag.GetAllCategoriesForPartialView = categories;

            ViewBag.GetAllCategoriesForPartialView = CatagoryCacheHelper.GetCategoriesFromCache();

            return PartialView("GetAllCategories");
        }

        // GET: Category/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category category = db.Categories.Find(id);
            Category category = _categoryService.GetCategoryById((long)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById((long)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById((long)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _categoryService.DeleteCategoryById(id);
            return RedirectToAction("Index");
        }

    }
}
