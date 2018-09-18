using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
using System.IO;

namespace FermeAvicole.Controllers
{
    public class DoctorController : baseuserController
    {

        FermeDBEntities db = new FermeDBEntities();
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoctorList()
        {
            List<DoctorVM> lstItems = new List<DoctorVM>();
            lstItems = (from i in db.Doctors
                        select new DoctorVM()
                        {
                            Docter_id = i.Doctor_Id,
                            Doctor_Name = i.Doctor_Name,
                            Contact_Number = i.Contact_Number,
                            Email_Id = i.Email_Id,
                            Address = i.Address
                        }).ToList();
            return View(lstItems);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DoctorVM obj)
        {

            if (ModelState.IsValid)
            {
                Doctor i = new Doctor();
                i.Doctor_Id = obj.Docter_id;
                i.Doctor_Name = obj.Doctor_Name;
                i.Contact_Number = obj.Contact_Number;
                i.Email_Id = obj.Email_Id;
                i.Address = obj.Address;
                db.Doctors.Add(i);
                db.SaveChanges();
                return RedirectToAction("DoctorList", "Doctor");

            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DoctorVM iVM = new DoctorVM();
            iVM = (from i in db.Doctors.Where(m => m.Doctor_Id == id)
                   select new DoctorVM()
                   {
                       Docter_id = i.Doctor_Id,
                       Doctor_Name = i.Doctor_Name,
                       Contact_Number = i.Contact_Number,
                       Email_Id = i.Email_Id,
                       Address = i.Address,

                   }).FirstOrDefault();

            return View(iVM);
        }
        [HttpPost]
        public ActionResult Edit(DoctorVM obj)
        {

            if (ModelState.IsValid)
            {

                Doctor i;
                i = db.Doctors.Where(m => m.Doctor_Id == obj.Docter_id).FirstOrDefault();
                i.Doctor_Name = obj.Doctor_Name;
                i.Contact_Number = obj.Contact_Number;
                i.Email_Id = obj.Email_Id;
                i.Address = obj.Address;


                db.SaveChanges();
                return RedirectToAction("DoctorList", "Doctor");
            }
            else
            {
               
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Doctor i;
            i = db.Doctors.Where(m => m.Doctor_Id == id).FirstOrDefault();
            if (i != null)
            {
                db.Doctors.Remove(i);
                db.SaveChanges();
                return RedirectToAction("DoctorList", "Doctor");
            }
            return View();
        }
    }
}
    