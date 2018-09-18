using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class CategoryController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryList()
        {
            List<CategoryVM> cVM = new List<CategoryVM>();
            cVM = (from c in db.Categories
                   select new CategoryVM()
                   {
                       Category_Id = c.Category_Id,
                       Category_Name = c.Category_Name
                   }).ToList();
            return View(cVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryVM obj)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category();
                c.Category_Name = obj.Category_Name;
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category c;
            c = db.Categories.Where(m => m.Category_Id == id).FirstOrDefault();
            if (c != null)
            {
                db.Categories.Remove(c);
                db.SaveChanges();
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return RedirectToAction("CategoryList", "Category");
            }



        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryVM cVM = new CategoryVM();
            cVM = (from c in db.Categories.Where(m => m.Category_Id == id)
                   select new CategoryVM()
                   {
                       Category_Id = c.Category_Id,
                       Category_Name = c.Category_Name
                   }).FirstOrDefault();


            return View(cVM);
        }

        [HttpPost]
        public ActionResult Edit(CategoryVM obj)
        {
            Category c;
            if (ModelState.IsValid)
            {
                c = db.Categories.Where(m => m.Category_Id == obj.Category_Id).FirstOrDefault();
                if (c != null)
                {
                    c.Category_Name = obj.Category_Name;
                   
                    db.SaveChanges();
                    return RedirectToAction("CategoryList", "Category");
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }


        }
    }
}