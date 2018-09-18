using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class StoreController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult StoreList()
        {
            List<StoreVM> StoreList = new List<StoreVM>();
            StoreList = (from s in db.Stores
                         select new StoreVM()
                             {
                                Store_Id=s.Store_Id,
                                Latitude=s.Latitude,
                                Longitude=s.Longitude,
                                User_Id=s.User_Id,
                                Store_Name=s.Store_Name,
                                Description=s.Description

                            }).ToList();
            return View(StoreList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StoreVM obj)
        {
            Store s = new Store();
            s.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            s.Store_Id = obj.Store_Id;
            s.Latitude = obj.Latitude;
            s.Longitude = obj.Longitude;
            s.Store_Name = obj.Store_Name;
            s.Description = obj.Description;
            db.Stores.Add(s);
            db.SaveChanges();
            return RedirectToAction("StoreList", "Store");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            StoreVM obj = new StoreVM();
            obj = (from c in db.Stores.Where(m => m.Store_Id == id)
                   select new StoreVM()
                   {
                       Store_Id = c.Store_Id,
                       Latitude=c.Latitude,
                       Longitude=c.Longitude,
                       User_Id=c.User_Id,
                       Store_Name=c.Store_Name,
                       Description=c.Description


                   }).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(StoreVM obj)
        {
            Store s;
            s = db.Stores.Where(m => m.Store_Id== obj.Store_Id).FirstOrDefault();
            if (s != null)
            {
                s.Store_Id = obj.Store_Id;
                s.Store_Name = obj.Store_Name;

                s.Latitude = obj.Latitude;
                s.Longitude = obj.Longitude;
                s.User_Id = obj.User_Id;
                s.Description = obj.Description;
                db.SaveChanges();
            }

            return RedirectToAction("StoreList", "Store");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Store c;
            c = db.Stores.Where(m => m.Store_Id == id).FirstOrDefault();
            if (c != null)
            {
                db.Stores.Remove(c);
                db.SaveChanges();
            }

            return RedirectToAction("StoreList", "Store");

        }
    }
}