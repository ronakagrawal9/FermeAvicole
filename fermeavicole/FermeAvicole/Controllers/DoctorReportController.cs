using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class DoctorReportController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: DoctorReport
        public ActionResult Index()
        {
            return View();
        }
        public List<SelectListItem> GetDoctorDDL()
        {
            List<SelectListItem> lstDoctors = new List<SelectListItem>();
            var data = db.Doctors.ToList();
            foreach (var item in data)
            {
                lstDoctors.Add(new SelectListItem()
                {
                    Text = item.Doctor_Name,
                    Value = item.Doctor_Id.ToString()

                });
            }
            return lstDoctors;
        }
        public List<SelectListItem> GetItemDDL()
        {
            List<SelectListItem> lstItems = new List<SelectListItem>();
            var data = db.Items.ToList();
            foreach (var item in data)
            {
                lstItems.Add(new SelectListItem()
                {
                    Text = item.Item_Name,
                    Value = item.Item_Id.ToString()
                });
            }
            return lstItems;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DoctorDDL = GetDoctorDDL();
            ViewBag.ItemDDL = GetItemDDL();
            return View();
        }
        [HttpPost]
        public ActionResult Create(DoctorReportVM obj)
        {
            if (ModelState.IsValid)
            {
                string filExtension = "";
                if (obj.ImageFile != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.ImageFile.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.ImageFile.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                DoctorReport r = new DoctorReport();


                r.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
                r.Doctor_Id = obj.Doctor_Id;
                r.Item_Id = obj.Item_Id;
                r.Report_Date = obj.Report_Date;
                r.File_Path = string.IsNullOrEmpty(obj.Image_Path) ? "/Upload/Default.png" : obj.Image_Path;
                r.Description = obj.Description;
                r.Remarks = obj.Remarks;

                db.DoctorReports.Add(r);
                db.SaveChanges();
                return RedirectToAction("DoctorReportList", "DoctorReport");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DoctorReportVM obj = new DoctorReportVM();
            obj = (from c in db.DoctorReports.Where(m=>m.DoctorReport_Id== id)
                   select new DoctorReportVM()
                   {
                       Doctor_Name = c.Doctor.Doctor_Name,
                       Doctor_Id = c.Doctor_Id,
                       Item_Name=c.Item.Item_Name,
                       Report_Date = c.Report_Date,
                       Image_Path = c.File_Path,
                       Description = c.Description,
                       Remarks = c.Remarks,
                       DoctorReport_Id=c.DoctorReport_Id
                   }).FirstOrDefault();
            ViewBag.DoctorDDL = GetDoctorDDL();
            ViewBag.ItemDDL = GetItemDDL();
            return View(obj);
        }



        [HttpPost]
        public ActionResult Edit(DoctorReportVM obj)
        {
            if (ModelState.IsValid)
            {
                string filExtension = "";
                if (obj.ImageFile != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.ImageFile.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.ImageFile.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                DoctorReport r;
                r = db.DoctorReports.Where(m => m.DoctorReport_Id == obj.DoctorReport_Id).FirstOrDefault();

                r.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID; ;
                r.Doctor_Id = obj.Doctor_Id;
                r.Item_Id = obj.Item_Id;
                r.Report_Date = obj.Report_Date;
                r.File_Path = string.IsNullOrEmpty(obj.Image_Path) ? "/Upload/Default.png" : obj.Image_Path;
                r.Description = obj.Description;
                r.Remarks = obj.Remarks;
                db.SaveChanges();
                return RedirectToAction("DoctorReportlist", "DoctorReport");
            }
            else
            {
                ViewBag.DoctorDDL = GetDoctorDDL();
                ViewBag.ItemDDL = GetItemDDL();
                return View();
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DoctorReport r;
            r = db.DoctorReports.Where(m => m.DoctorReport_Id == id).FirstOrDefault();
            db.DoctorReports.Remove(r);
            db.SaveChanges();
            return RedirectToAction("DoctorReportList", "DoctorReport");
           
        }

        public ActionResult DoctorReportList()
        {
            DoctorReportVM obj = new DoctorReportVM();
            obj.DoctorReportList = (from c in db.DoctorReports
                                    select new DoctorReportVM()
                                    {
                                        DoctorReport_Id = c.DoctorReport_Id,
                                        Doctor_Name = c.Doctor.Doctor_Name,
                                        Doctor_Id = c.Doctor_Id,
                                        Report_Date = c.Report_Date,
                                        Image_Path = c.File_Path,
                                        Description = c.Description,
                                        Remarks = c.Remarks,
                                        Item_Name=c.Item.Item_Name

                                    }).ToList();
            return View(obj.DoctorReportList);
        }

    }

}