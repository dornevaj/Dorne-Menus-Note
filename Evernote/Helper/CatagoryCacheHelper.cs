using Evernote.BLL.Abstract;
using Evernote.BLL.Concrete;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Evernote.Helper
{
    public class CatagoryCacheHelper
    {
        private readonly ICategoryService _categoryService;
        public  CatagoryCacheHelper(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //we cant use non static method in static method
        public static List<Category> GetCategoriesFromCache()
        {
            CatagoryCacheHelper catagoryCacheHelper = new CatagoryCacheHelper(new CategoryManager());
            return catagoryCacheHelper.Get();


        }

        public List<Category> Get()
        {
            var cache = WebCache.Get("CategoryList");
            if (cache == null)
            {
                cache = _categoryService.GetAllCategories();
                WebCache.Set("CategoryList", cache, 60, true);
            }

            return cache;
        }

       
        public static void RemoveCategoryListFromCache()
        {
            if (WebCache.Get("CategoryList") != null)
            {
                WebCache.Remove("CategoryList");
            }
        }

    }
}