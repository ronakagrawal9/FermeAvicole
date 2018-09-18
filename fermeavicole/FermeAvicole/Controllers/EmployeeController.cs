using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class EmployeeController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> GetRoleDDL()
        {
            List<SelectListItem> lstRole = new List<SelectListItem>();
            var data = db.Roles.ToList();
            foreach(var item in data)
            {
                lstRole.Add(new SelectListItem()
                {
                    Text=item.Role_Name,
                    Value=item.Role_Id.ToString(),
                    Selected=item.Role_Id==0

                });
            }
            return lstRole;
        }

        public ActionResult EmployeeList()
        {
            EmployeeVM obj = new EmployeeVM();
            obj.EmployeeList = (from c in db.Registrations
                                select new EmployeeVM()
                                {
                                    Employee_Id = c.User_Id,
                                    Role_Id = c.Role_Id,
                                    Email_Id = c.Email_Id,
                                    Password = c.Password,
                                    First_Name = c.First_Name,
                                    Last_Name = c.Last_Name,
                                    Contact_Number = c.Contact_Number,
                                    Image_Path = c.Image_Path,
                                    Address=c.Address,
                                    PinCode=c.PinCode,

                   }).ToList();
            return View(obj.EmployeeList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleDDL = GetRoleDDL();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Registration r;
            r = db.Registrations.Where(m => m.User_Id==id).FirstOrDefault();
            db.Registrations.Remove(r);
            db.SaveChanges();
            return RedirectToAction("EmployeeList", "Employee");
        }

        [HttpPost]
        public ActionResult Create(EmployeeVM obj)
        {
            if (ModelState.IsValid)
            {
                string filExtension = "";
                if (obj.ImageFile.FileName != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.ImageFile.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.ImageFile.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }
              
                Registration r=new Registration();
            

                r.User_Id = obj.Employee_Id;
                r.Role_Id = obj.Role_Id;
                r.Email_Id = obj.Email_Id;
                r.Password = obj.Password;
                r.First_Name = obj.First_Name;
                r.Last_Name = obj.Last_Name;
                r.Contact_Number = obj.Contact_Number;
                r.Image_Path = obj.Image_Path;
                r.Address = obj.Address;
                r.PinCode = obj.PinCode;

                db.Registrations.Add(r);
                db.SaveChanges();
                return RedirectToAction("EmployeeList", "Employee");
            }
            else
            {
                ViewBag.RoleDDL = GetRoleDDL();
                return View();
            }


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeVM obj = new EmployeeVM();
            obj = (from c in db.Registrations.Where(m => m.User_Id == id)
                   select new EmployeeVM()
                                {
                                    Employee_Id = c.User_Id,
                                    Role_Id = c.Role_Id,
                                    Email_Id = c.Email_Id,
                                    Password = c.Password,
                                    First_Name = c.First_Name,
                                    Last_Name = c.Last_Name,
                                    Contact_Number = c.Contact_Number,
                                    Image_Path = c.Image_Path,
                                    Address = c.Address,
                                    PinCode = c.PinCode,

                                }).FirstOrDefault();
            ViewBag.RoleDDL = GetRoleDDL();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeVM obj)
        {
            if (ModelState.IsValid)
            {
                string filExtension = "";
                if (obj.ImageFile.FileName != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.ImageFile.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.ImageFile.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                Registration r;
                r = db.Registrations.Where(m => m.User_Id==obj.Employee_Id).FirstOrDefault();


                r.User_Id = obj.Employee_Id;
                r.Role_Id = obj.Role_Id;
                r.Email_Id = obj.Email_Id;
                r.Password = obj.Password;
                r.First_Name = obj.First_Name;
                r.Last_Name = obj.Last_Name;
                r.Contact_Number = obj.Contact_Number;
                r.Image_Path = obj.Image_Path;
                r.Address = obj.Address;
                r.PinCode = obj.PinCode;


             
                db.SaveChanges();
                return RedirectToAction("EmployeeList", "Employee");
            }
            else
            {
                ViewBag.RoleDDL = GetRoleDDL();
                return View();
            }
          
        
        }

    }
}
