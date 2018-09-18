using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class PurchaseCategoryController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PurchaseCategoryList()
        {
            List<PurchaseCategoryVM> cVM = new List<PurchaseCategoryVM>();
            cVM = (from c in db.PurchaseCategories
                   select new PurchaseCategoryVM()
                   {
                       PurchaseCategory_Id = c.PurchaseCategory_Id,
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
        public ActionResult Create(PurchaseCategoryVM obj)
        {
            if (ModelState.IsValid)
            {
                PurchaseCategory c = new PurchaseCategory();
                c.Category_Name = obj.Category_Name;
                db.PurchaseCategories.Add(c);
                db.SaveChanges();
                return RedirectToAction("PurchaseCategoryList", "PurchaseCategory");
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PurchaseCategory c;
            c = db.PurchaseCategories.Where(m => m.PurchaseCategory_Id == id).FirstOrDefault();
            if (c != null)
            {
                db.PurchaseCategories.Remove(c);
                db.SaveChanges();
                return RedirectToAction("PurchaseCategoryList", "PurchaseCategory");
            }
            else
            {
                return RedirectToAction("PurchaseCategoryList", "PurchaseCategory");
            }



        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PurchaseCategoryVM cVM = new PurchaseCategoryVM();
            cVM = (from c in db.PurchaseCategories.Where(m => m.PurchaseCategory_Id == id)
                   select new PurchaseCategoryVM()
                   {
                       PurchaseCategory_Id = c.PurchaseCategory_Id,
                       Category_Name = c.Category_Name
                   }).FirstOrDefault();


            return View(cVM);
        }

        [HttpPost]
        public ActionResult Edit(PurchaseCategoryVM obj)
        {
            PurchaseCategory c;
            if (ModelState.IsValid)
            {
                c = db.PurchaseCategories.Where(m => m.PurchaseCategory_Id == obj.PurchaseCategory_Id).FirstOrDefault();
                if (c != null)
                {
                    c.Category_Name = obj.Category_Name;

                    db.SaveChanges();
                    return RedirectToAction("PurchaseCategoryList", "PurchaseCategory");
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