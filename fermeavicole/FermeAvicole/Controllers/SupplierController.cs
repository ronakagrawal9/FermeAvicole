using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
namespace FermeAvicole.Controllers
{
    public class SupplierController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SupplierList()
        {
            List<SupplierVM> SupplierList = new List<SupplierVM>();
            SupplierList = (from s in db.Suppliers
                           select new SupplierVM()
                           {
                               Supplier_Id=s.Supplier_Id,
                               Supplier_Name=s.Supplier_Name,
                               Address=s.Address,
                               Pincode=s.Pincode,
                               Email_Id=s.Email_Id,
                               Contact_Number=s.Contact_Number

                           }).ToList();
            return View(SupplierList);
        }

        [HttpGet]
        public ActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierVM obj)
        {
            Supplier s = new Supplier();
            s.Supplier_Id = obj.Supplier_Id;
            s.Supplier_Name = obj.Supplier_Name;
            s.Address = obj.Address;
            s.Pincode = obj.Pincode;
            s.Contact_Number = obj.Contact_Number;
            s.Email_Id = obj.Email_Id;
            db.Suppliers.Add(s);
            db.SaveChanges();
            return RedirectToAction("SupplierList","Supplier");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SupplierVM obj = new SupplierVM();
            obj = (from c in db.Suppliers.Where(m => m.Supplier_Id == id)
                   select new SupplierVM()
                   {
                       Supplier_Id = c.Supplier_Id,
                       Supplier_Name = c.Supplier_Name,
                       Address = c.Address,
                       Pincode = c.Pincode,
                       Contact_Number = c.Contact_Number,
                       Email_Id = c.Email_Id,

        }).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(SupplierVM obj)
        {
            Supplier s;
            s = db.Suppliers.Where(m => m.Supplier_Id == obj.Supplier_Id).FirstOrDefault();
            if (s != null)
            {
                s.Supplier_Id = obj.Supplier_Id;
                s.Supplier_Name = obj.Supplier_Name;
                s.Address = obj.Address;
                s.Pincode = obj.Pincode;
                s.Contact_Number = obj.Contact_Number;
                s.Email_Id = obj.Email_Id;
                db.SaveChanges();
             }

            return RedirectToAction("SupplierList", "Supplier");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Supplier c;
            c = db.Suppliers.Where(m => m.Supplier_Id == id).FirstOrDefault();
            if (c != null)
            {
                db.Suppliers.Remove(c);
                db.SaveChanges();
            }

            return RedirectToAction("SupplierList", "Supplier");
           
        }
    }
}